using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.DiagramData;

namespace UMLer.Loading.Parsers
{
    public interface ILangParser
    {
        Language Language { get; }
        void SetParseDirectory(string path);
        void Parse();
        IEnumerable<Clazz> GetAllClasses();
        IEnumerable<Field> GetAllLoneFields();
        IEnumerable<Method> GetAllLoneMethods();
        bool FinishedWithoutErrors();
    }
}
