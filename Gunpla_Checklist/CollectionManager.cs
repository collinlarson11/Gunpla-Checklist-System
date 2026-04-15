using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Gunpla_Checklist
{
    internal class CollectionManager
    {
        private readonly List<GunplaKit> MyKits; // private field to hold the collection of kits

        // Data file in %AppData%\GunplaChecklist\kits.json
        private static string DataFilePath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                         "GunplaChecklist", "kits.json");

        public CollectionManager() // public constructor to initialize the collection
        {
            MyKits = new List<GunplaKit>();
        }

        public void AddKit(GunplaKit kit) 
        {
            if (kit == null) throw new ArgumentNullException(nameof(kit));
            MyKits.Add(kit);
            Save(); // auto-save after adding
        }

        public void DisplayChecklist()
        {
            if (!MyKits.Any())
            {
                Console.WriteLine("Your collection is empty.");
                return;
            }

            Console.WriteLine("Gunpla Checklist:");
            for (int i = 0; i < MyKits.Count; i++)
                Console.WriteLine($"{i + 1}. {MyKits[i].GetDetails()}");
        }

        public string GetCollectionStats()
        {
            int total = MyKits.Count;
            int built = MyKits.Count(k => k.IsBuilt);
            int unbuilt = total - built;

            var seriesBreakdown = MyKits
                .GroupBy(k => k.Series ?? string.Empty)
                .OrderBy(g => g.Key)
                .Select(g => $"{(string.IsNullOrEmpty(g.Key) ? "(Unknown)" : g.Key)}: {g.Count()}")
                .ToArray();

            string seriesPart = seriesBreakdown.Length > 0
                ? $", By Series: {string.Join(", ", seriesBreakdown)}"
                : string.Empty;

            return $"Total: {total}, Built: {built}, Unbuilt: {unbuilt}{seriesPart}";
        }

        public bool TryMarkKitBuilt(int oneBasedIndex)
        {
            if (oneBasedIndex < 1 || oneBasedIndex > MyKits.Count) return false;
            MyKits[oneBasedIndex - 1].MarkAsBuilt();
            Save(); // persist the change
            return true;
        }

        // Persist collection to disk (JSON)
        public void Save()
        {
            try
            {
                var path = DataFilePath;
                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(MyKits, options);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                // Non-fatal: write to stderr; adapt to UI/logging if needed
                Console.Error.WriteLine($"Failed to save collection: {ex.Message}");
            }
        }

        // Load collection from disk (JSON)
        public void Load()
        {
            try
            {
                var path = DataFilePath;
                if (!File.Exists(path)) return;

                var json = File.ReadAllText(path);
                var items = JsonSerializer.Deserialize<List<GunplaKit>>(json);
                MyKits.Clear();
                if (items != null) MyKits.AddRange(items);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load collection: {ex.Message}");
            }
        }
    }
}
