using System;

namespace _06_Garage_List;

public class Menu
{
    public static void printInstructions()
    {
        Console.WriteLine("0. Exit application");
        Console.WriteLine("1. Create new garage.");
        Console.WriteLine("2. Add new vehicle");
        Console.WriteLine("3. List all parked vehicles");
        Console.WriteLine("4. Remove vehicle");
        Console.WriteLine("5. Search for vehicle");
        Console.Write("Please input a command: ");
    }
}
