using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Paintables;

namespace UMLer.Controls
{
    public class ElementPanel : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private HashSet<IPaintable> _Paintables = new HashSet<IPaintable>();
        public HashSet<IPaintable> Paintables { get => _Paintables; set
            {
                _Paintables = value;
                RegenFocusObj();
            } }
        public Focus FocusObj;
        private IPaintable _FocusedElement;
        public Diagram Diagram { get; set; }
        public IPaintable FocusedElement { get => _FocusedElement; private set
            {
                _FocusedElement = value;
                FocusChanged?.Invoke(this, new PaintableEventArgs(_FocusedElement));
            } }

        public event PaintableEventHandler FocusChanged;

        public void BindDiagram(Diagram diagram)
        {
            this.Diagram = diagram;
        }

        public void PaintablesLoseFocus()
        {
            //TODO:Implement
        }

        private void RegenFocusObj()
        {
            if (Paintables.OfType<Focus>().Count() != 0)
            {
                Paintables.RemoveWhere(r => r is Focus);
            }
            FocusObj = new Focus(this);
            Paintables.Add(FocusObj);
        }

        public ElementPanel()
        {
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);
            RegenFocusObj();
            FocusChanged += FocusRefresher;
        }

        private void FocusRefresher(object sender, PaintableEventArgs args)
        {
            if (args.IPaintable is ILink)
            {
                FocusObj.FocusAround = null;
            }
            else
            {
                FocusObj.FocusAround = args.IPaintable;
            }
            this.Invalidate();
            //throw new NotImplementedException();
        }

        public void Focus(IPaintable paintable)
        {
            if (FocusedElement == paintable)
                return;
            FocusedElement = paintable;
            paintable.Focus();
        }

        private void CascadeClickToTool(MouseEventArgs args, IPaintable paintable)
        {
            if (Diagram == null)
            {
                throw new InvalidOperationException(
                    "Diagram has not been set." +
                    " Its tool can't be used"
                    );
            }

            if (Diagram.SelectedTool == null)
            {
                return;
            }

            if(paintable != null)
            {
                Diagram.SelectedTool.ClickedOnPaintable(args, paintable);
            }
            else
            {
                Diagram.SelectedTool.Clicked(args);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            IPaintable paintableClicked = null;
            foreach (var paintable in Paintables)
            {
                if (paintable.Contains(e.Location))
                {
                    paintableClicked = paintable;
                    paintable.RaiseClicked(e);
                    break;
                }
            }

            CascadeClickToTool(e, paintableClicked);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            foreach (var paintable in Paintables)
            {
                if (paintable.Contains(e.Location))
                {
                    paintable.RaiseMouseUp(e);
                    break;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            foreach (var paintable in Paintables)
            {
                if (paintable.Contains(e.Location))
                {
                    paintable.RaiseMouseDown(e);
                    break;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            foreach (var paintable in Paintables)
            {
                if (paintable.Contains(e.Location))
                {
                    paintable.RaiseMouseMove(e);
                    break;
                }
            }
        }


        private void HandleIPaintable(IPaintable paintable)
        {
            paintable.LocationChanged += (object o, EventArgs a) => this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var paintable in Paintables)
            {
                paintable.Paint(e.Graphics);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is IPaintable)
            {
                HandleIPaintable((IPaintable)e.Control);
            }
        }


    }
}
