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

    public Vehicle? FindVehicleByRegistrationNumber(string registrationNumber, out int index)
    {
        index = Data.FindIndex(vehicle => vehicle.RegistrationNumber == registrationNumber);
        return index < 0 ? null : Data[index];
    }

    private (List<string>, List<string>) ValidateSearchString(string search)
    {
        List<string> searchList = [.. search.Split(' ', StringSplitOptions.RemoveEmptyEntries)];
        List<string> searchKeys = [];
        List<string> searchValues = [];

        foreach (string query in searchList)
        {
            // Search queries must include a dash that separates a search key and value
            int dashIndex = query.IndexOf('-');
            if (dashIndex <= 0 || dashIndex == query.Length - 1)
            {
                throw new ArgumentException(
                    $"Invalid search '{query}'. Please use key-value format."
                );
            }

            string searchKey = query.Substring(0, dashIndex);
            string searchValue = query.Substring(dashIndex + 1);

            switch (searchKey)
            {
                case "registration":
                case "color":
                case "type":
                    break;
                case "wheels":
                    if (!int.TryParse(searchValue, out _))
                    {
                        throw new ArgumentException(
                            "Error: Number of wheels must be a whole integer."
                        );
                    }
                    break;
                default:
                    throw new ArgumentException(
                        $"Error: Search key '{searchKey}' is not a valid key.'"
                    );
            }
            searchKeys.Add(searchKey);
            searchValues.Add(searchValue);
        }
        return (searchKeys, searchValues);
    }

    public List<Vehicle> Search(string search)
    {
        List<Vehicle> matches = [];
        (List<string> validatedSearchKeys, List<string> validatedSearchValues) =
            ValidateSearchString(search);
        if (validatedSearchKeys.Count <= 0 && validatedSearchValues.Count <= 0)
        {
            throw new ArgumentException("Error: No valid search queries");
        }
        foreach (Vehicle vehicle in Data)
        {
            var isMatch = true;
            for (int i = 0; i < validatedSearchKeys.Count && isMatch is true; i++)
            {
                switch (validatedSearchKeys[i])
                {
                    case "registration":
                        isMatch = string.Equals(
                            vehicle.RegistrationNumber,
                            validatedSearchValues[i],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                    case "color":
                        isMatch = string.Equals(
                            vehicle.Color,
                            validatedSearchValues[i],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                    case "type":
                        isMatch = string.Equals(
                            vehicle.GetType().Name,
                            validatedSearchValues[i],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                    case "wheels":
                        isMatch = string.Equals(
                            vehicle.NumberOfWheels.ToString(),
                            validatedSearchValues[i],
                            StringComparison.OrdinalIgnoreCase
                        );
                        break;
                }
            }
            if (isMatch)
            {
                matches.Add(vehicle);
            }
        }
        return matches;
    }
}
