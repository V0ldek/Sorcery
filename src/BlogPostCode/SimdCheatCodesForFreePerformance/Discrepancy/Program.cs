﻿// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Sse2 = System.Runtime.Intrinsics.X86.Sse2;
using Avx2 = System.Runtime.Intrinsics.X86.Avx2;
using Avx512 = System.Runtime.Intrinsics.X86.Avx512BW;

BenchmarkRunner.Run<Benches>();

[DisassemblyDiagnoser]
public class Benches
{
    [Params(1_000_000)]
    public int Length { get; set; }

    public byte[] Stream1 { get; set; } = Array.Empty<byte>();

    public byte[] Stream2 { get; set; } = Array.Empty<byte>();

    public const int Seed = 2137;

    [GlobalSetup]
    public void Setup()
    {
        Stream1 = new byte[Length];
        Stream2 = new byte[Length];

        var rng = new Random(Seed);
        rng.NextBytes(Stream1);

        Stream1.CopyTo(Stream2, 0);

        Stream2[^1] ^= 0x01;
    }

    [Benchmark(Baseline = true)]
    public int? Sequential() => Sequential(Stream1, Stream2);

    [Benchmark]
    public int? Simd32() => Simd32(Stream1, Stream2);

    [Benchmark]
    public int? Simd64() => Simd64(Stream1, Stream2);

    [Benchmark]
    public int? Simd128SSE() => Simd128Manual(Stream1, Stream2);

    [Benchmark]
    public int? Simd128Portable() => Simd128Portable(Stream1, Stream2);

    [Benchmark]
    public int? Simd256AVX() => Simd256Manual(Stream1, Stream2);

    [Benchmark]
    public int? Simd256Portable() => Simd256Portable(Stream1, Stream2);

    [Benchmark]
    public int? Simd512Portable() => Simd512Portable(Stream1, Stream2);

    [MethodImpl(MethodImplOptions.NoInlining)]
    private int? Sequential(ReadOnlySpan<byte> sensor1, ReadOnlySpan<byte> sensor2)
    {
        if (sensor1.Length != sensor2.Length)
        {
            throw new ArgumentException("Unequal stream lengths");
        }

        for (var i = 0; i < sensor1.Length; i += 1)
        {
            if (sensor1[i] != sensor2[i])
            {
                return i;
            }
        }

        return null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private int? Simd32(ReadOnlySpan<byte> sensor1, ReadOnlySpan<byte> sensor2)
    {
        if (sensor1.Length != sensor2.Length)
        {
            throw new ArgumentException("Unequal stream lengths");
        }
        const int Size = 4;

        DetachFullBlocks(sensor1, Size, out var stream1, out var remainder1);
        DetachFullBlocks(sensor2, Size, out var stream2, out var remainder2);

        for (var i = 0; i < stream1.Length; i += Size)
        {
            var value1 = BitConverter.ToUInt32(stream1[i..(i + Size)]);
            var value2 = BitConverter.ToUInt32(stream2[i..(i + Size)]);
            var xor = value1 ^ value2;

            if (xor != 0)
            {
                var offset = BitOperations.TrailingZeroCount(xor) / 8;
                return i + offset;
            }
        }

        return Sequential(remainder1, remainder2) + stream1.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private int? Simd64(ReadOnlySpan<byte> sensor1, ReadOnlySpan<byte> sensor2)
    {
        if (sensor1.Length != sensor2.Length)
        {
            throw new ArgumentException("Unequal stream lengths");
        }
        const int Size = 8;

        // Take the cleanly divisible part and leave the remainders for later.
        DetachFullBlocks(sensor1, Size, out var stream1, out var remainder1);
        DetachFullBlocks(sensor2, Size, out var stream2, out var remainder2);

        // Stride by 8 bytes at a time...
        for (var i = 0; i < stream1.Length; i += Size)
        {
            // ... interpreting each 8-byte block as a single 64-bit number.
            var value1 = BitConverter.ToUInt64(stream1[i..(i + Size)]);
            var value2 = BitConverter.ToUInt64(stream2[i..(i + Size)]);
            var xor = value1 ^ value2;

            if (xor != 0)
            {
                var offset = BitOperations.TrailingZeroCount(xor) / 8;
                return i + offset;
            }
        }

        return Sequential(remainder1, remainder2) + stream1.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private unsafe int? Simd128Manual(ReadOnlySpan<byte> sensor1, ReadOnlySpan<byte> sensor2)
    {
        if (sensor1.Length != sensor2.Length)
        {
            throw new ArgumentException("Unequal stream lengths");
        }
        const int Size = 16;

        // Take the cleanly divisible part and leave the reminder for later.
        DetachFullBlocks(sensor1, Size, out var stream1, out var remainder1);
        DetachFullBlocks(sensor2, Size, out var stream2, out var remainder2);

        // Unsafe fun!
        // Soundness follows from the fact that we increment by Size up until
        // reaching stream1.Length, and we've asserted both streams are of equal size.
        fixed (byte* sensor1Ptr = &MemoryMarshal.GetReference(stream1))
        fixed (byte* sensor2Ptr = &MemoryMarshal.GetReference(stream2))
        {
            byte* sensor1Current = sensor1Ptr;
            byte* sensor2Current = sensor2Ptr;

            for (var i = 0; i < stream1.Length; i += Size)
            {
                Vector128<byte> vector1 = Sse2.LoadVector128(sensor1Current);
                Vector128<byte> vector2 = Sse2.LoadVector128(sensor2Current);

                Vector128<byte> cmpeq = Sse2.CompareEqual(vector1, vector2);
                int mask = Sse2.MoveMask(cmpeq);

                if (mask != 0xFFFF)
                {
                    var offset = BitOperations.TrailingZeroCount(~mask);
                    return i + offset;
                }

                sensor1Current += Size;
                sensor2Current += Size;
            }
        }

        return Sequential(remainder1, remainder2) + stream1.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private int? Simd128Portable(ReadOnlySpan<byte> sensor1, ReadOnlySpan<byte> sensor2)
    {
        if (sensor1.Length != sensor2.Length)
        {
            throw new ArgumentException("Unequal stream lengths");
        }
        const int Size = 16;

        // Take the cleanly divisible part and leave the reminder for later.
        DetachFullBlocks(sensor1, Size, out var stream1, out var remainder1);
        DetachFullBlocks(sensor2, Size, out var stream2, out var remainder2);

        ref byte sensor1Current = ref MemoryMarshal.GetReference(stream1);
        ref byte sensor2Current = ref MemoryMarshal.GetReference(stream2);

        for (var i = 0; i < stream1.Length; i += Size)
        {
            Vector128<byte> vector1 = Vector128.LoadUnsafe(ref sensor1Current);
            Vector128<byte> vector2 = Vector128.LoadUnsafe(ref sensor2Current);

            Vector128<byte> cmpeq = Vector128.Equals(vector1, vector2);
            uint mask = Vector128.ExtractMostSignificantBits(cmpeq);

            if (mask != 0xFFFF)
            {
                var offset = BitOperations.TrailingZeroCount(~mask);
                return i + offset;
            }

            sensor1Current = ref Unsafe.Add(ref sensor1Current, Size);
            sensor2Current = ref Unsafe.Add(ref sensor2Current, Size);
        }

        return Sequential(remainder1, remainder2) + stream1.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private unsafe int? Simd256Manual(ReadOnlySpan<byte> sensor1, ReadOnlySpan<byte> sensor2)
    {
        if (sensor1.Length != sensor2.Length)
        {
            throw new ArgumentException("Unequal stream lengths");
        }
        const int Size = 32;

        // Take the cleanly divisible part and leave the reminder for later.
        DetachFullBlocks(sensor1, Size, out var stream1, out var remainder1);
        DetachFullBlocks(sensor2, Size, out var stream2, out var remainder2);

        // Unsafe fun!
        // Soundness follows from the fact that we increment by Size up until
        // reaching stream1.Length, and we've asserted both streams are of equal size.
        fixed (byte* sensor1Ptr = &MemoryMarshal.GetReference(stream1))
        fixed (byte* sensor2Ptr = &MemoryMarshal.GetReference(stream2))
        {
            byte* sensor1Current = sensor1Ptr;
            byte* sensor2Current = sensor2Ptr;

            for (var i = 0; i < stream1.Length; i += Size)
            {
                Vector256<byte> vector1 = Avx2.LoadVector256(sensor1Current);
                Vector256<byte> vector2 = Avx2.LoadVector256(sensor2Current);

                Vector256<byte> cmpeq = Avx2.CompareEqual(vector1, vector2);
                uint mask = (uint)Avx2.MoveMask(cmpeq);

                if (mask != 0xFFFFFFFF)
                {
                    var offset = BitOperations.TrailingZeroCount(~mask);
                    return i + offset;
                }

                sensor1Current += Size;
                sensor2Current += Size;
            }
        }

        return Sequential(remainder1, remainder2) + stream1.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private int? Simd256Portable(ReadOnlySpan<byte> sensor1, ReadOnlySpan<byte> sensor2)
    {
        if (sensor1.Length != sensor2.Length)
        {
            throw new ArgumentException("Unequal stream lengths");
        }
        const int Size = 32;

        // Take the cleanly divisible part and leave the reminder for later.
        DetachFullBlocks(sensor1, Size, out var stream1, out var remainder1);
        DetachFullBlocks(sensor2, Size, out var stream2, out var remainder2);

        ref byte sensor1Current = ref MemoryMarshal.GetReference(stream1);
        ref byte sensor2Current = ref MemoryMarshal.GetReference(stream2);

        for (var i = 0; i < stream1.Length; i += Size)
        {
            Vector256<byte> vector1 = Vector256.LoadUnsafe(ref sensor1Current);
            Vector256<byte> vector2 = Vector256.LoadUnsafe(ref sensor2Current);

            Vector256<byte> cmpeq = Vector256.Equals(vector1, vector2);
            uint mask = Vector256.ExtractMostSignificantBits(cmpeq);

            if (mask != 0xFFFFFFFF)
            {
                var offset = BitOperations.TrailingZeroCount(~mask);
                return i + offset;
            }

            sensor1Current = ref Unsafe.Add(ref sensor1Current, Size);
            sensor2Current = ref Unsafe.Add(ref sensor2Current, Size);
        }

        return Sequential(remainder1, remainder2) + stream1.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private int? Simd512Portable(ReadOnlySpan<byte> sensor1, ReadOnlySpan<byte> sensor2)
    {
        if (sensor1.Length != sensor2.Length)
        {
            throw new ArgumentException("Unequal stream lengths");
        }
        const int Size = 64;

        // Take the cleanly divisible part and leave the reminder for later.
        DetachFullBlocks(sensor1, Size, out var stream1, out var remainder1);
        DetachFullBlocks(sensor2, Size, out var stream2, out var remainder2);

        ref byte sensor1Current = ref MemoryMarshal.GetReference(stream1);
        ref byte sensor2Current = ref MemoryMarshal.GetReference(stream2);

        for (var i = 0; i < stream1.Length; i += Size)
        {
            Vector512<byte> vector1 = Vector512.LoadUnsafe(ref sensor1Current);
            Vector512<byte> vector2 = Vector512.LoadUnsafe(ref sensor2Current);

            Vector512<byte> cmpeq = Vector512.Equals(vector1, vector2);
            ulong mask = Vector512.ExtractMostSignificantBits(cmpeq);

            if (mask != 0xFFFFFFFFFFFFFFFF)
            {
                var offset = BitOperations.TrailingZeroCount(~mask);
                return i + offset;
            }

            sensor1Current = ref Unsafe.Add(ref sensor1Current, Size);
            sensor2Current = ref Unsafe.Add(ref sensor2Current, Size);
        }

        return Sequential(remainder1, remainder2) + stream1.Length;
    }

    private static void DetachFullBlocks(
        ReadOnlySpan<byte> bytes,
        int blockSize,
        out ReadOnlySpan<byte> fullBlocks,
        out ReadOnlySpan<byte> remainder)
    {
        var numberOfFullBlocks = bytes.Length / blockSize;
        var prefixLength = numberOfFullBlocks * blockSize;

        fullBlocks = bytes[..prefixLength];
        remainder = bytes[prefixLength..];
    }
}
