using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.DiagramData
{
    public interface IClazz : ISyntax
    {
        Language Language { get; set; }
        AccessModifier AccessModifier { get; set; }
        string Name { get; set; }
        IList<IMethod> Methods { get; set; }
        IList<IField> Fields { get; set; }
        IList<ExtraModifier> ExtraModifiers { get; set; }
    }
}
