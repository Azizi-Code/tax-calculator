using Congestion.Tax.Calculator.Vehicles;

namespace Congestion.Tax.Calculator;

public interface ITaxCalculator
{
    int CalculateTax(IVehicle vehicle, DateTime[] dates);
    int GetTollFee(DateTime date);
}