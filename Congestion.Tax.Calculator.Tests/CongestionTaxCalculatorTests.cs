using Congestion.Tax.Calculator.Vehicles;
using Xunit;

namespace Congestion.Tax.Calculator.Tests;
//test before refactoring
// [Theory]
// [MemberData(nameof(CustomTestData))]
// public void IsTollFreeDate(DateTime dateTime)
// {
//     var sut = new CongestionTaxCalculator();
//
//     var result = sut.IsTollFreeDate(dateTime);
//
//     Assert.True(result);
// }
//
// public static IEnumerable<object[]> CustomTestData =>
//     new List<object[]>
//     {
//         new Object[] { new DateTime(2013, 1, 1) },
//         new Object[] { new DateTime(2013, 3, 28) },
//         new Object[] { new DateTime(2013, 3, 29) },
//         new Object[] { new DateTime(2013, 4, 1) },
//         new Object[] { new DateTime(2013, 4, 30) },
//         new Object[] { new DateTime(2013, 5, 1) },
//         new Object[] { new DateTime(2013, 5, 8) },
//         new Object[] { new DateTime(2013, 5, 9) },
//         new Object[] { new DateTime(2013, 6, 5) },
//         new Object[] { new DateTime(2013, 6, 6) },
//         new Object[] { new DateTime(2013, 6, 21) },
//         new Object[] { new DateTime(2013, 11, 1) },
//         new Object[] { new DateTime(2013, 12, 24) },
//         new Object[] { new DateTime(2013, 12, 25) },
//         new Object[] { new DateTime(2013, 12, 26) },
//         new Object[] { new DateTime(2013, 12, 31) }
//     };

public class CongestionTaxCalculator_GetTollFee
{
    [Theory]
    [MemberData(nameof(ValidHoursTestData))]
    public void ValidHoursForSekFee_ReturnsExpectedFee(int expectedFee, DateTime dateTime)
    {
        var sut = new CongestionTaxCalculator();

        var result = sut.GetTollFee(dateTime, new Car());

        Assert.Equal(expectedFee, result);
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
}