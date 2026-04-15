using System;

namespace Gunpla_Checklist
{
    /// <summary>
    /// Master Grade variant — similar to RealGradeKit but uses a different discriminator.
    /// </summary>
    internal class MasterGradeKit : GunplaKit
    {
        public MasterGradeKit() { }

        public MasterGradeKit(string modelName, string series, int lineNumber)
            : base(modelName, series, "1/100")
        {
        }

        public override string GetDetails()
            => $"{base.GetDetails()} [Master Grade]";
    }
}

