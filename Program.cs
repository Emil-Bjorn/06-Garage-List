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
                    Menu.AddVehicle(garage);
                    break;
                }
                case "3":
                    Menu.ListVehicles(garage);
                    break;
                case "4":
                {
                    Menu.RemoveVehicle(garage);
                    break;
                }
                case "5":
                {
                    Menu.SearchVehicle(garage);
                    break;
                }
            }
        }
    }
}
