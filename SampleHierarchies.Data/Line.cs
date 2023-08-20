using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{

    // class that describes the lines on the screen

    public class Line
    {
        public List<ScreenLineEntry> LineEntries { get; set; } = new List<ScreenLineEntry>();
    }
}
