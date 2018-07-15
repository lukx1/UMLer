using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer.DiagramData;

namespace UMLer.Paintables
{
    [Serializable]
    public class DiagramClass : DraggableCoreClass
    {
        private Clazz _RepresentingClass = Clazz.Empty;
        public Clazz RepresentingClass
        {
            get => _RepresentingClass;
            set
            {
                _RepresentingClass = value;
                RaisePropertyChanged("RepresentingClass");
            }
        }

        private const int MIN_WIDTH = 40;
        private const int MAX_WIDTH = 300;
        private const int HEADER_PADDING = 6;
        private const int LINE_VERT_SEP = 3;

        private Rectangle HeaderRect = new Rectangle();

        private bool RecalcPaint = true;

        private InnerTextField __HeaderTextField;
        private InnerTextField HeaderField
        {
            get => __HeaderTextField;
            set
            {
                Parent.Paintables.Remove(__HeaderTextField);
                lock (lockObj)
                {
                    Parent.Paintables.Add(value);
                }
                __HeaderTextField = value;
            }
        }

        private object lockObj = new object();

        private IList<InnerTextField> Methods = new List<InnerTextField>();
        private IList<InnerTextField> Fields = new List<InnerTextField>();

        private IEnumerable<InnerTextField> ValidTFS
        {
            get
            {
                if (HeaderField != null)
                    yield return HeaderField;
                foreach (var method in Methods)
                {
                    yield return method;
                }
                foreach (var field in Fields)
                {
                    yield return field;
                }
            }
        }

        public DiagramClass()
        {
            this.PropertyChanged += DiagramClass_PropertyChanged;
        }

        private void DiagramClass_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RecalcPaint = true;
        }

        private void CalculateHeader(Graphics g)
        {
            var headerTextSize = g.MeasureString(RepresentingClass.Name, Font);
            var headerWidth = DisplayRectangle.Width;
            var headerHeight = Font.Height + HEADER_PADDING * 2;
            if (headerTextSize.Width > DisplayRectangle.Width)
            {
                headerHeight += (int)((Font.Height + LINE_VERT_SEP) * (headerTextSize.Width / MAX_WIDTH));
            }
            HeaderRect = new Rectangle(Location.X, Location.Y, (int)headerWidth, headerHeight);
            if (HeaderField == null)
            {
                HeaderField = new InnerTextField(this);
                HeaderField.Name = "Header Field";
                MakeStandardITF(HeaderField);
            }
            else
            {
                HeaderField.Location = this.Location;
                HeaderField.Size = HeaderRect.Size;
            }
        }

        private void MakeStandardITF(InnerTextField innerTextField)
        {
            innerTextField.ParentPaintable = this;
            innerTextField.Location = this.Location;
            innerTextField.Size = HeaderRect.Size;
            innerTextField.PrimaryColor = this.PrimaryColor;
            innerTextField.SecondaryColor = this.SecondaryColor;
            innerTextField.BackgroundColor = this.BackgroundColor;
            innerTextField.Parent = this.Parent;
            innerTextField.ZIndex = 1;
            innerTextField.StringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };
            innerTextField.Font = this.Font;
        }

        private void CalculateFields()
        {
            var fieldsHeight = RepresentingClass.Fields.Count() * (Font.Height + LINE_VERT_SEP);
            var fieldHeight = Font.Height + LINE_VERT_SEP;
            if (RepresentingClass.Fields.Count() > 0)
            {
                var startY = HeaderRect.Y + HeaderRect.Height;
                for (int i = 0; i < RepresentingClass.Fields.Count(); i++)
                {
                    var field = new InnerTextField(this);
                    field.Name = RepresentingClass.Fields[i].Name;
                    field.PaintBackground = true;
                    field.BackgroundColor = Color.Red;
                    MakeStandardITF(field);
                    field.Location = new Point(DisplayRectangle.X, startY + fieldHeight * i);
                    field.Size = new Size(DisplayRectangle.Width, fieldHeight);
                    Fields.Add(field);
                    lock (lockObj)
                    {
                        Parent.Paintables.Add(field);
                    }
                }
            }
            else
            {
                var startY = HeaderRect.Y + HeaderRect.Height;
                for (int i = 0; i < Fields.Count(); i++)
                {
                    var field = Fields[i];
                    field.Location = new Point(DisplayRectangle.X, startY + fieldHeight * i);
                    field.Size = new Size(DisplayRectangle.Width, fieldHeight);
                }
            }
        }

        private void CalculateMethods()
        {

        }

        private void CalculatePainting(Graphics g)
        {
            CalculateHeader(g);
            CalculateFields();
            CalculateMethods();
        }

        private void PaintHeader(Graphics g)
        {
            if (RecalcPaint)
            {
                CalculatePainting(g);
                RecalcPaint = false;
            }
            g.FillRectangle(new SolidBrush(SecondaryColor), HeaderRect);
        }

        public override void Paint(Graphics g)
        {
            PaintHeader(g);

        }
    }
}
