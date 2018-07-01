using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Special
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IncludeInToolBoxAttribute : Attribute
    {
        public bool Include { get; private set; }

        public IncludeInToolBoxAttribute(bool b)
        {
            this.Include = b;
        }
    }
}
