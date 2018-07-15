using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.DiagramData
{
    public class Parameter
    {
        public Clazz Class { get; set; }
        public string Name { get; set; }

        public bool IsGeneric { get; set; }
        public bool IsRef { get; set; }
        public bool IsParams { get; set; }
        public bool IsArray { get; set; }
        public int ArrayDimensions { get; set; } = -1;
        public bool IsOut { get; set; }

        public void StringToThis()
        {

        }

        private void ParseSymbol(string symbol, int pos,int count)
        {
            if (symbol.Contains('[') && symbol.Contains(']'))
            {
                this.IsArray = true;
                this.ArrayDimensions = symbol.Count(r => r == ',') + 1;
                if (pos - 1 == count)
                    this.Name = symbol.Substring(symbol.IndexOf(']'));
                return;
            }
            switch (symbol.ToUpper())
            {
                case "OUT":
                    IsOut = true;
                    break;
                case "REF":
                    IsRef = true;
                    break;
                case "PARAMS":
                    IsParams = true;
                    break;
                default:
                    if (pos == count - 1)
                        this.Name = symbol;
                    else
                        throw new ArgumentException("Unexpected symbol " + symbol);
                    break;
            }
        }

        public string ParameterToString()
        {
            string s = "";
            if (IsParams)
                s += "params ";
            if (IsOut)
                s += "out ";
            if (IsRef)
                s += "ref ";
            s += this.Class.Name+" ";
            s += this.Name;
            if (IsArray)
            {
                s += "[";
                for (int i = 0; i < ArrayDimensions-1; i++)
                {
                    s += ",";
                }
                s += "]";
            }
            return s;
        }

    }
}
