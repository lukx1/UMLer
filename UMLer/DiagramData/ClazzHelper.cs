using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UMLer.Special;

namespace UMLer.DiagramData
{
    public class ClazzHelper
    {
        /// <summary>
        /// Valid name regex
        /// </summary>
        private static readonly string VNRX = "@?[a-zA-Z0-9_]+";

        private static readonly string ClassSyntaxRegXBest = $"^(public|private|public) class {VNRX}$";
        private static readonly string ClassSyntaxRegXNoAM = $"^class {VNRX}$";
        private static readonly string ClassSyntaxRegXNoClass = $"^(public|private|public) {VNRX}$";
        private static readonly string ClassSyntaxRegXNoRawNAme = $"^{VNRX}$";

        private static readonly string FieldSyntaxRegXBest = $"^(public|private|protected) {VNRX} {VNRX}$";
        private static readonly string FieldSyntaxRegXNoAM = $"^{VNRX} {VNRX}$";

        private static readonly string MethodReplaceRegX = GenMethodReplaceRegX();

        private static string GenMethodReplaceRegX()
        {
            var r = new StringBuilder();
            bool b = false;
            foreach (var item in (ExtraModifier[]) Enum.GetValues(typeof(ExtraModifier)))
            {
                if (b)
                    r.Append("|");
                r.Append(item);
                b = true;
            }
            return r.ToString();
        }

        public static bool TryParseAcMod(string s, out AccessModifier access)
        {
            s = s.ToUpper();
            if (s == "PUBLIC")
                access = AccessModifier.PUBLIC;
            else if (s == "PRIVATE")
                access = AccessModifier.PRIVATE;
            else if (s == "PROTECTED")
                access = AccessModifier.PROTECTED;
            else
            {
                access = AccessModifier.ERR;//Required line
                return false;
            }
            return true;
        }

        private class ParseClassResult
        {
            public AccessModifier AccessModifier { get; set; }
            public string Name { get; set; }
            public string FixedText { get; set; }
            public string OriginalText { get; set; }
        }

        private ParseClassResult ParseClassSyntax(string s)
        {
            ParseClassResult p = new ParseClassResult();
            p.OriginalText = s;
            if (Regex.IsMatch(s, ClassSyntaxRegXBest, RegexOptions.IgnoreCase))
            {
                var parts = s.Split(' ');
                p.AccessModifier = ParseAcMod(parts[0]);
                p.Name = parts[2];
                p.FixedText = p.AccessModifier.ToString().ToLower() + " class " + p.Name;
            }
            else if (Regex.IsMatch(s, ClassSyntaxRegXNoAM, RegexOptions.IgnoreCase))
            {
                var parts = s.Split(' ');
                p.AccessModifier = AccessModifier.PUBLIC;
                p.Name = parts[1];
                p.FixedText = p.AccessModifier.ToString().ToLower() + " class " + p.Name;
            }
            else if (Regex.IsMatch(s, ClassSyntaxRegXNoClass, RegexOptions.IgnoreCase))
            {
                var parts = s.Split(' ');
                p.AccessModifier = ParseAcMod(parts[0]);
                p.Name = parts[1];
                p.FixedText = p.AccessModifier.ToString().ToLower() + " class " + p.Name;
            }
            else if (Regex.IsMatch(s, ClassSyntaxRegXNoRawNAme, RegexOptions.IgnoreCase))
            {
                var parts = s.Split(' ');
                p.AccessModifier = AccessModifier.PUBLIC;
                p.Name = parts[0];
                p.FixedText = p.AccessModifier.ToString().ToLower() + " class " + p.Name;
            }
            else
            {
                throw new ParseException("Syntax is not valid");
            }
            return p;
        }

        public static AccessModifier ParseAcMod(string s)
        {
            s = s.ToUpper();
            if (s == "PUBLIC")
                return AccessModifier.PUBLIC;
            else if (s == "PRIVATE")
                return AccessModifier.PRIVATE;
            else if (s == "PROTECTED")
                return AccessModifier.PROTECTED;
            throw new ParseException("Cannot parse access modifier");
        }

        public IMethod MakeMethodFromSyntax(string s)
        {
            IMethod method = new Clazz.Method();
            var su = s.ToUpper();
            if (su.Contains("STATIC"))
                method.ExtraModifiers.Add(ExtraModifier.STATIC);
            if (su.Contains("ABSTRACT") || su.Contains("VIRTUAL"))
                method.ExtraModifiers.Add(ExtraModifier.ABSTRACT);
            var fixedString = Regex.Replace(s, MethodReplaceRegX, "",RegexOptions.IgnoreCase);
            fixedString = Regex.Replace(fixedString, "\\s\\s+", "");
            var nameing = fixedString.Substring(0, fixedString.IndexOf('('));
            if(Regex.IsMatch(fixedString, "^(public|private|protected).*$", RegexOptions.IgnoreCase))
            {
                var namingParts = nameing.Split(' ');
                method.AccessModifier = ParseAcMod(namingParts[0]);
                method.ReturnType = namingParts[1];
                method.Name = namingParts[2];
            }
            else
            {
                var namingParts = nameing.Split(' ');
                method.AccessModifier = AccessModifier.PUBLIC;
                method.ReturnType = namingParts[1];
                method.Name = namingParts[1];
            }
            var oPIndex = s.IndexOf('(');
            var cPIndex = s.IndexOf(')');
            method.Parameters = ParseParamsInsideMethod(s.Substring(oPIndex+1, cPIndex - oPIndex-1));
            return method;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">Everything between (), parentheses not included</param>
        /// <returns></returns>
        private IEnumerable<IField> ParseParamsInsideMethod(string s)
        {
            List<IField> fields = new List<IField>();
            foreach (var part in s.Split(','))
            {
                var pT = part.Trim();
                var pts = pT.Split(' ');
                if (pts.Length != 2)
                    throw new ParseException("Invalid argument syntax");
                fields.Add(new Clazz.Field()
                {
                    Type = pts[0],
                    Name =  pts[1],
                    AccessModifier = AccessModifier.NONE
                });
            }
            return fields;
        }

        public IField MakeFieldFromSyntax(string s)
        {
            IField field = new Clazz.Field();
            if (Regex.IsMatch(s, FieldSyntaxRegXBest, RegexOptions.IgnoreCase))
            {
                var parts = s.Split(' ');
                field.AccessModifier = ParseAcMod(parts[0]);
                field.Type = parts[1];
                field.Name = parts[2];
            }
            else if (Regex.IsMatch(s, FieldSyntaxRegXNoAM, RegexOptions.IgnoreCase))
            {
                var parts = s.Split(' ');
                field.AccessModifier = AccessModifier.PUBLIC;
                field.Type = parts[0];
                field.Name = parts[1];
            }
            else
            { //TODO:Better exceptions
                throw new ParseException("Field syntax is not valid");
            }
            return field;
        }

        public IClazz MakeClassFromSyntax(string s)
        {
            var r = ParseClassSyntax(s);
            return new Clazz()
            {
                AccessModifier = r.AccessModifier,
                Name = r.Name
            };
        }
    }
}
