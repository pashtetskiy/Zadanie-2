using SampleHierarchies.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{
    // class that describes the lines on the screen
    public class ScreenDefinition
    {
        public Dictionary<LineEntryEnums, Line> Lines { get; set; } = new Dictionary<LineEntryEnums, Line>();
    }
}
