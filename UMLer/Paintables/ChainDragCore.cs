using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Paintables
{
    public class ChainDragCore : DragCore
    {
        private IPaintable chain;

        public ChainDragCore(IPaintable capture,IPaintable chain) : base(capture)
        {
            this.chain = chain;
        }

        protected override void DoMove(Size moveAmount)
        {
            chain.Location += moveAmount;
        }

    }
}
