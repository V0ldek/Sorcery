// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace Discrepancy.UnitTests;

public class UnitTests
{
    public static TheoryData<Implementation, int?> AllImplementations
    {
        get
        {
            const int Size = 100_000;
            var benchmark = typeof(Benches);

            var implementations =
                from m in benchmark.GetMethods()
                where m.GetCustomAttribute<BenchmarkAttribute>() is not null
                let name = m.Name
                let del = m.CreateDelegate<Func<Benches, int?>>()
                let f = (Func<int?>)(() =>
                {
                    var benches = new Benches();
                    benches.Length = 100_000;
                    benches.Setup();
                    return del(benches);
                })
                select new Implementation(name, f);

            var theoryData = new TheoryData<Implementation, int?>();

            foreach (var x in implementations)
            {
                theoryData.Add(x, Size - 1);
            }

            return theoryData;
        }
    }

    [Theory]
    [MemberData(nameof(AllImplementations))]
    public void TestAllImplementations(Implementation implementation, int? expected)
    {
        var actual = implementation.Function();

        Assert.Equal(expected, actual);
    }

    public readonly record struct Implementation(string Name, Func<int?> Function);
}
