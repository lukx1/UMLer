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

        private const int FIELD_HEIGHT_INC = 10;

        private Rectangle HeaderRect = new Rectangle();
        private Rectangle FieldRect = new Rectangle();
        private Rectangle MethodRect = new Rectangle();

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
            var headerHeight = Font.Height + FIELD_HEIGHT_INC + HEADER_PADDING * 2;
            if (headerTextSize.Width > DisplayRectangle.Width)
            {
                headerHeight += (int)((Font.Height + LINE_VERT_SEP) * (headerTextSize.Width / MAX_WIDTH));
            }
            HeaderRect = new Rectangle(Location.X, Location.Y, (int)headerWidth, headerHeight);
            
            if (HeaderField == null)
            {
                HeaderField = new ClassITF(this,RepresentingClass);
                MakeStandardITF(HeaderField);
                HeaderField.Name = "Header Field";
                HeaderField.Text = RepresentingClass.ToSyntax();
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
            innerTextField.ZIndex = this.ZIndex+1;
            innerTextField.PaintBackground = true;
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
            var fieldHeight = Font.Height + FIELD_HEIGHT_INC + LINE_VERT_SEP;
            var fieldsHeight = RepresentingClass.Fields.Count() * fieldHeight;
            
            FieldRect = new Rectangle(HeaderRect.Location,new Size(HeaderRect.X,fieldsHeight));
            if (RepresentingClass.Fields.Count() > 0 && Fields.Count() == 0) // Rep fields exist but aren't drawn yet
            {
                var startY = HeaderRect.Y + HeaderRect.Height;
                for (int i = 0; i < RepresentingClass.Fields.Count(); i++)
                {
                    var field = new FieldITF(this,RepresentingClass.Fields[i]);
                    field.Name = RepresentingClass.Fields[i].Name;
                    field.PaintBackground = true;
                    field.Text = RepresentingClass.Fields[i].ToSyntax();
                    field.BackgroundColor = Color.White;
                    MakeStandardITF(field);
                    field.Location = new Point(DisplayRectangle.X, startY + fieldHeight * i);
                    field.Size = new Size(DisplayRectangle.Width, fieldHeight);
                    if(i == RepresentingClass.Fields.Count() - 1) // Last field
                    {
                        field.LastField = true;
                    }
                    Fields.Add(field);
                    lock (lockObj)
                    {
                        Parent.Paintables.Add(field);
                    }
                }
            }
            else // Fields exist but must be updated
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
            var methodHeight = Font.Height + FIELD_HEIGHT_INC + LINE_VERT_SEP;
            var methodsHeight = RepresentingClass.Methods.Count() * methodHeight;
            
            MethodRect = new Rectangle(HeaderRect.Location,new Size(HeaderRect.X,methodsHeight));
            if (RepresentingClass.Methods.Count() > 0 && Methods.Count() == 0) // Rep fields exist but aren't drawn yet
            {
                var startY = HeaderRect.Y + HeaderRect.Height + FieldRect.Height;
                for (int i = 0; i < RepresentingClass.Methods.Count(); i++)
                {
                    var methoditf = new MethodITF(this, RepresentingClass.Methods[i]);
                    methoditf.Name = RepresentingClass.Methods[i].Name;
                    methoditf.PaintBackground = true;
                    methoditf.Text = RepresentingClass.Methods[i].ToSyntax();
                    methoditf.BackgroundColor = Color.White;
                    MakeStandardITF(methoditf);
                    methoditf.Location = new Point(DisplayRectangle.X, startY + methodHeight * i);
                    methoditf.Size = new Size(DisplayRectangle.Width, methodHeight);
                    Methods.Add(methoditf);
                    lock (lockObj)
                    {
                        Parent.Paintables.Add(methoditf);
                    }
                }
            }
            else // Methods exist but must be updated
            {
                var startY = HeaderRect.Y + HeaderRect.Height + FieldRect.Height;
                for (int i = 0; i < Methods.Count(); i++)
                {
                    var methoditf = Methods[i];
                    methoditf.Location = new Point(DisplayRectangle.X, startY + methodHeight * i);
                    methoditf.Size = new Size(DisplayRectangle.Width, methodHeight);
                }
            }
        }

        private void CalculatePainting(Graphics g)
        {

            CalculateHeader(g);
            CalculateFields();
            CalculateMethods();
            this.Height = HeaderRect.Height + FieldRect.Height + MethodRect.Height;
        }

        private void PaintAll(Graphics g)
        {
            if (RecalcPaint)
            {
                CalculatePainting(g);
                RecalcPaint = false;
            }
            
        }

        public override void Paint(Graphics g)
        {     
            PaintAll(g);
        }
    }
}
