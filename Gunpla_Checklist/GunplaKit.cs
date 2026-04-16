using System;
using System.Text.Json.Serialization;

namespace Gunpla_Checklist
{
    /// <summary>
    /// Base class for a kit. Designed to be JSON-serializable.
    /// Subclasses (RealGradeKit, MasterGradeKit) can extend this type.
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(RealGradeKit), "RG")]
    [JsonDerivedType(typeof(MasterGradeKit), "MG")]
    internal class GunplaKit
    {
        /// <summary>Model name shown to the user.</summary>
        public string ModelName { get; set; } = string.Empty;

        /// <summary>Whether this kit is marked as built.</summary>
        public bool IsBuilt { get; set; }

        /// <summary>Series name (used for grouping/stats).</summary>
        public string Series { get; set; } = string.Empty;

        /// <summary>Scale string (e.g. "1/144", "1/100").</summary>
        public string Scale { get; set; } = string.Empty;

        /// <summary>Grade string (e.g. "RG", "MG").</summary>
        public string Grade { get; set; } = string.Empty;

        /// <summary>Parameterless ctor required by JSON serializer.</summary>
        public GunplaKit() { }

        /// <summary>Convenience ctor used by the UI to create a kit instance.</summary>
        public GunplaKit(string modelName, string series, string scale)
        {
            ModelName = modelName ?? string.Empty;
            Series = series ?? string.Empty;
            Scale = scale ?? string.Empty;
            Grade = scale switch
            {
                "1/144" => "RG",
                "1/100" => "MG",
                _ => string.Empty
            };
            IsBuilt = false;
        }

        /// <summary>Mark this kit as built.</summary>
        public void MarkAsBuilt() => IsBuilt = true;

        /// <summary>Return a human-readable details string for console display.</summary>
        public virtual string GetDetails()
        {
            // Returns the standard part of the string
            return $"Model: {ModelName}, Series: {Series}, Scale: {Scale}, Built: {IsBuilt}, Grade:{Grade}";
        }
    }
}