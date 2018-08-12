using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.DiagramData;

namespace UMLer.Coder
{
    public interface ICoder
    {
        Language CodingLanguage { get; }
        string OutputDirectory { get; set; }
        Diagram Diagram { get; set; }
        void CreateCode();
        bool AreClazzesValid();
    }
}
