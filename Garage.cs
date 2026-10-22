using System;

namespace _06_Garage_List;

public class Garage
{
    public List<Vehicle> Data { get; set; }
    public int Capacity { get; set; }

    public Garage(int capacity)
    {
        Capacity = capacity;
        Data = new List<Vehicle>(capacity);
    }

    /*public void AddVehicle(Vehicle vehicle)
    {
        // Find the first index in Data that is empty
        int emptyIndex = Array.IndexOf(Data, null);
        // If there is an empty index in Data and the registration number is not used by another vehicle, set empty index to provided vehicle
        if (emptyIndex != -1)
        {
            if (FindVehicleByRegistrationNumber(vehicle.RegistrationNumber, out int index) == null)
            {
                Data[emptyIndex] = vehicle;
            }
            else
            {
                throw new InvalidOperationException(
                    "Error: Vehicle with that registration number already exists"
                );
            }
        }
        else
        {
            throw new InvalidOperationException("Error: No free space in garage.");
        }
    }*/

    public void AddVehicle(Vehicle vehicle)
    {
        // Because Lists do not have a fixed size, the method must now check if Data.Count and Capacity are equal to see if the garage is full.
        if (Data.Count == Capacity)
        {
            throw new InvalidOperationException("Error: No free space in the garage.");
        }
        else if (FindVehicleByRegistrationNumber(vehicle.RegistrationNumber, out _) is not null)
        {
            throw new InvalidOperationException(
                "Error: Vehicle with that registration number already exists."
            );
        }
        Data.Add(vehicle);
    }

    // Returns true if operation is successful and false if not
    /*public bool RemoveVehicle(string registrationNumber)
    {
        FindVehicleByRegistrationNumber(registrationNumber, out int index);
        if (index != -1)
        {
            Data[index] = null;
            return true;
        }
        else
        {
            return false;
        }
    }*/

    //Changed return type of RemoveVehicle() to be more consequential with AddVehicle()
    public void RemoveVehicle(string registrationNumber)
    {
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            throw new ArgumentException("Error: Registration number must be filled in.");
        }
        if (FindVehicleByRegistrationNumber(registrationNumber, out int index) is null)
        {
            throw new InvalidOperationException(
                $"Error: Vehicle with registration number {registrationNumber} could not be found."
            );
        }
        Data.RemoveAt(index);
    }

    // public Vehicle? FindVehicleByRegistrationNumber(string registrationNumber, out int index)
    // {
    //     index = -1;
    //     for (int i = 0; i < Data.Length; i++)
    //     {
    //         Vehicle? vehicle = Data[i];
    //         if (
    //             vehicle != null
    //             && vehicle.RegistrationNumber.Equals(
    //                 registrationNumber,
    //                 StringComparison.OrdinalIgnoreCase
    //             )
    //         )
    //         {
    //             index = i;
    //             return vehicle;
    //         }
    //     }
    //     return null;
    // }

    public Vehicle? FindVehicleByRegistrationNumber(string registrationNumber, out int index)
    {
        index = Data.FindIndex(vehicle => vehicle.RegistrationNumber == registrationNumber);
        return index <= 0 ? null : Data[index];
    }

    // Takes a string of search queries separated by spaces and formatted as key-value pairs.
    // Loops through each vehicle in garage and each key-value search to identify if the vehicle is a match
    // If it is a match add it to a new array which is returned by the funciton.
    public Vehicle[] Search(string search)
    {
        string[] searchList = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] searchKeys = new string[searchList.Length];
        string[] searchValues = new string[searchList.Length];

        // Loop through searchList and validate that they are in the correct format
        for (int i = 0; i < searchList.Length; i++)
        {
            int dashCheck = searchList[i].IndexOf('-');
            if (dashCheck <= 0 || dashCheck == searchList[i].Length - 1)
            {
                throw new ArgumentException(
                    $"Error: Invalid search '{searchList[i]}'. Please use key-value format."
                );
            }

            // Separate keys and values into separate arrays.
            searchKeys[i] = searchList[i].Substring(0, dashCheck).ToLower();
            searchValues[i] = searchList[i].Substring(dashCheck + 1).ToLower();

            switch (searchKeys[i])
            {
                case "registration":
                case "color":
                case "type":
                    break;
                case "wheels":
                    // Don't need the actual number of wheels for now, just checking to see if it is a valid int.
                    if (!int.TryParse(searchValues[i], out _))
                    {
                        throw new ArgumentException(
                            $"Error: '{searchValues[i]}' is not a valid number of wheels."
                        );
                    }
                    break;
                default:
                    throw new ArgumentException($"Error: Unknown search term {searchKeys[i]}");
            }
        }

        // Loop through Data and add them to searchMatches if they match all search parameters.
        // Create new vehicle array to store search matches.
        Vehicle[] searchMatches = new Vehicle[Data.Length];
        int matchCount = 0;

        for (int i = 0; i < Data.Length; i++)
        {
            if (Data[i] == null)
                continue;

            bool isMatch = true;
            for (int j = 0; j < searchKeys.Length && isMatch; j++)
            {
                switch (searchKeys[j])
                {
                    case "registration":
                        isMatch = string.Equals(
                            Data[i].RegistrationNumber,
                            searchValues[j],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                    case "color":
                        isMatch = string.Equals(
                            Data[i].Color,
                            searchValues[j],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                    case "wheels":
                        isMatch = Data[i].NumberOfWheels == int.Parse(searchValues[j]);
                        break;
                    case "type":
                        isMatch = string.Equals(
                            Data[i].GetType().Name,
                            searchValues[j],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                }
            }

            if (isMatch)
            {
                searchMatches[matchCount] = Data[i];
                matchCount++;
            }
        }

        // Remove empty slots in searchMatches array
        Array.Resize(ref searchMatches, matchCount);
        return searchMatches;
    }
}
