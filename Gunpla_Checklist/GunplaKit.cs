using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gunpla_Checklist
{
    internal class GunplaKit
    {
        // Private model name per UML
        private string ModelName { get; }

        // Public readable built flag, writable only by the class
        public bool IsBuilt { get; private set; }

        // Public properties per UML
        public string Series { get; }
        public string Scale { get; }

        // Constructor now includes scale
        public GunplaKit(string modelName, string series, string scale)
        {
            ModelName = modelName ?? string.Empty;
            Series = series ?? string.Empty;
            Scale = scale ?? string.Empty;
            IsBuilt = false;
        }

        // Mark kit built
        public void MarkAsBuilt()
        {
            IsBuilt = true;
        }

        // Return details including scale
        public string GetDetails()
        {
            return $"Model: {ModelName}, Series: {Series}, Scale: {Scale}, Built: {IsBuilt}";
        }
    }
}
