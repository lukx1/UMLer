using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Special
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class NoSerializeAttribute : Attribute
    {
    }
}
