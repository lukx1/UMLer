using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Category("Naming")]
        public string Name => "Link tool";

        [Category("Naming")]
        public string Description => "Links two elements together";

        private IPaintable FirstClicked;
        private IPaintable SecondClicked;

        [Category("Design")]
        public Color LinkColor { get; set; } = Color.Black;
        [Category("Design")]
        public float LineWidth { get; set; } = 1;
        [Category("Design")]
        public BendStyle BendStyle { get; set; } = BendStyle.FORTY_FIVE;
        [Category("Design")]
        public LinkType StartArrow { get; set; } = LinkType.DEPENDANCE;
        [Category("Design")]
        public LinkType FinishArrow { get; set; } = LinkType.DEPENDANCE;

        private static List<Link> Links = new List<Link>();
        [Browsable(false)]
        public IEnumerable<string> Linked { get {
                return Links.Select(r => r.Start.Location.ToString()+"->"+r.Finish.Location.ToString());
            } }

        [Browsable(false)]
        public Diagram Diagram { get; set; }

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
            link.BendStyle = BendStyle;
            link.LinkTypeStart = StartArrow;
            link.LinkTypeFinish = FinishArrow;
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
