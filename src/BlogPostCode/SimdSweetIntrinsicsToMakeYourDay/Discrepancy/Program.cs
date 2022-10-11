// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benches>();

public class Benches
{
    [Params(100_000)]
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

        // Take the cleanly divisible part and leave the reminder for later.
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
