using System;
using System.Text.RegularExpressions;

namespace _06_Garage_List;

public class Vehicle
{
    private string _registrationNumber;

    public string RegistrationNumber
    {
        get { return _registrationNumber; }
        set
        {
            if (!Regex.IsMatch(value, @"\A[A-Za-z]{3}[0-9]{3}\z"))
            {
                throw new ArgumentException(
                    "Registration number must be three letters followed by 3 digits (ABC123)."
                );
            }
            _registrationNumber = value;
        }
    }
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
