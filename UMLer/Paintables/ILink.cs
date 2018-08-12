using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Paintables.LinkPainters;

namespace UMLer.Paintables
{
    public interface ILink 
    {
        IPaintable Start { get; set; }
        int StartID { get; set; }
        IPaintable Finish { get; set; }
        int FinishID { get; set; }
        double AngleStart { get; set; }
        double AngleFinish { get; set; }
        LinkType LinkTypeStart { get; set; }
        LinkType LinkTypeFinish { get; set; }
    }
}
