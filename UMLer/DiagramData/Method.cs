using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UMLer.DiagramData.Attributes;
using UMLer.Special;

namespace UMLer.DiagramData
{
    public class Method
    {
        public AccessModifier AccessModifier { get; set; } = AccessModifier.PUBLIC;
        public Clazz ReturnType { get; set; } = Clazz.Void;
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
        [XmlIgnore]
        public Dictionary<string,Clazz> GenericMustExtend { get; set; } = new Dictionary<string,Clazz>();
        public string Name { get; set; }

        [Csharp]
        public bool IsDelegate { get; set; }
        [Csharp]
        public bool IsAsync { get; set; }
        public bool IsStatic { get; set; }
        [Csharp]
        public bool IsUnsafe { get; set; }
        public bool IsConstructor { get; set; }
        [Csharp][Cpp]
        public bool IsDestructor { get; set; }
        [Csharp]
        public bool IsExtern { get; set; }
        [Csharp]
        public bool IsAbstract { get; set; }
        public bool IsOverride { get; set; }
        [Csharp][Cpp]
        public bool IsVirtual { get; set; }
        [Java]
        public bool IsSynchronized { get; set; }
        /// <summary>
        /// Final for java
        /// </summary>
        public bool IsSealed { get; set; }

        public string MethodToString()
        {
            string s = ""; 
            if (IsConstructor)
            {
                s = this.Name + "( ";
                int i = 0;
                foreach (var Parameter in Parameters)
                {
                    if (i++ > 0)
                        s += " ";
                    s += Parameter.ParameterToString();
                }
                s += " )";
                return s;
            }
            else if (IsDestructor)
            {
                return "~" + this.Name;
            }
            if (this.AccessModifier == AccessModifier.NOTHING) { }
            else
                s += this.AccessModifier.ToString().Replace('_', ' ').ToLower() + " ";
            if (IsDelegate)
                s += "delegate ";
            if (IsStatic)
                s += "static ";
            if (IsUnsafe)
                s += "unsafe ";
            if (IsExtern)
                s += "extern ";
            if (IsAbstract)
                s += "abstract ";
            if (IsOverride)
                s += "override ";
            if (IsVirtual)
                s += "virtual ";
            if (IsSynchronized)
                s += "synchronized ";
            s += (ReturnType == Clazz.Void ? "void" : ReturnType.Name)+" ";
            s += this.Name;
            if (Parameters.Count() < 1)
                return s + "()";
            else
            {
                s += "( ";

            }
        }

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
