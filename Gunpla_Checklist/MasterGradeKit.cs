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

        public MasterGradeKit(string modelName, string series, string scale)
        {
            ModelName = modelName;
            Series = series;
            Scale = scale;
        }

        // ctor used by program: (modelName, series, lineNumber)
        public MasterGradeKit(string modelName, string series, int lineNumber)
            : base(modelName, series, string.Empty)
        {
            Grade = "MG";
        }

        public override string GetDetails()
        {
            return $"Model: {ModelName}, Series: {Series}, Scale: {Scale}, Built: {IsBuilt}, Grade: [MG]";
        }
    }
}

