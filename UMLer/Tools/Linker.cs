using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Controls;
using UMLer.Paintables;
using UMLer.Paintables.LinkPainters;

namespace UMLer.Tools
{
    public class Linker : ITool
    {
        public string Name => "Link tool";

        public string Description => "Links two elements together";

        private IPaintable FirstClicked;
        private IPaintable SecondClicked;

        public Color LinkColor { get; set; } = Color.Black;
        public float LineWidth { get; set; } = 1;

        private static List<Link> Links = new List<Link>();
        public IEnumerable<string> Linked { get {
                return Links.Select(r => r.Start.Location.ToString()+"->"+r.Finish.Location.ToString());
            } }

        public event PaintMouseHandler ClickedUsingTool;

        private void CreateLink()
        {
            var link = new Link(
                FirstClicked,
                SecondClicked
                )
            {
                PrimaryColor = LinkColor,
                LineWidth = this.LineWidth
            };
            link.PrimaryColor = LinkColor;
            link.LineWidth = this.LineWidth;
            Links.Add(link);
            link.Parent.Paintables.Add(link);
        }

        private bool IsLinkValid()
        {
            if (FirstClicked.Parent != SecondClicked.Parent)
                throw new InvalidOperationException("Both paintables must have the same parent");
            return true;
        }

        private void HandleLink(IPaintable paintable)
        {
            if(FirstClicked == null || FirstClicked == paintable)
            {
                FirstClicked = paintable;
                return;
            }
            else
            {
                SecondClicked = paintable;

                if (IsLinkValid())
                    CreateLink();

                SecondClicked.Parent.PaintablesLoseFocus();
                SecondClicked.Parent.Invalidate();

                FirstClicked = null;
                SecondClicked = null;

                
            }
        }

        public void ClickedOnPaintable(MouseEventArgs a, IPaintable paintable)
        {
            HandleLink(paintable);
            ClickedUsingTool?.Invoke(this, new PaintMouseArgs(a, paintable));
        }

        public void Clicked(MouseEventArgs a)
        {
            ClickedUsingTool?.Invoke(this, new PaintMouseArgs(a, null));
        }
    }
}
