using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Paintables;

namespace UMLer.Tools
{
    public interface ITool
    {
        string Name { get; }
        string Description { get; }
        void Clicked(MouseEventArgs args);
        void ClickedOnPaintable(MouseEventArgs args, IPaintable paintable);
    }
}
