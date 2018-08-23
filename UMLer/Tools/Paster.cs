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
    public class Paster : AbstractTool
    {
        public override string Name => "Paster";

        public override string Description => "Pastes objects from the clipboard";

        public override void Clicked(MouseEventArgs args)
        {
            base.Clicked(args);
            
            if(Diagram != null && Diagram.ClipBoard != null)
            {
                if((Diagram.ClipBoard is IPaintable))
                {
                    object cloneObj = ((IPaintable)Diagram.ClipBoard).SafeDeepClone();
                    if(cloneObj == null)
                    {
                        MessageBox.Show("Object can't be pasted");
                        return;
                    }
                    IPaintable clone = (IPaintable)cloneObj;

                    clone.Location = args.Location;
                    Diagram.ElementPanel.Paintables.Add(clone);
                }
            }
        }
    }
}
