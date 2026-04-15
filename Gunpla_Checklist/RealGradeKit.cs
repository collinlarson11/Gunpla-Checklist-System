using System;

namespace Gunpla_Checklist
{
    /// <summary>
    /// Real Grade variant — includes a line number in addition to common kit properties.
    /// Inherits from GunplaKit so it can be persisted alongside other kits.
    /// </summary>
    internal class RealGradeKit : GunplaKit
    {
        /// <summary>Manufacturing line or kit number (optional numeric identifier).</summary>
        public int LineNumber { get; set; }

        /// <summary>Parameterless ctor required by serializer.</summary>
        public RealGradeKit() { }

        /// <summary>Construct a Real Grade kit with line number.</summary>
        public RealGradeKit(string modelName, string series, int lineNumber)
            : base(modelName, series, "RG") // use "RG" or leave Scale empty if preferred
        {
            LineNumber = lineNumber;
        }

        /// <summary>Return details including the line number.</summary>
        public override string GetDetails()
            => $"RG - Model: {ModelName}, Series: {Series}, Line#: {LineNumber}, Built: {IsBuilt}";
    }
}
