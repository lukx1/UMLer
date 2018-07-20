using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.DiagramData
{
    public interface IField
    {
        AccessModifier AccessModifier { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        IList<ExtraModifier> ExtraModifier { get; set; }
    }
}
