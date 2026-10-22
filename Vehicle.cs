using System;

namespace _06_Garage_List;

public class Vehicle
{
    public string RegistrationNumber { get; set; }
    public string Color { get; set; }
    public int NumberOfWheels { get; set; }

    public Vehicle(string registrationNumber, string color, int numberOfWheels)
    {
        RegistrationNumber = registrationNumber;
        Color = color;
        NumberOfWheels = numberOfWheels;
    }

    public override string ToString()
    {
        return $"{RegistrationNumber} - {GetType().Name}, Color: {Color}, Number of wheels: {NumberOfWheels}";
    }
}
