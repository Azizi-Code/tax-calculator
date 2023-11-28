using Congestion.Tax.Calculator.Vehicles;

namespace Congestion.Tax.Calculator;

public class CongestionTaxCalculator
{
    const int MaxFeePerDay = 60;
    private const int IntervalTimeoutInMinutes = 60;

    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */
    public int CalculateTax(IVehicle vehicle, DateTime[] dates)
    {
        ValidateInputParameters(vehicle, dates);

        if (IsTollFreeDate(dates[0]) || IsTollFreeVehicle(vehicle))
            return 0;

        dates = dates.OrderBy(x => x.Date).ToArray();

        DateTime intervalStart = dates[0];
        DateTime intervalMaxFee = dates[0];

        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date);
            int tempFee = GetTollFee(intervalMaxFee);

            var timeDifferenceInMinutes = (date - intervalStart).TotalMinutes;
            if (timeDifferenceInMinutes <= IntervalTimeoutInMinutes)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee)
                {
                    tempFee = nextFee;
                    intervalMaxFee = date;
                }

                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
                intervalStart = date;
                intervalMaxFee = date;
            }

            if (totalFee > MaxFeePerDay)
                return MaxFeePerDay;
        }

        return totalFee;
    }


    public int GetTollFee(DateTime date)
    {
        int hour = date.Hour;
        int minute = date.Minute;

        if ((hour == 6 && minute >= 0 && minute <= 29) ||
            (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) ||
            (hour == 18 && minute >= 0 && minute <= 29))
            return 8;

        if ((hour == 6 && minute >= 30 && minute <= 59) ||
            (hour == 8 && minute >= 0 && minute <= 29) ||
            (hour == 15 && minute >= 0 && minute <= 29) ||
            (hour == 17 && minute >= 0 && minute <= 59))
            return 13;

        if ((hour == 7 && minute >= 0 && minute <= 59) ||
            hour == 15 && minute >= 30 ||
            hour == 16 && minute <= 59)
            return 18;

        return 0;
    }

    private static void ValidateInputParameters(IVehicle vehicle, DateTime[] dates)
    {
        if (dates == null || dates.Length == 0)
            throw new ArgumentException("Dates can't be null or empty.");

        if (vehicle == null)
            throw new ArgumentNullException(nameof(vehicle), "Vehicle can't be null.");

        var firstDate = dates.First().Date;
        var isAllDatesAreInASameDay = dates.All(x => x.Date == firstDate);
        if (!isAllDatesAreInASameDay) throw new ArgumentException("All the date times should be in a same day.");
    }

    private static bool IsTollFreeVehicle(IVehicle vehicle) =>
        Enum.IsDefined(typeof(TollFreeVehicles), vehicle.GetVehicleType().ToString());

    private static bool IsTollFreeDate(DateTime dateTime)
    {
        if (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.Month == 7) return true;

        var vacations = new HashSet<DateTime>
        {
            new(2013, 1, 1), new(2013, 3, 28), new(2013, 3, 29), new(2013, 4, 1),
            new(2013, 4, 30), new(2013, 5, 1), new(2013, 5, 8), new(2013, 5, 9),
            new(2013, 6, 5), new(2013, 6, 6), new(2013, 6, 21), new(2013, 11, 1),
            new(2013, 12, 24), new(2013, 12, 25), new(2013, 12, 26), new(2013, 12, 31)
        };
        return vacations.Contains(dateTime.Date);
    }

    private enum TollFreeVehicles
    {
        Motorcycle = VehicleType.Motorcycle,
        Bus = VehicleType.Bus,
        Emergency = VehicleType.Emergency,
        Diplomat = VehicleType.Diplomat,
        Foreign = VehicleType.Foreign,
        Military = VehicleType.Military
    }
}