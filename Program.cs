namespace _06_Garage_List;

class Program
{
    static void Main(string[] args)
    {
        Garage? garage = null;
        bool running = true;

        Console.WriteLine("Hello, World!");
        while (running == true)
        {
            Menu.PrintInstructions();
            string? cmd = Console.ReadLine();
            switch (cmd)
            {
                case "0":
                {
                    running = false;
                    Console.WriteLine("Exiting application.");
                    break;
                }
                case "1":
                {
                    garage = Menu.CreateGarage();
                    break;
                }
                case "2":
                {
                    if (garage == null)
                    {
                        Console.WriteLine("Error: Garage has not been initialized.");
                    }
                    else
                    {
                        Console.WriteLine("1. Car");
                        Console.WriteLine("2. Motorcycle");
                        Console.WriteLine("3. Bus");
                        Console.WriteLine("4. Boat");
                        Console.WriteLine("5. Airplane");
                        Console.WriteLine("6. Other");
                        Console.Write("Please input vehicle type: ");
                        string? vehicleType = Console.ReadLine();
                        Console.Write("Please input registration number: ");
                        string? registrationNumber = Console.ReadLine();
                        Console.Write("Please input color: ");
                        string? color = Console.ReadLine();
                        Console.Write("Please input number of wheels: ");
                        if (!int.TryParse(Console.ReadLine(), out int numberOfWheels))
                        {
                            Console.WriteLine("Error: Number of wheels must be a whole integer");
                            break;
                        }
                        else if (registrationNumber == null || color == null)
                        {
                            Console.WriteLine(
                                "Error: registration number or color can not be null."
                            );
                            break;
                        }
                        else
                        {
                            try
                            {
                                switch (vehicleType)
                                {
                                    case "1":
                                        Console.Write("Please input fuel type: ");
                                        string? fuelType = Console.ReadLine();
                                        if (fuelType != null)
                                        {
                                            garage.AddVehicle(
                                                new Car(
                                                    registrationNumber,
                                                    color,
                                                    numberOfWheels,
                                                    fuelType
                                                )
                                            );
                                        }
                                        else
                                        {
                                            Console.WriteLine(
                                                "Error: Fuel type type can not be null."
                                            );
                                        }
                                        break;
                                    case "2":
                                        Console.Write(
                                            "Does the motorcycle have a sidecar attached?(y/n): "
                                        );
                                        string? hasSidecar = Console.ReadLine();
                                        switch (hasSidecar)
                                        {
                                            case "y":
                                                garage.AddVehicle(
                                                    new Motorcycle(
                                                        registrationNumber,
                                                        color,
                                                        numberOfWheels,
                                                        true
                                                    )
                                                );
                                                break;
                                            case "n":
                                                garage.AddVehicle(
                                                    new Motorcycle(
                                                        registrationNumber,
                                                        color,
                                                        numberOfWheels,
                                                        false
                                                    )
                                                );
                                                break;
                                            default:
                                                Console.WriteLine(
                                                    "You must specify whether the motorcycle has a sidecar by writing [y] or [n]."
                                                );
                                                break;
                                        }
                                        break;
                                    case "3":
                                        Console.Write("Please input number of seats: ");
                                        if (int.TryParse(Console.ReadLine(), out int numberOfSeats))
                                        {
                                            garage.AddVehicle(
                                                new Bus(
                                                    registrationNumber,
                                                    color,
                                                    numberOfWheels,
                                                    numberOfSeats
                                                )
                                            );
                                        }
                                        else
                                        {
                                            Console.WriteLine(
                                                "Error: Number of seats must be a whole integer."
                                            );
                                        }
                                        break;
                                    case "4":
                                        Console.Write("Please input length in meters: ");
                                        if (double.TryParse(Console.ReadLine(), out double length))
                                        {
                                            garage.AddVehicle(
                                                new Boat(
                                                    registrationNumber,
                                                    color,
                                                    numberOfWheels,
                                                    length
                                                )
                                            );
                                        }
                                        else
                                        {
                                            Console.WriteLine(
                                                "Error: Please input a valid length."
                                            );
                                        }
                                        break;
                                    case "5":
                                        Console.Write("Please input number of engines: ");
                                        if (
                                            int.TryParse(
                                                Console.ReadLine(),
                                                out int numberOfEngines
                                            )
                                        )
                                        {
                                            garage.AddVehicle(
                                                new Airplane(
                                                    registrationNumber,
                                                    color,
                                                    numberOfWheels,
                                                    numberOfEngines
                                                )
                                            );
                                        }
                                        else
                                        {
                                            Console.WriteLine(
                                                "Error: Number of engines must be a whole integer."
                                            );
                                        }
                                        break;
                                    case "6":
                                        garage.AddVehicle(
                                            new Vehicle(registrationNumber, color, numberOfWheels)
                                        );
                                        break;
                                    default:
                                        Console.WriteLine(
                                            "Error: We did not understand your command. Please select from the available options."
                                        );
                                        break;
                                }
                            }
                            catch (InvalidOperationException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    break;
                }
                case "3":
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
                    break;
                case "4":
                {
                    Console.Write(
                        "Please provide the registration number for the vehicle you would like to remove: "
                    );
                    string? registrationNumber = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(registrationNumber) && garage != null)
                    {
                        try
                        {
                            garage.RemoveVehicle(registrationNumber);
                            Console.WriteLine(
                                $"Vehicle '{registrationNumber}' succesfully removed."
                            );
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Please ensure garage is initialized and registration number are provided correctly."
                        );
                    }
                    break;
                }
                case "5":
                {
                    Console.WriteLine(
                        "To search for a vehicle, input a property followed by a dash and your search."
                    );
                    Console.WriteLine(
                        "You can use multiple properties together by separating them with a space."
                    );
                    Console.WriteLine("Ex. registration-abc123 color-red wheels-2 type-motorcycle");
                    Console.WriteLine("Please enter a search");
                    string? search = Console.ReadLine();
                    bool matchFound = false;
                    if (search != null && garage != null)
                    {
                        try
                        {
                            foreach (Vehicle vehicle in garage.Search(search))
                            {
                                Console.WriteLine(vehicle);
                                matchFound = true;
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Ensure garage is initialized and search is not null"
                        );
                    }
                    if (matchFound == false)
                    {
                        Console.WriteLine("No matches found.");
                    }
                    break;
                }
            }
        }
    }
}
