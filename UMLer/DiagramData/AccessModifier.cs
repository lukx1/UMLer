using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.DiagramData.Attributes;

namespace UMLer.DiagramData
{
    public enum AccessModifier
    {
        PUBLIC,
        PRIVATE,
        PROTECTED,
        [Csharp]
        PRIVATE_PROTECTED,
        [Csharp]
        INTERNAL,
        [Csharp]
        PROTECTED_INTERNAL,
        NOTHING
    }
}
