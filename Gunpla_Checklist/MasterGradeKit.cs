using System;

namespace Gunpla_Checklist
{
    /// <summary>
    /// Master Grade variant — similar to RealGradeKit but uses a different discriminator.
    /// </summary>
    internal class MasterGradeKit : GunplaKit
    {
        /// <summary>Optional numeric identifier for master-grade kits.</summary>
        public int LineNumber { get; set; }

        public MasterGradeKit() { }

        public MasterGradeKit(string modelName, string series, int lineNumber)
            : base(modelName, series, "MG")
        {
            LineNumber = lineNumber;
        }

        public override string GetDetails()
            => $"MG - Model: {ModelName}, Series: {Series}, Line#: {LineNumber}, Built: {IsBuilt}";
    }
}

