using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.Paintables;

namespace UMLer.Controls
{
    public class InnerTextBox 
    {
        public string Text { get; set; } = "";
        public IPaintable Supervisor { get; set; }
        public Rectangle DisplayRectange { get; set; }

        public InnerTextBox(IPaintable Supervisor)
        {
            this.Supervisor = Supervisor;
        }

        public bool Contains(Point p)
        {
            return DisplayRectange.Contains(p);
        }

        public void Paint(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
