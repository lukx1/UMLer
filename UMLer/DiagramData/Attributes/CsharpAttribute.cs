using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.DiagramData.Attributes
{
    public class CsharpAttribute : Attribute, IForLanguage
    {
        public bool IsCsharpOnly { get; private set; } = true;

        public CsharpAttribute(bool b = true)
        {
            this.IsCsharpOnly = true;
        }
    }
}
