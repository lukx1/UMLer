using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.DiagramData
{
    public class Clazz : IClazz
    {
        public Language Language { get; set; } = Language.CSHARP;
        public AccessModifier AccessModifier { get; set; } = AccessModifier.PUBLIC;
        public string Name { get; set; }
        public IList<IMethod> Methods { get; set; } = new List<IMethod>();
        public IList<IField> Fields { get; set; } = new List<IField>();
        public IList<ExtraModifier> ExtraModifier { get; set; } = new List<ExtraModifier>();

        public static readonly Clazz Empty = new Clazz();

        public override string ToString()
        {
            return AccessModifier.ToString().ToLower() + " class " + Name;
        }

        public class Field : IField
        {
            public AccessModifier AccessModifier { get; set; } = AccessModifier.PUBLIC;
            public string Name { get; set; }
            public string Type { get; set; }
            public IList<ExtraModifier> ExtraModifier { get; set; } = new List<ExtraModifier>();
        }

        public class Method : IMethod
        {
            public AccessModifier AccessModifier { get; set; } = AccessModifier.PUBLIC;
            public string Name { get; set; }
            public string ReturnType { get; set; }
            public IEnumerable<IField> Parameters { get; set; } = new List<Field>();
            public IList<ExtraModifier> ExtraModifier { get; set; } = new List<ExtraModifier>();

        }
    }
}
