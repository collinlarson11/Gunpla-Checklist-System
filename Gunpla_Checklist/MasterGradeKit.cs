using System;

namespace Gunpla_Checklist
{
    /// <summary>
    /// Inherits from GunplaKit and represents a Master Grade kit. .
    /// Master Grade kit (MG). Sets Grade = "MG" automatically.
    /// </summary>
    internal class MasterGradeKit : GunplaKit
    {

        // Parameterless ctor for serializer
        public MasterGradeKit()
        {
            Grade = "MG";
        }

        
        /// <summary>
        /// Primary ctor used by the app for Master Grade kits (no line number).
        /// </summary>
        public MasterGradeKit(string modelName, string series, string scale)
            : base(modelName, series, scale)
        {
            Grade = "MG";
        }

        public override string GetDetails()
        {
            return $"Model: {ModelName}, Series: {Series}, Scale: {Scale} Built: {IsBuilt}, Grade: [MG]";
        }
    }
}

