using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
