using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UMLer.DiagramData
{
    public class Clazz : IClazz
    {
        public Language Language { get; set; } = Language.CSHARP;
        public AccessModifier AccessModifier { get; set; } = AccessModifier.PUBLIC;
        public ClassType ClassType { get; set; } = ClassType.CLASS;
        public string Name { get; set; }
        [XmlIgnore]
        public IList<IMethod> Methods { get; set; } = new List<IMethod>();
        [XmlIgnore]
        public IList<IField> Fields { get; set; } = new List<IField>();
        [XmlIgnore]
        public IList<ExtraModifier> ExtraModifiers { get; set; } = new List<ExtraModifier>();

        public static readonly Clazz Empty = new Clazz();

        public string ToSyntax()
        {
            return AccessModifier.ToString().ToLower() + ExtraModsToString(ExtraModifiers) + " class " + Name;
        }

        private static string ExtraModsToString(IList<ExtraModifier> Modifiers)
        {
            var builder = new StringBuilder();
            bool first = true;
            foreach (var item in Modifiers)
            {
                if (!first)
                    builder.Append(' ');
                builder.Append(item.ToString().ToLower());
                first = false;
            }
            return builder.ToString();
        }

        public class Field : IField
        {
            public AccessModifier AccessModifier { get; set; } = AccessModifier.PUBLIC;
            public string Name { get; set; }
            public string Type { get; set; }
            public IList<ExtraModifier> ExtraModifiers { get; set; } = new List<ExtraModifier>();

            public string ToSyntax()
            {
                return AccessModifier.ToString().ToLower() + ExtraModsToString(ExtraModifiers) + " "+Type+" " + Name;
            }
        }

        public class Method : IMethod
        {
            public AccessModifier AccessModifier { get; set; } = AccessModifier.PUBLIC;
            public string Name { get; set; }
            public string ReturnType { get; set; }
            public IEnumerable<IField> Parameters { get; set; } = new List<Field>();
            public IList<ExtraModifier> ExtraModifiers { get; set; } = new List<ExtraModifier>();

            private string ParamsToSyntax()
            {
                var builder = new StringBuilder();
                bool first = true;
                foreach (var param in Parameters)
                {
                    if (!first)
                        builder.Append(", ");
                    builder.Append(param.Type).Append(" ").Append(param.Name);
                    first = false;
                }
                return builder.ToString();
            }

            public string ToSyntax()
            {
                return AccessModifier.ToString().ToLower() + ExtraModsToString(ExtraModifiers) + " " + ReturnType + " " + Name +
                    "(" +
                    ParamsToSyntax()+
                    ")";
            }

        }
        
        #region serialization

        public List<SerField> SerFields { get => ToSerFields(); set => FromSerFields(value); }
        public List<SerMethod> SerMethods { get => ToSerMethods(); set => FromSerMethods(value); }
        public List<ExtraModifier> SerExtraModifiers { get => ExtraModifiers.ToList(); set => ExtraModifiers = value.ToList(); }

        public class SerField
        {
            public AccessModifier AccessModifier { get; set; } = AccessModifier.PUBLIC;
            public string Name { get; set; }
            public string Type { get; set; }
            public List<ExtraModifier> ExtraModifiers { get; set; } = new List<ExtraModifier>();

            public IField ToIField()
            {
                return new Clazz.Field()
                {
                    Name = this.Name,
                    AccessModifier = this.AccessModifier,
                    ExtraModifiers = this.ExtraModifiers,
                    Type = this.Type
                };
            }

            public static SerField FromIField(IField f)
            {
                var sf = new SerField()
                {
                    AccessModifier = f.AccessModifier,
                    Name = f.Name,
                    Type = f.Type,
                };
                sf.ExtraModifiers.AddRange(f.ExtraModifiers);
                return sf;
            }
        }

        private List<SerField> ToSerFields()
        {
            var sfl = new List<SerField>();
            foreach (var f in Fields)
            {
                sfl.Add(SerField.FromIField(f));
            }
            return sfl;
        }

        private void FromSerFields(List<SerField> serFields)
        {
            this.Fields.Clear();
            serFields.ForEach(r => Fields.Add(r.ToIField()));
        }

        private List<SerMethod> ToSerMethods()
        {
            var sml = new List<SerMethod>();
            foreach (var m in Methods)
            {
                sml.Add(SerMethod.FromIMethod(m));
            }
            return sml;
        }

        private void FromSerMethods(List<SerMethod> serMethods)
        {
            this.Methods.Clear();
            serMethods.ForEach(r => Methods.Add(r.ToIMethod()));
        }

        public class SerMethod
        {
            public AccessModifier AccessModifier { get; set; } = AccessModifier.PUBLIC;
            public string Name { get; set; }
            public string ReturnType { get; set; }
            public List<SerField> Parameters { get; set; } = new List<SerField>();
            public List<ExtraModifier> ExtraModifiers { get; set; } = new List<ExtraModifier>();

            public IMethod ToIMethod()
            {
                var m = new Clazz.Method
                {
                    AccessModifier = this.AccessModifier,
                    ExtraModifiers = this.ExtraModifiers,
                    Name = this.Name,
                    ReturnType = this.ReturnType,
                    
                };
                List<IField> pars = new List<IField>();
                Parameters.ForEach(r => pars.Add(r.ToIField()));
                return m;
            }

            public static SerMethod FromIMethod(IMethod imethod)
            {
                var sm = new SerMethod()
                {
                    AccessModifier = imethod.AccessModifier,
                    Name = imethod.Name,
                    ReturnType = imethod.ReturnType,

                };
                sm.ExtraModifiers.AddRange(imethod.ExtraModifiers);
                foreach (var param in imethod.Parameters)
                {
                    sm.Parameters.Add(SerField.FromIField(param));
                }
                return sm;
            }

        }
        #endregion
    }
}
