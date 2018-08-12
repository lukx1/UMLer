using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Paintables.LinkPainters;

namespace UMLer.DiagramData
{
    public class ClazzLink
    {
        public Clazz Start { get; set; }
        public Clazz Finish { get; set; }
        public LinkType LinkStart { get; set; }
        public LinkType LinkFinish { get; set; }
    }
}
