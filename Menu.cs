using System;

namespace _06_Garage_List;

public class Menu
{
    public static void PrintInstructions()
    {
        Console.WriteLine("0. Exit application");
        Console.WriteLine("1. Create new garage.");
        Console.WriteLine("2. Add new vehicle");
        Console.WriteLine("3. List all parked vehicles");
        Console.WriteLine("4. Remove vehicle");
        Console.WriteLine("5. Search for vehicle");
        Console.Write("Please input a command: ");
    }

    public static Garage? CreateGarage()
    {
        Console.Write("Please define garage size: ");
        if (int.TryParse(Console.ReadLine(), out int garageSize))
        {
            Garage garage = new Garage(garageSize);
            Console.WriteLine("Garage created.");
            return garage;
        }
        else
        {
            Console.WriteLine("Garage size can only be whole integers.");
            return null;
        }
    }

    public static void ListVehicles(Garage? garage)
    {
        if (garage != null)
        {
            foreach (Vehicle? vehicle in garage.Data)
            {
                if (vehicle != null)
                {
                    Console.WriteLine(vehicle);
                }
            }
        }
    }
}
