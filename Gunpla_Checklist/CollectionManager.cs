using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Gunpla_Checklist
{
    /// <summary>
    /// Manages an in-memory list of GunplaKit instances and performs
    /// persistence (JSON save/load) for the collection.
    /// </summary>
    internal class CollectionManager
    {
        // Backing list that stores the kits. The UI displays 1-based indices
        // corresponding to positions inside this list.
        private readonly List<GunplaKit> MyKits; // private field to hold the collection of kits

        // Path to the JSON data file used to persist the collection.
        // Using %AppData% keeps per-user data in a conventional location.
        private static string DataFilePath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                         "GunplaChecklist", "kits.json");

        public CollectionManager()
        {
            MyKits = new List<GunplaKit>();
        }

        /// <summary>
        /// Add a kit to the collection and auto-save the data to disk.
        /// Throws ArgumentNullException if kit is null.
        /// </summary>
        public void AddKit(GunplaKit kit)
        {
            if (kit == null) throw new ArgumentNullException(nameof(kit));
            MyKits.Add(kit);
            Save(); // persist immediately so the data is not lost on crash
        }

        /// <summary>
        /// Write a human-readable checklist to the console.
        /// Uses 1-based indices so the list is friendlier for users.
        /// </summary>
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

        /// <summary>
        /// Returns a compact stats summary (total, built, unbuilt and series counts).
        /// </summary>
        public string GetCollectionStats()
        {
            int total = MyKits.Count;
            int built = MyKits.Count(k => k.IsBuilt);
            int unbuilt = total - built;

            // Group by series for a quick breakdown
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

        /// <summary>
        /// Marks the kit at the given 1-based index as built and persists the change.
        /// Returns false if index is out of range.
        /// </summary>
        public bool TryMarkKitBuilt(int oneBasedIndex)
        {
            if (oneBasedIndex < 1 || oneBasedIndex > MyKits.Count) return false;

            // Convert to zero-based index and mark built
            MyKits[oneBasedIndex - 1].MarkAsBuilt();
            Save(); // persist the updated state
            return true;
        }

        /// <summary>
        /// Added a feature to delete a kit by its 1-based index. 
        /// Returns false if the index is invalid.
        /// </summary>
        /// <param name="oneBasedIndex"></param>
        /// <returns></returns>
        public bool TryDeleteKit(int oneBasedIndex)
        {
            // Validate that the number entered exists in the list
            if (oneBasedIndex < 1 || oneBasedIndex > MyKits.Count)
                return false;

            // Remove from the underlying list (adjusting for 0-based index)
            MyKits.RemoveAt(oneBasedIndex - 1);

            // Auto-save so the deletion is permanent
            Save();
            return true;
        }

        /// <summary>
        /// Save the current collection to a JSON file. Non-fatal: on error the exception
        /// is written to stderr but not thrown, so the UI can continue.
        /// </summary>
        public void Save()
        {
            try
            {
                var path = DataFilePath;
                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                // Pretty-print JSON to make manual inspection easier
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(MyKits, options);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                // Non-fatal: log to stderr. Consider adding a proper logger later.
                Console.Error.WriteLine($"Failed to save collection: {ex.Message}");
            }
        }

        /// <summary>
        /// Load the collection from disk. If the file is missing this is a no-op.
        /// On parse errors we write the error to stderr and keep the in-memory list empty.
        /// </summary>
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
                // Non-fatal: write to stderr; you can add migration or backup logic here.
                Console.Error.WriteLine($"Failed to load collection: {ex.Message}");
            }
        }
    }
}
