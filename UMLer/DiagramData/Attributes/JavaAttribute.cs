using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.DiagramData.Attributes
{
    public class JavaAttribute : Attribute, IForLanguage
    {
        public bool IsJavaOnly { get; private set; } = true;

        public JavaAttribute(bool b = true)
        {
            this.IsJavaOnly = b;
        }
    }
}
