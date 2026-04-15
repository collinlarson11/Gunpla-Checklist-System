using System;

namespace Gunpla_Checklist
{
    // Inherit from GunplaKit
    internal class MasterGradeKit : GunplaKit
    {
        // Removed LineNumber property entirely 

        public MasterGradeKit() : base() { }

        // The constructor now ONLY takes name and series. 
        // We pass "1/100" to the base class automatically.
        public MasterGradeKit(string modelName, string series)
            : base(modelName, series, "1/100")
        {
        }

        public override string GetDetails() =>
            $"{base.GetDetails()} [Master Grade]";
    }
}

