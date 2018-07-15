using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UMLer.DiagramData.Attributes;
using UMLer.Special;

namespace UMLer.DiagramData
{
    [Serializable]
   public class Clazz
    {
        public AccessModifier AccessModifier { get;set; }

        public Language Language { get; set; } = Language.CSHARP;

        public List<Clazz> Extends { get; set; } = new List<Clazz>();
        public List<Clazz> Implements { get; set; } = new List<Clazz>();

        [XmlIgnore]
        public Dictionary<string, Clazz> GenericMustExtend { get; set; } = new Dictionary<string, Clazz>();

        public string Name { get; set; }
        public string FullName { get; set; }
        public string Namespace { get; set; }

        [Csharp][Cpp][C]
        public bool IsStruct { get; set; } = false;
        public bool IsStatic { get; set; } = false;
        public bool IsAbstract { get; set; } = false;
        public bool IsEnum { get; set; } = false;
        public bool IsInterface { get; set; } = false;
        public bool IsSealed { get; set; } = false;
        [Csharp]
        public bool IsPartial { get; set; } = false;
        public bool IsPrimitive { get; set; } = false;

        public bool IsNested { get; set; } = false;
        public Clazz NestedIn { get; set; } = null;

        public bool IsJustVisual { get; set; }

        public List<Method> Methods = new List<Method>();
        public List<Field> Fields = new List<Field>();

        public void StringToThis(string s)
        {
            IsStruct = false;
            IsStatic = false;
            IsAbstract = false;
            IsEnum = false;
            IsInterface = false;
            IsStatic = false;
            IsPartial = false;
            IsPrimitive = false;
            IsNested = false;
            int i = 0;
            var syms = s.Split(' ');
            foreach (var symbol in syms)
            {
                ParseSymbol(symbol,i,syms.Count());
                i++;
            }
        }

        private void ParseSymbol(string symbol,int pos,int count)
        {
            switch (symbol.ToUpper())
            {
                case "CLASS":
                    break;
                case "INTERFACE":
                    IsInterface = true;
                    break;
                case "ENUM":
                    IsEnum = true;
                    break;
                case "STATIC":
                    IsStatic = true;
                    break;
                case "SEALED":
                    IsSealed = true;
                    break;
                case "PARTIAL":
                    IsPartial = true;
                    break;
                case "ABSTRACT":
                    IsAbstract = true;
                    break;
                case "STRUCT":
                    break;
                default:
                    if (pos == count - 1)
                        this.Name = symbol;
                    else
                        throw new ArgumentException("Unexpected symbol " + symbol);
                    break;
            }
        }

        public string ClassToString()
        {
            string str = "";
            if (this.IsAbstract)
                str += "absract ";
            else if (this.IsStatic)
                str += "static ";
            else if (this.IsSealed)
                str += "sealed ";
            if (this.AccessModifier == AccessModifier.NOTHING) { }
            else
                str += this.AccessModifier.ToString().Replace('_', ' ').ToLower() + " ";
            if (this.IsEnum)
                str += "enum ";
            else if (this.IsInterface)
                str += "interface ";
            else
                str += "class ";
            str += this.Name;
            return str;
        }

        public Clazz() { }
        private Clazz(bool justVisual)
        {
            this.IsJustVisual = justVisual;
        }

        public static Clazz CreateJV(Type Type, AccessModifier accessModifier = AccessModifier.PUBLIC, string Namespace = "")
        {
            Clazz c = new Clazz(true);
            c.Name = Type.Name;
            c.Namespace = Type.Namespace;
            c.FullName = Type.FullName;
            c.Language = Language.CSHARP;
            c.IsStruct = Type.IsValueType && !Type.IsEnum;
            c.IsEnum = Type.IsEnum;
            c.IsInterface = Type.IsInterface;
            c.IsStatic = Type.IsAbstract && Type.IsSealed;
            c.IsSealed = Type.IsSealed;
            c.IsPartial = false;//Nejde detekovat;
            c.IsPrimitive = Type.IsPrimitive;
            return c;
        }

        public static Clazz CreateJV(string Name, AccessModifier accessModifier = AccessModifier.PUBLIC,string Namespace = "")
        {
            Clazz c = new Clazz(true);
            c.Name = Name;
            c.AccessModifier = accessModifier;
            c.Namespace = Namespace;
            return c;
        }

        public static readonly Clazz Empty = new Clazz();
        public static readonly Clazz Generic = new Clazz() { Name = "Generic Class" };
        public static readonly Clazz Void = new Clazz() { Name = "Void"};
        #region serialization
        public SerializeableKeyValue<string, Clazz>[] GenericMustExtendSerializable
        {
            get
            {
                var list = new List<SerializeableKeyValue<string, Clazz>>();
                if (GenericMustExtend != null)
                {
                    list.AddRange(GenericMustExtend.Keys.Select(key => new SerializeableKeyValue<string, Clazz>() { Key = key, Value = GenericMustExtend[key] }));
                }
                return list.ToArray();
            }
            set
            {
                GenericMustExtend = new Dictionary<string, Clazz>();
                foreach (var item in value)
                {
                    GenericMustExtend.Add(item.Key, item.Value);
                }
            }
        }
        #endregion
    }
}
