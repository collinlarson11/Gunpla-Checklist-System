using System;

namespace Gunpla_Checklist
{
    /// <summary>
    /// Console entry point and simple UI for the Gunpla checklist application.
    /// Contains the menu loop and small helper workflows (add, pause).
    /// </summary>
    internal class Program
    {
        static void Main(string[] args) => ShowMenu();

        /// <summary>
        /// Main interactive menu loop. Loads data, shows options, and calls
        /// CollectionManager for business operations.
        /// </summary>
        public static void ShowMenu()
        {
            var manager = new CollectionManager();
            manager.Load(); // Load persisted collection at startup

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gunpla Checklist - Menu");
                Console.WriteLine("1) Add Kit");
                Console.WriteLine("2) Display Checklist");
                Console.WriteLine("3) Show Collection Stats");
                Console.WriteLine("4) Mark Kit as Built");
                Console.WriteLine("5) Delete a Kit"); // New Option
                Console.WriteLine("6) Exit");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddKitWorkflow(manager);
                        break;
                    case "2":
                        // Show the saved/loaded checklist
                        manager.DisplayChecklist();
                        Pause();
                        break;
                    case "3":
                        // Print a brief stats summary computed by CollectionManager
                        Console.WriteLine(manager.GetCollectionStats());
                        Pause();
                        break;
                    case "4":
                        Console.Clear();
                        var unbuiltList = manager.DisplayUnbuiltKits();

                        if (unbuiltList.Count > 0)
                        {
                            Console.Write("\nEnter the number of the kit to mark as built: ");

                            // Changed 'choice' to 'selectionIdx' to avoid the name conflict
                            if (int.TryParse(Console.ReadLine(), out int selectionIdx))
                            {
                                if (selectionIdx > 0 && selectionIdx <= unbuiltList.Count)
                                {
                                    var selectedKit = unbuiltList[selectionIdx - 1];
                                    selectedKit.MarkAsBuilt();
                                    manager.Save();
                                    Console.WriteLine($"\n'{selectedKit.ModelName}' is now marked as built!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid selection.");
                                }
                            }
                        }
                        Pause();
                        break;
                    case "5":
                        Console.Clear();
                        manager.DisplayChecklist();
                        Console.WriteLine();
                        Console.Write("Enter the number of the kit you want to DELETE: ");

                        if (int.TryParse(Console.ReadLine(), out int deleteIdx))
                        {
                            if (manager.TryDeleteKit(deleteIdx))
                                Console.WriteLine("Kit deleted successfully.");
                            else
                                Console.WriteLine("Invalid kit number.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                        }
                        Pause();
                        break;

                    case "6": // Moved Exit to 6
                        manager.Save();
                        return;
                }
            }
        }

        /// <summary>
        /// Small workflow that prompts the user for kit information and adds it to the manager.
        /// Handles different kit types (Real Grade, Master Grade, other).
        /// </summary>
        /// <param name="manager">The collection manager to receive the new kit.</param>
        private static void AddKitWorkflow(CollectionManager manager)
        {
            Console.WriteLine("\nSelect Grade: 1) Real Grade, 2) Master Grade, 3) Other");
            var type = Console.ReadLine();

            Console.Write("Model Name: ");
            var name = Console.ReadLine() ?? "Unknown";
            Console.Write("Series: ");
            var series = Console.ReadLine() ?? "Unknown";

            if (type == "1") // Real Grade - Keep the Number
            {
                Console.Write("RG Line Number: ");
                int.TryParse(Console.ReadLine(), out int line);
                manager.AddKit(new RealGradeKit(name, series, line));
            }
            else if (type == "2") // Master Grade - No Line Number, fixed scale
            {
                manager.AddKit(new MasterGradeKit(name, series));
            }
            else // Standard/Other
            {
                Console.Write("Scale (e.g., 1/144): ");
                var scale = Console.ReadLine() ?? "1/144";
                manager.AddKit(new GunplaKit(name, series, scale));
            }

            Console.WriteLine("Kit added successfully!");
            Pause();
        }

        /// <summary>
        /// Small UI helper to pause the console until the user presses Enter.
        /// </summary>
        private static void Pause()
        {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
