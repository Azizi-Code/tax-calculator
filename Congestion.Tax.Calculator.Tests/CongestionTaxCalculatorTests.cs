using Congestion.Tax.Calculator.Vehicles;
using Xunit;

namespace Congestion.Tax.Calculator.Tests;

public class CongestionTaxCalculator_GetTollFee
{
    [Theory]
    [MemberData(nameof(CustomTestData))]
    public void FreeTollDates_ReturnsZeroToll(DateTime[] dateTime)
    {
        var sut = new CongestionTaxCalculator();

        var result = sut.CalculateTax(new Car(), dateTime);

        Assert.Equal(0, result);
    }

    public static IEnumerable<object[]> CustomTestData =>
        new List<object[]>
        {
            new Object[] { new[] { new DateTime(2013, 1, 1, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 3, 28, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 3, 29, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 4, 1, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 4, 30, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 5, 1, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 5, 8, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 5, 9, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 6, 5, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 6, 6, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 6, 21, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 11, 1, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 12, 24, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 12, 25, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 12, 26, 6, 30, 0) } },
            new Object[] { new[] { new DateTime(2013, 12, 31, 6, 30, 0) } }
        };

    [Theory]
    [MemberData(nameof(ValidHoursTestData))]
    public void ValidHoursForSekFee_ReturnsExpectedFee(int expectedFee, DateTime dateTime)
    {
        var sut = new CongestionTaxCalculator();

        var result = sut.GetTollFee(dateTime);

        Assert.Equal(expectedFee, result);
    }

    [Fact]
    public void ValidDates_ReturnsExpectedFee()
    {
        var sut = new CongestionTaxCalculator();

        var result = sut.CalculateTax(new Car(),
            new[]
            {
                new DateTime(2013, 4, 5, 14, 59, 0),
                new DateTime(2013, 4, 5, 15, 29, 0),
                new DateTime(2013, 4, 5, 15, 35, 0),
                new DateTime(2013, 4, 5, 15, 32, 0),
                new DateTime(2013, 4, 5, 14, 50, 0),
                new DateTime(2013, 4, 5, 15, 36, 0),
            });

        Assert.Equal(18, result);
    }

    public static IEnumerable<object[]> ValidHoursTestData =>
        new List<object[]>
        {
            new Object[] { 8, new DateTime(2013, 1, 2, 6, 0, 0) },
            new Object[] { 8, new DateTime(2013, 1, 2, 6, 29, 0) },
            new Object[] { 8, new DateTime(2013, 1, 2, 8, 30, 0) },
            new Object[] { 8, new DateTime(2013, 1, 2, 14, 59, 0) },
            new Object[] { 8, new DateTime(2013, 1, 2, 18, 0, 0) },
            new Object[] { 8, new DateTime(2013, 1, 2, 18, 29, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 6, 30, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 6, 59, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 8, 0, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 8, 29, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 15, 0, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 15, 29, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 17, 0, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 17, 59, 0) },
            new Object[] { 18, new DateTime(2013, 1, 2, 7, 0, 0) },
            new Object[] { 18, new DateTime(2013, 1, 2, 7, 59, 0) },
            new Object[] { 18, new DateTime(2013, 1, 2, 15, 30, 0) },
            new Object[] { 18, new DateTime(2013, 1, 2, 16, 59, 0) }
        };

    public static IEnumerable<object[]> ValidDatesTestData =>
        new List<object[]>
        {
            new Object[] { 8, new DateTime(2013, 1, 2, 6, 0, 0) },
            new Object[] { 8, new DateTime(2013, 1, 2, 6, 29, 0) },
            new Object[] { 8, new DateTime(2013, 1, 2, 8, 30, 0) },
            new Object[] { 8, new DateTime(2013, 1, 2, 14, 59, 0) },
            new Object[] { 8, new DateTime(2013, 1, 2, 18, 0, 0) },
            new Object[] { 8, new DateTime(2013, 1, 2, 18, 29, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 6, 30, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 6, 59, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 8, 0, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 8, 29, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 15, 0, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 15, 29, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 17, 0, 0) },
            new Object[] { 13, new DateTime(2013, 1, 2, 17, 59, 0) },
            new Object[] { 18, new DateTime(2013, 1, 2, 7, 0, 0) },
            new Object[] { 18, new DateTime(2013, 1, 2, 7, 59, 0) },
            new Object[] { 18, new DateTime(2013, 1, 2, 15, 30, 0) },
            new Object[] { 18, new DateTime(2013, 1, 2, 16, 59, 0) }
        };
}