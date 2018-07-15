using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.DiagramData.Attributes
{
    public class CppAttribute : Attribute, IForLanguage
    {
        public bool IsCppOnly { get; private set; } = true;

        public CppAttribute(bool b = true)
        {
            this.IsCppOnly = b;
        }
    }
}
