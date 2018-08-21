using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Paintables
{
    public class PositionBinder
    {
        private List<IPaintable> Sources = new List<IPaintable>();
        private IPaintable Target;
        private Point PreviousTargetPos;

        public PositionBinder(IPaintable Source,IPaintable Target)
        {
            this.Target = Target;
            this.Sources.Add(Source);
        }

        public void AddSource(IPaintable Source)
        {
            this.Sources.Add(Source);
        }

        public bool RemoveSource(IPaintable Source)
        {
            return this.Sources.Remove(Source);
        }

        public void BindTo(IPaintable Target)
        {
            if(Target != null)
            {
                Unbind();
            }
            PreviousTargetPos = Target.Location;
            Target.LocationChanged += Target_LocationChanged;
        }

        public void Unbind()
        {
            Target.LocationChanged -= Target_LocationChanged;
        }

        private void Target_LocationChanged(object sender, EventArgs e)
        {
            var moveAmount = new Size(Target.Location.X - PreviousTargetPos.X, Target.Location.Y - PreviousTargetPos.Y);
            PreviousTargetPos = Target.Location;
            this.Sources.ForEach(r => r.Location += moveAmount);
        }
    }
}
