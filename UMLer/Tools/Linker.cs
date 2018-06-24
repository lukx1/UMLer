using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Paintables;

namespace UMLer.Tools
{
    public class Linker : ITool
    {
        public string Name => "Link tool";

        public string Description => "Links two elements together";

        private IPaintable FirstClicked;
        private IPaintable SecondClicked;

        private List<Link> Links = new List<Link>();

        private void CreateLink()
        {
            var link = new Link(FirstClicked.Parent, FirstClicked, SecondClicked);
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

                FirstClicked = null;
                SecondClicked = null;
            }
        }

        public void ClickedOnPaintable(MouseEventArgs a, IPaintable paintable)
        {
            HandleLink(paintable);
        }

        public void Clicked(MouseEventArgs args)
        {
            //Empty
        }
    }
}
