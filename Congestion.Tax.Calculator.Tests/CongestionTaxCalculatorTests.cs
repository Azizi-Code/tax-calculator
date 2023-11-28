using System.Collections.Immutable;
using Congestion.Tax.Calculator.Vehicles;
using Xunit;
using static Congestion.Tax.Calculator.Tests.CongestionTaxCalculatorTestHarness;

namespace Congestion.Tax.Calculator.Tests;

public class CongestionTaxCalculator_CalculateTax
{
    [Fact]
    public void NullVehicle_ThrowsException()
    {
        var sut = CreateSut();

        var result = Record.Exception(() => sut.CalculateTax(null!, DefaultNonTollFreeDates));

        Assert.IsType<ArgumentNullException>(result);
        Assert.Equal("Vehicle can't be null. (Parameter 'vehicle')", result.Message);
    }

    [Fact]
    public void NullDates_ThrowsException()
    {
        var sut = CreateSut();

        var result = Record.Exception(() => sut.CalculateTax(DefaultNonTollFreeVehicle, null!));

        Assert.IsType<ArgumentException>(result);
        Assert.Equal("Dates can't be null or empty.", result.Message);
    }

    [Fact]
    public void EmptyDates_ThrowsException()
    {
        var sut = CreateSut();

        var result = Record.Exception(() =>
            sut.CalculateTax(DefaultNonTollFreeVehicle, ImmutableArray<DateTime>.Empty.ToArray()));

        Assert.IsType<ArgumentException>(result);
        Assert.Equal("Dates can't be null or empty.", result.Message);
    }

    [Fact]
    public void DatesAreNotInTheSameDay_ThrowsException()
    {
        var sut = CreateSut();

        var result = Record.Exception(() =>
            sut.CalculateTax(DefaultNonTollFreeVehicle, new[] { new DateTime(2013, 1, 1), new DateTime(2013, 1, 3) }));

        Assert.IsType<ArgumentException>(result);
        Assert.Equal("All the date times should be in a same day.", result.Message);
    }

    [Theory]
    [MemberData(nameof(FreeTollDatesTestData))]
    public void FreeTollDatesWithNonTollFreeVehicle_ReturnsZeroToll(DateTime[] dateTime)
    {
        var sut = CreateSut();

        var result = sut.CalculateTax(DefaultNonTollFreeVehicle, dateTime);

        Assert.Equal(0, result);
    }

    [Theory]
    [MemberData(nameof(FreeTollDatesTestData))]
    public void FreeTollDatesWithTollFreeVehicle_ReturnsZeroToll(DateTime[] dateTime)
    {
        var sut = CreateSut();

        var result = sut.CalculateTax(DefaultTollFreeVehicle, dateTime);

        Assert.Equal(0, result);
    }

    [Fact]
    public void DatesWithTollFreeVehicle_ReturnsZeroToll()
    {
        var sut = CreateSut();

        var result = sut.CalculateTax(DefaultTollFreeVehicle, DefaultNonTollFreeDates);

        Assert.Equal(0, result);
    }

    [Fact]
    public void SeveralTollsIn60Minutes_ReturnsTheMaxTollFee()
    {
        var expectedResult = 18;
        var sut = CreateSut();

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

        Assert.Equal(expectedResult, result);
    }

    public static IEnumerable<object[]> FreeTollDatesTestData =>
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
}

public class CongestionTaxCalculator_GetTollFee
{
    [Theory]
    [MemberData(nameof(ValidHoursForFeeTestData))]
    public void ValidHoursForFee_ReturnsExpectedFee(int expectedFee, DateTime dateTime)
    {
        var sut = CreateSut();

        var result = sut.GetTollFee(dateTime);

        Assert.Equal(expectedFee, result);
    }

    public static IEnumerable<object[]> ValidHoursForFeeTestData =>
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

public static class CongestionTaxCalculatorTestHarness
{
    public static readonly IVehicle DefaultTollFreeVehicle = new Motorbike();
    public static readonly IVehicle DefaultNonTollFreeVehicle = new Car();

    public static readonly DateTime[] DefaultNonTollFreeDates =
        { new(2013, 1, 2, 6, 30, 0), new(2013, 1, 2, 7, 35, 0) };

    public static ITaxCalculator CreateSut() => new CongestionTaxCalculator();
}