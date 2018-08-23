using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UMLer.Controls;
using UMLer.DiagramData;

namespace UMLer.Paintables
{
    [Serializable]
    public class DiagramClass : DraggableCoreClass
    {
        private Clazz _RepresentingClass = Clazz.CreateEmpty();
        public Clazz RepresentingClass
        {
            get => _RepresentingClass;
            set
            {
                _RepresentingClass = value;
                RaisePropertyChanged("RepresentingClass");
            }
        }

        [BrowsableAttribute(false)]
        [XmlIgnore]
        public override ElementPanel Parent { get => base.Parent; set
            {
                base.Parent = value;
                if (!isCheckingForLinks)
                {
                    value.Diagram.LinkCreated += Diagram_LinkCreated;
                    isCheckingForLinks = true;
                }
            }
        }

        private void Diagram_LinkCreated(object sender, ILink link)
        {
            /*
             Created links are not saved because ITFs are temporary
             IPaintables, link info must be saved directly here
             and in Saver.HandleLink must be added something for this.
             Class names are unique!
             */
            throw new NotImplementedException();
        }

        private bool isCheckingForLinks = false;

        private bool IsDeleted = false;

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

        private List<InnerTextField> Methods = new List<InnerTextField>();
        private List<InnerTextField> Fields = new List<InnerTextField>();

        public class RepClassLinks
        {

        }

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

        private void SetMethodVars(MethodITF field, IMethod repMethod, Point location, Size size)
        {
            field.Name = repMethod.Name;
            field.PaintBackground = true;
            field.Text = repMethod.ToSyntax();
            field.BackgroundColor = this.BackgroundColor;
            MakeStandardITF(field);
            field.Location = location;
            field.Size = size;
        }

        private void SetFieldVars(FieldITF field, IField repfield,Point location,Size size)
        {
            field.Name = repfield.Name;
            field.PaintBackground = true;
            field.Text = repfield.ToSyntax();
            field.BackgroundColor = this.BackgroundColor;
            MakeStandardITF(field);
            field.Location = location;
            field.Size = size;
        }

        private int GetFieldsStartY()
        {
            return HeaderRect.Y + HeaderRect.Height;
        }

        private int GetMethodStartY()
        {
            int y = HeaderRect.Y + HeaderRect.Height;
            foreach (var field in Fields)
            {
                y += field.Height;
            }
            return y;
        }

        private void CalculateFields()
        {
            var fieldHeight = Font.Height + FIELD_HEIGHT_INC + LINE_VERT_SEP;
            var fieldsHeight = (RepresentingClass.Fields.Count()+1) * fieldHeight;
            
            FieldRect = new Rectangle(HeaderRect.Location,new Size(HeaderRect.X,fieldsHeight));
            if (RepresentingClass.Fields.Count() > 0 && Fields.Count() == 0) // Rep fields exist but aren't drawn yet
            {
                var startY = HeaderRect.Y + HeaderRect.Height;
                var totalFields = RepresentingClass.Fields.Count() + 1;
                for (int i = 0; i < totalFields; i++)
                {
                    if (i != totalFields-1)
                    {
                        var field = new FieldITF(this, RepresentingClass.Fields[i], RepresentingClass);
                        SetFieldVars(
                            field,
                            RepresentingClass.Fields[i],
                            new Point(DisplayRectangle.X, startY + fieldHeight * i),
                            new Size(DisplayRectangle.Width, fieldHeight)
                            );
                        Fields.Add(field);
                        lock (lockObj)
                        {
                            Parent.Paintables.Add(field);
                        }
                    }
                    else
                    {
                        var repField = Clazz.Field.CreateEmpty();
                        repField.Type = "...";
                        var field = new FieldITF(this, repField, RepresentingClass,false);
                        field.Initialized += (object o, EventArgs a) =>
                        {
                            var rf = Clazz.Field.CreateEmpty();
                            var fld = new FieldITF(this, rf, RepresentingClass, false);
                            fld.Text = "...";
                            SetFieldVars(
                            fld,
                            rf,
                            new Point(DisplayRectangle.X, startY + fieldHeight * i),
                            new Size(DisplayRectangle.Width, fieldHeight)
                            );
                            AddFieldToBottom(fld);
                        }
                        ;
                        SetFieldVars(
                            field,
                            repField,
                            new Point(DisplayRectangle.X, startY + fieldHeight * i),
                            new Size(DisplayRectangle.Width, fieldHeight)
                            );
                        Fields.Add(field);
                        lock (lockObj)
                        {
                            Parent.Paintables.Add(field);
                        }
                    }
                }

            }
            else // Fields exist but must be updated
            {
                var startY = GetFieldsStartY();
                var yOffset = 0;
                for (int i = 0; i < Fields.Count(); i++)
                {
                    var field = Fields[i];
                    field.Location = new Point(DisplayRectangle.X, startY + yOffset);
                    field.Size = new Size(DisplayRectangle.Width, field.Height);
                    yOffset += field.Height;
                }
            }
        }

        

        public bool RemoveField(Clazz.Field field)
        {
            foreach (var fld in Fields)
            {
                if(fld is FieldITF)
                {
                    if (((FieldITF)fld).RepresentingField == field)
                    {
                        Fields.Remove(fld);
                        RepresentingClass.Fields.Remove((Clazz.Field)((FieldITF)fld).RepresentingField);
                        RecalcPaint = true;
                        return true;
                    }
                        
                }
            }
            return false;
        }

        public bool RemoveMethod(Clazz.Method method)
        {
            foreach (var mtd in Methods)
            {
                if (mtd is MethodITF)
                {
                    if (((MethodITF)mtd).RepresentingMethod == method)
                    {
                        Methods.Remove(mtd);
                        RepresentingClass.Methods.Remove((Clazz.Method)((MethodITF)mtd).RepresentingMethod);
                        RecalcPaint = true;
                        return true;
                    }

                }
            }
            return false;
        }

        private void CalculateMethods()
        {
            var methodHeight = Font.Height + FIELD_HEIGHT_INC + LINE_VERT_SEP;
            var methodsHeight = (RepresentingClass.Methods.Count() + 1) * methodHeight;

            MethodRect = new Rectangle(HeaderRect.Location, new Size(HeaderRect.X, methodsHeight));
            if (RepresentingClass.Methods.Count() > 0 && Methods.Count() == 0) // Rep fields exist but aren't drawn yet
            {
                var startY = HeaderRect.Y + HeaderRect.Height + FieldRect.Height;
                var totalMethods = RepresentingClass.Methods.Count() + 1;
                for (int i = 0; i < totalMethods; i++)
                {
                    if (i != totalMethods - 1)
                    {
                        var methoditf = new MethodITF(this, RepresentingClass.Methods[i], RepresentingClass);

                        methoditf.Name = RepresentingClass.Methods[i].Name;
                        methoditf.PaintBackground = true;
                        methoditf.Text = RepresentingClass.Methods[i].ToSyntax();
                        methoditf.BackgroundColor = this.BackgroundColor;
                        MakeStandardITF(methoditf);
                        methoditf.Location = new Point(DisplayRectangle.X, startY + methodHeight * i);
                        methoditf.Size = new Size(DisplayRectangle.Width, methodHeight);
                        Methods.Add(methoditf);
                        lock (lockObj)
                        {
                            Parent.Paintables.Add(methoditf);
                        }
                    }
                    else
                    {
                        var repMethod = Clazz.Method.CreateEmpty();
                        repMethod.ReturnType = "...";
                        var field = new MethodITF(this, repMethod, RepresentingClass, false);
                        field.Initialized += (object o, EventArgs a) =>
                        {
                            var rm = Clazz.Method.CreateEmpty();
                            var fld = new MethodITF(this, rm, RepresentingClass, false);
                            fld.Text = "...";
                            SetMethodVars(
                            fld,
                            rm,
                            new Point(DisplayRectangle.X, startY + methodHeight * i),
                            new Size(DisplayRectangle.Width, methodHeight)
                            );
                            AddMethodToBottom(fld);
                        };
                        SetMethodVars(
                            field,
                            repMethod,
                            new Point(DisplayRectangle.X, startY + methodHeight * i),
                            new Size(DisplayRectangle.Width, methodHeight)
                            );
                        Methods.Add(field);
                        lock (lockObj)
                        {
                            Parent.Paintables.Add(field);
                        }
                    }
                }
            }
            else // Methods exist but must be updated
            {
                var startY = GetMethodStartY();
                var yOffset = 0;
                for (int i = 0; i < Methods.Count(); i++)
                {
                    var methoditf = Methods[i];
                    methoditf.Location = new Point(DisplayRectangle.X, startY + yOffset);
                    methoditf.Size = new Size(DisplayRectangle.Width, methodHeight);
                    yOffset += methoditf.Height;
                }
            }
        }

        private void AddFieldToBottom(FieldITF field)
        {
            field.Location = new Point(
                HeaderRect.X,
                HeaderRect.Y + HeaderRect.Height + FieldRect.Height +field.Height
                );
            FieldRect = new Rectangle(FieldRect.X, FieldRect.Y, FieldRect.Width, FieldRect.Height + field.Height*2);
            MethodRect = new Rectangle(MethodRect.X, MethodRect.Y+field.Height, MethodRect.Width, MethodRect.Height);
            foreach (var method in Methods)
            {
                method.Location = new Point(method.Location.X+200,method.Location.Y+field.Height);
            }
            this.Height += field.Height;
            Fields.Add(field);
            lock (lockObj)
            {
                this.Parent.Paintables.Add(field);
            }
            this.Parent.Refresh();
        }

        private void AddMethodToBottom(MethodITF method)
        {
            method.Location = new Point(
                HeaderRect.X,
                HeaderRect.Y + HeaderRect.Height + FieldRect.Height + MethodRect.Height+method.Height
                );
            MethodRect = new Rectangle(MethodRect.X, MethodRect.Y, MethodRect.Width, MethodRect.Height + method.Height);
            Methods.Add(method);
            this.Width += method.Height;
            lock (lockObj)
            {
                this.Parent.Paintables.Add(method);
            }
            this.Parent.Refresh();
        }

       private int GetHeight()
        {
            int h = HeaderRect.Height;
            foreach (var methfield in Methods.Union<IPaintable>(Fields))
            {
                h += methfield.Height;
            }
            return h;
        }

        private void CalculatePainting(Graphics g)
        {

            CalculateHeader(g);
            CalculateFields();
            CalculateMethods();
            this.Height = GetHeight();
        }

        private void PaintAll(Graphics g)
        {
            if (RecalcPaint)
            {
                CalculatePainting(g);
                RecalcPaint = false;
            }
            
        }

        public override void OnDeleted()
        {
            if (IsDeleted)
                return;
            lock (this)
            {
                Parent.Paintables.SilentRemove(__HeaderTextField);
                foreach (var methodOrField in Methods.Union<IPaintable>(Fields))
                {
                    Parent.Paintables.SilentRemove(methodOrField);
                }
                IsDeleted = true;
            }
        }

        public override void Paint(Graphics g)
        {     
            PaintAll(g);
        }
    }
}
