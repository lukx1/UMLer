using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.DiagramData
{
    public interface IMethod : ISyntax
    {
        AccessModifier AccessModifier { get; set; }
        string Name { get; set; }
        string ReturnType { get; set; }
        IEnumerable<IField> Parameters { get; set; }
        IList<ExtraModifier> ExtraModifiers { get; set; }

        string ParametersToSyntax();
    }
}
