using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Controls;
using UMLer.Paintables;
using UMLer.Special;

namespace UMLer.Tools
{
   
    public interface ITool
    {
        string Name { get; }
        string Description { get; }
        
        Diagram Diagram { get; set; }

        event PaintMouseHandler ClickedUsingTool;

        void Clicked(MouseEventArgs args);
        void ClickedOnPaintable(MouseEventArgs args, [CanBeNull]IPaintable nullablePaintable);
    }
}
