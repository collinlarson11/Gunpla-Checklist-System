using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gunpla_Checklist
{
    internal class MasterGradeKit
    {
        // Private Field per UML diagram
        private int LineNumber { get; }

        // Public property per UML diagram
        public string Scale { get; set; }

        // Constructor to initialize the LineNumber and Scale
        public MasterGradeKit(int lineNumber, string scale)
        {
            LineNumber = lineNumber;
            Scale = scale ?? string.Empty;
        }

        // Public method per UML diagram to return kit details
        public string GetDetails()
        {
            return $"Line Number: {LineNumber}, Scale: {Scale}";
        }
    }
}

