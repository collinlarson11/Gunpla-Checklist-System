using System;

namespace Gunpla_Checklist
{
    internal class Program
    {
        static void Main(string[] args) => ShowMenu();

        public static void ShowMenu()
        {
            var manager = new CollectionManager();
            manager.Load(); // load persisted data at startup

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gunpla Checklist - Menu");
                Console.WriteLine("1) Add Kit");
                Console.WriteLine("2) Display Checklist");
                Console.WriteLine("3) Show Collection Stats");
                Console.WriteLine("4) Mark Kit as Built");
                Console.WriteLine("5) Exit");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        Console.Write("Model name: ");
                        var model = Console.ReadLine()?.Trim() ?? string.Empty;
                        Console.Write("Series: ");
                        var series = Console.ReadLine()?.Trim() ?? string.Empty;
                        Console.Write("Scale: ");
                        var scale = Console.ReadLine()?.Trim() ?? string.Empty;

                        var kit = new GunplaKit(model, series, scale);
                        manager.AddKit(kit);
                        Console.WriteLine();
                        Console.WriteLine("Added: " + kit.GetDetails());
                        Pause();
                        break;

                    case "2":
                        Console.WriteLine();
                        manager.DisplayChecklist();
                        Pause();
                        break;

                    case "3":
                        Console.WriteLine();
                        Console.WriteLine(manager.GetCollectionStats());
                        Pause();
                        break;

                    case "4":
                        Console.WriteLine();
                        manager.DisplayChecklist();
                        Console.WriteLine();
                        Console.Write("Enter kit number to mark as built: ");
                        var input = Console.ReadLine()?.Trim();
                        if (int.TryParse(input, out int idx))
                        {
                            if (manager.TryMarkKitBuilt(idx))
                                Console.WriteLine($"Marked kit #{idx} as built.");
                            else
                                Console.WriteLine("Invalid kit number.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        Pause();
                        break;

                    case "5":
                        manager.Save(); // ensure persisted on exit
                        ExitProgram();
                        return;

                    default:
                        Console.WriteLine("Unknown selection.");
                        Pause();
                        break;
                }
            }
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void ExitProgram()
        {
            Console.WriteLine();
            Console.WriteLine("Exiting application. Goodbye.");
            Environment.Exit(0);
        }
    }
}
