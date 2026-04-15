using System;

namespace Gunpla_Checklist
{
    internal class GunplaKit
    {
        // Make properties serializable (public getters + setters)
        public string ModelName { get; set; } = string.Empty; // private model name
        public bool IsBuilt { get; set; } // public readable build status

        // public properties for series and scale to allow filtering
        public string Series { get; set; } = string.Empty;
        public string Scale { get; set; } = string.Empty;

        // Parameterless constructor required by many serializers
        public GunplaKit() { }

        // Primary constructor used by the app
        public GunplaKit(string modelName, string series, string scale)
        {
            ModelName = modelName ?? string.Empty;
            Series = series ?? string.Empty;
            Scale = scale ?? string.Empty;
            IsBuilt = false;
        }

        // mark kit as built
        public void MarkAsBuilt() => IsBuilt = true;

        // return the details of the kit
        public string GetDetails() =>
            $"Model: {ModelName}, Series: {Series}, Scale: {Scale}, Built: {IsBuilt}";
    }
}
