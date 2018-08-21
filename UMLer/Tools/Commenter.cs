using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Paintables;
using UMLer.Special;

namespace UMLer.Tools
{
    public class Commenter : AbstractTool
    {
        public override string Name => "Commenter";

        public override string Description => "Creates comments";

        public override void Clicked(MouseEventArgs args)
        {
            base.Clicked(args);
            AddComment(args);
        }

        private void AddComment(MouseEventArgs args)
        {
            var comment = new Comment(Diagram.ElementPanel);
            comment.Location = args.Location;
            comment.Size = new System.Drawing.Size(100, 100);
            Diagram.ElementPanel.Paintables.Add(comment);
        }

        public override void ClickedOnPaintable(MouseEventArgs args, [CanBeNull] IPaintable nullablePaintable)
        {
            base.ClickedOnPaintable(args, nullablePaintable);
            Diagram.SelectTool<Pointer>();
            //AddComment(args);
        }
    }
}
