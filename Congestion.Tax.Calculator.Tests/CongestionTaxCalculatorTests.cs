using System.Collections;
using Xunit;

namespace Congestion.Tax.Calculator.Tests;

public class CongestionTaxCalculatorTests
{
    [Theory]
    [MemberData(nameof(CustomTestData))]
    public void IsTollFreeDate(DateTime dateTime)
    {
        var sut = new CongestionTaxCalculator();

        var result = sut.IsTollFreeDate(dateTime);

        Assert.True(result);
    }

    public static IEnumerable<object[]> CustomTestData =>
        new List<object[]>
        {
            new Object[] { new DateTime(2013, 1, 1) },
            new Object[] { new DateTime(2013, 3, 28) },
            new Object[] { new DateTime(2013, 3, 29) },
            new Object[] { new DateTime(2013, 4, 1) },
            new Object[] { new DateTime(2013, 4, 30) },
            new Object[] { new DateTime(2013, 5, 1) },
            new Object[] { new DateTime(2013, 5, 8) },
            new Object[] { new DateTime(2013, 5, 9) },
            new Object[] { new DateTime(2013, 6, 5) },
            new Object[] { new DateTime(2013, 6, 6) },
            new Object[] { new DateTime(2013, 6, 21) },
            new Object[] { new DateTime(2013, 11, 1) },
            new Object[] { new DateTime(2013, 12, 24) },
            new Object[] { new DateTime(2013, 12, 25) },
            new Object[] { new DateTime(2013, 12, 26) },
            new Object[] { new DateTime(2013, 12, 31) }
        };
}