using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.DiagramData.Attributes
{
    public class CAttribute : Attribute, IForLanguage
    {
        public bool IsCOnly { get; private set; } = true;

        public CAttribute(bool b = true)
        {
            this.IsCOnly = b;
        }
    }
}
