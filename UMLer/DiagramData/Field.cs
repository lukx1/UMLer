using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.DiagramData.Attributes;

namespace UMLer.DiagramData
{
    public class Field
    {
        public AccessModifier AccessModifier { get; set; } = AccessModifier.PUBLIC;
        public Clazz Type { get; set; } = Clazz.Empty;
        public string Name { get; set; }

        public bool IsArray { get; set; }
        public int ArrayDimensions { get; set; } = 0;
        [Csharp]
        public bool IsEvent { get; set; } = false;
        public bool IsConst { get; set; } = false;
        [Csharp]
        public bool IsNew { get; set; } = false;
        public bool IsOverride { get; set; } = false;
        [Csharp]
        public bool IsReadonly { get; set; } = false;
        public bool IsStatic { get; set; }
        [Cpp]
        [Csharp]
        public bool IsVirtual { get; set; }
        public bool IsVolatile { get; set; }
        [Csharp]
        public bool IsProperty { get; set; }
        [Csharp]
        public bool HasGet { get; set; }
        [Csharp]
        public bool HasSet { get; set; }
        [Csharp]
        public AccessModifier GetModifier { get; set; } = AccessModifier.PUBLIC;
        [Csharp]
        public AccessModifier SetModifier { get; set; } = AccessModifier.PUBLIC;

        public void StringToThis(string s)
        {
            IsArray = false;
            ArrayDimensions = 0;
            IsEvent = false;
            IsConst = false;
            IsNew = false;
            IsOverride = false;
            IsReadonly = false;
            IsStatic = false;
            IsVirtual = false;
            IsVolatile = false;
            IsProperty = false;
            HasGet = false;
            HasSet = false;
            GetModifier = AccessModifier.PUBLIC;
            SetModifier = AccessModifier.PUBLIC;
            var symbols = s.Split(' ');
            int i = 0;
            foreach (var symbol in symbols)
            {
                ParseSymbol(symbol,i,symbols.Count());
                i++;
            }
        }

        private string FieldToString()
        {
            string s = "";

            if (this.AccessModifier == AccessModifier.NOTHING) { }
            else
                s += this.AccessModifier.ToString().Replace('_', ' ').ToLower() + " ";
            if (IsVolatile)
                s += "volatile ";
            if (IsStatic)
                s += "static ";
            if (IsConst)
                s += "const ";
            if (IsNew)
                s += "new ";
            if (IsOverride)
                s += "override ";
            else if (IsVirtual)
                s += "virtual ";
            s += Type.Name+" ";
            s += this.Name;
            return s;
        }

        private void ParseSymbol(string symbol, int pos, int count)
        {
            if(symbol.Contains('[') && symbol.Contains(']'))
            {
                this.IsArray = true;
                this.ArrayDimensions = symbol.Count(r => r == ',') + 1;
                if (pos - 1 == count)
                    this.Name = symbol.Substring(symbol.IndexOf(']'));
                return;
            }
            else if(symbol.Contains('{') && symbol.Contains('}'))
            {
                this.IsProperty = true;
                return;
            }
            switch (symbol.ToUpper())
            {
                case "EVENT":
                    this.IsEvent = true;
                    break;
                case "CONST":
                    this.IsConst = true;
                    break;
                case "NEW":
                    this.IsNew = true;
                    break;
                case "OVERRIDE":
                    this.IsOverride = true;
                    break;
                case "READONLY":
                    this.IsReadonly = true;
                    break;
                case "STATIC":
                    this.IsStatic = true;
                    break;
                case "VIRTUAL":
                    this.IsVirtual = true;
                    break;
                case "VOLATILE":
                    this.IsVolatile = true;
                    break;
                default:
                    if (pos == count - 1)
                        this.Name = symbol;
                    else
                        throw new ArgumentException("Unexpected symbol " + symbol);
                    break;
            }
        }

    }
}
