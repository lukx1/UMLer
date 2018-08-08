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
        ClassType ClassType { get; set; }
        string Name { get; set; }
        List<Clazz.Method> Methods { get; set; }
        List<Clazz.Field> Fields { get; set; }
        List<ExtraModifier> ExtraModifiers { get; set; }
    }
}
