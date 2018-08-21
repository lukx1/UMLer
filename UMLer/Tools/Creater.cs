using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.DiagramData;
using UMLer.Paintables;
using UMLer.Special;

namespace UMLer.Tools
{
    public class Creater : AbstractTool
    {
        [Category("Naming")]
        public override string Name => "Creater";

        [Category("Naming")]
        public override string Description => "Creates UML objects";

        public Size Size { get; set; } = new Size(100, 100);

        public Color PrimaryColor { get; set; } = Color.Cyan;

        public Color SecondaryColor { get; set; } = Color.Black;

        public Color BackgroundColor { get; set; } = Color.White;

        public override void Clicked(MouseEventArgs args)
        {
            
            base.Clicked(args);
            var dc = new DiagramClass();
            dc.Location = args.Location;
            dc.RepresentingClass = Clazz.CreateEmpty();
            dc.RepresentingClass.Name = "EmptyClass";
            dc.Size = Size;
            dc.Parent = Diagram.ElementPanel;
            dc.PrimaryColor = PrimaryColor;
            dc.SecondaryColor = SecondaryColor;
            dc.BackgroundColor = BackgroundColor;
            dc.Name = "New Class";
            Diagram.ElementPanel.Paintables.Add(dc);
        }

        public override void ClickedOnPaintable(MouseEventArgs args, [CanBeNull] IPaintable nullablePaintable)
        {
            base.ClickedOnPaintable(args, nullablePaintable);
            Diagram.SelectTool<Pointer>();
        }
    }
}
