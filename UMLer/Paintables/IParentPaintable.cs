﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Paintables
{
    public interface IParentPaintable
    {
        IPaintable ParentPaintable { get; set; }
    }
}
