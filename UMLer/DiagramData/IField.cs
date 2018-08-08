using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.DiagramData
{
    public interface IField : ISyntax
    {
        AccessModifier AccessModifier { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        List<ExtraModifier> ExtraModifiers { get; set; }
    }
}
