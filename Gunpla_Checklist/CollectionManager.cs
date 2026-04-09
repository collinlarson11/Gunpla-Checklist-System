using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gunpla_Checklist
{
    internal class CollectionManager
    {
        // Private collection per UML
        private readonly List<GunplaKit> MyKits;

        public CollectionManager()
        {
            MyKits = new List<GunplaKit>();
        }

        // AddKit(GunplaKit kit) : void
        public void AddKit(GunplaKit kit)
        {
            if (kit == null) throw new ArgumentNullException(nameof(kit));
            MyKits.Add(kit);
        }

        // DisplayChecklist() : void
        public void DisplayChecklist()
        {
            if (!MyKits.Any())
            {
                Console.WriteLine("Your collection is empty.");
                return;
            }

            Console.WriteLine("Gunpla Checklist:");
            for (int i = 0; i < MyKits.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {MyKits[i].GetDetails()}");
            }
        }

        // Named to match Program.cs calls
        // GetCollectionStats() : string
        public string GetCollectionStats()
        {
            int total = MyKits.Count;
            int built = MyKits.Count(k => k.IsBuilt);
            int unbuilt = total - built;

            var seriesBreakdown = MyKits
                .GroupBy(k => k.Series ?? string.Empty)
                .OrderBy(g => g.Key)
                .Select(g => $"{g.Key}: {g.Count()}")
                .ToArray();

            string seriesPart = seriesBreakdown.Length > 0
                ? $", By Series: {string.Join(", ", seriesBreakdown)}"
                : string.Empty;

            return $"Total: {total}, Built: {built}, Unbuilt: {unbuilt}{seriesPart}";
        }

        // TryMarkKitBuilt(int oneBasedIndex) : bool
        // Returns true if the kit was found and marked built; false otherwise.
        public bool TryMarkKitBuilt(int oneBasedIndex)
        {
            if (oneBasedIndex < 1 || oneBasedIndex > MyKits.Count) return false;
            MyKits[oneBasedIndex - 1].MarkAsBuilt();
            return true;
        }
    }
}
