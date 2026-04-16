using System;

namespace Gunpla_Checklist
{
    /// <summary>
    /// Inherits from GunplaKit and represents a Real Grade kit. Adds LineNumber property.
    /// Real Grade kit (RG). Sets Grade = "RG" automatically.
    /// </summary>
    internal class RealGradeKit : GunplaKit
    {
        public int LineNumber { get; set; }

        // Parameterless ctor for serializer
        public RealGradeKit()
        {
            Grade = "RG";
        }

        // ctor used by program: (modelName, series, lineNumber)
        public RealGradeKit(string modelName, string series, int lineNumber, string scale)
            : base(modelName, series, scale) 
        {
            LineNumber = lineNumber;
            Grade = "RG";
        }

        public override string GetDetails()
        {
            return $"Model: {ModelName}, Series: {Series}, Scale: {Scale}, Line#: {LineNumber}, Built: {IsBuilt}, Grade: [RG]";
        }
    }
}
