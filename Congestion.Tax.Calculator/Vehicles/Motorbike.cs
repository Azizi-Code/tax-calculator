namespace Congestion.Tax.Calculator.Vehicles;

public class Motorbike : IVehicle
{
    public VehicleType GetVehicleType()
    {
        return VehicleType.Motorcycle;
    }
}