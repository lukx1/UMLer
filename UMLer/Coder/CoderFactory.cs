﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.DiagramData;

namespace UMLer.Coder
{
    public class CoderFactory
    {
        public Language Language { get; set; }
        public Diagram Diagram { get; set; }

        public CoderFactory(Diagram diagram)
        {
            this.Diagram = diagram;
        }

        public ICoder CreateCoder()//TODO:this
        {
            switch (Language)
            {
                case Language.CSHARP:
                    return new CSharpCoder(Diagram);
                default:
                case Language.CPP:
                case Language.JAVA:
                    throw new NotSupportedException("No coder for \"" + (Language.ToString().ToLower()) + "\" exists");
            }
        }
    }
}
