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
        public RealGradeKit() : base() { }

        /// <summary>Construct a Real Grade kit with line number.</summary>
        public RealGradeKit(string modelName, string series, int lineNumber)
            : base(modelName, series, "1/144")
        {
            LineNumber = lineNumber;
        }

        /// <summary>Return details including the line number.</summary>
        public override string GetDetails()
        {
            // base.GetDetails() gets the Model, Series, Scale, and Built status
            return $"{base.GetDetails()} RG Line #:{LineNumber}";
        }
    }
}
