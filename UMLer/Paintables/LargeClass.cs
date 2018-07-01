using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Controls;

namespace UMLer.Paintables
{
    public class LargeClass : DraggableCoreClass
    {
        private Rectangle methodRect;
        private Rectangle propRect;
        private Rectangle headerRect;

        private const int PADDING = 2;

        private const int INNER_ELEMENT_HEIGHT = 20;
        private const int HEADER_MAX_HEIGHT = 23;
        private const int PROPS_MIN_HEIGHT = 8;
        private const int METHODS_MIN_HEIGHT = 8;
        private const int ELEMENT_MIN_WIDTH = 200;
        private const int ELEMENT_MIN_HEIGHT = 200;

        [Category("Functional")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<string> Properties { get; set; } = new List<string>();
        [Category("Functional")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<string> Methods { get; set; } = new List<string>();


        /// <summary>
        /// Draws the header bounding box
        /// and writes the name of this element
        /// inside it
        /// </summary>
        /// <param name="gfx">Graphics used to draw</param>
        private void DrawHeader(Graphics gfx) //TODO: Add trimming
        {
            headerRect = new Rectangle(
                DisplayRectangle.X,
                DisplayRectangle.Y,
                DisplayRectangle.Width,
                HEADER_MAX_HEIGHT - 1
                );

            gfx.FillRectangle(new SolidBrush(SecondaryColor), headerRect);
            gfx.DrawRectangle(new Pen(new SolidBrush(PrimaryColor), 1f), headerRect);

            StringFormat stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
            };
            gfx.DrawString(
                Name,
                SystemFonts.DefaultFont, //Font
                Brushes.Black, // Color
                new RectangleF(
                    DisplayRectangle.X,
                    DisplayRectangle.Y,
                    DisplayRectangle.Width,
                    HEADER_MAX_HEIGHT
                    ),
                stringFormat // Format
                );
        }

        /// <summary>
        /// Creates enough space for all props to fit inside it
        /// </summary>
        /// <returns>Height of box in pixels</returns>
        private int GetPropsBoxHeight() //TODO: Finish
        {
            return Math.Max(PROPS_MIN_HEIGHT, Properties == null ? 0 : Properties.Count() * (INNER_ELEMENT_HEIGHT + 1));
        }

        /// <summary>
        /// Creates enough space for all methods to fit inside it
        /// </summary>
        /// <returns>Height of box in pixels</returns>
        private int GetMethodsBoxHeight() //TODO: Finish
        {
            return Math.Max(METHODS_MIN_HEIGHT, Methods == null ? 0 : Methods.Count() * (INNER_ELEMENT_HEIGHT + 1));
        }

        /// <summary>
        /// Fills a rectangle with texts
        /// and separates them with lines
        /// </summary>
        /// <param name="gfx">Graphics used to draw</param>
        /// <param name="rectangle">Rectangle in which the text is draw</param>
        /// <param name="texts">Texts to draw</param>
        private void FillRectangleWithText(Graphics gfx, Rectangle rectangle, IEnumerable<string> texts)//TODO: Filling options
        {
            if (texts == null || texts.Count() < 1) // No texts check
                return;
            var textList = texts.ToList();//TODO: Check if box is big enough
            for (int i = 0; i < textList.Count(); i++)
            {
                gfx.DrawString(
                    textList[i],
                    Font,
                    new SolidBrush(PrimaryColor),
                    rectangle.X + PADDING, // Left side of box + padding for text
                    (rectangle.Y + (i * (INNER_ELEMENT_HEIGHT + 1)) + 2) // Top of box + i * space between text+1
                    );//(for separator line) +2(for top offset)
                var separatorY = rectangle.Y + (i % textList.Count() * (INNER_ELEMENT_HEIGHT + 1));
                //^ Top of box + i % count to just be Y on last element * space between text +1 for separator
                gfx.DrawLine(new Pen(PrimaryColor, 1f), rectangle.X, separatorY, rectangle.X + rectangle.Width, separatorY);
            }
        }

        /// <summary>
        /// Draws the properties bounding box
        /// and fills it with text foundin Properties member
        /// </summary>
        /// <param name="gfx">Graphics used to draw</param>
        private void DrawProps(Graphics gfx)
        {
            propRect = new Rectangle(
                DisplayRectangle.X,
                DisplayRectangle.Y + headerRect.Height,
                DisplayRectangle.Width,
                GetPropsBoxHeight()
                );

            gfx.FillRectangle(new SolidBrush(BackgroundColor), propRect);
            gfx.DrawRectangle(new Pen(PrimaryColor), propRect);

            FillRectangleWithText(gfx, propRect, Properties);
        }

        /// <summary>
        /// Draws a thick separating line between
        /// methods and props bounding boxes
        /// </summary>
        /// <param name="gfx">Graphics used to draw</param>
        private void DrawMethodsPropsSeparatorLine(Graphics gfx)
        {
            gfx.DrawLine(
                new Pen(PrimaryColor, 2),
                DisplayRectangle.X,
                DisplayRectangle.Y + headerRect.Height + propRect.Height,
                DisplayRectangle.X + methodRect.Width,
                DisplayRectangle.Y + headerRect.Height + propRect.Height
                );
        }

        /// <summary>
        /// Draws the methods bounding box a fills it with text
        /// found in Methods member
        /// </summary>
        /// <param name="gfx">Graphics used to draw</param>
        private void DrawMethods(Graphics gfx)
        {
            methodRect = new Rectangle(
                DisplayRectangle.X,
                DisplayRectangle.Y + headerRect.Height + propRect.Height,
                DisplayRectangle.Width,
                GetMethodsBoxHeight()
                );

            gfx.FillRectangle(new SolidBrush(BackgroundColor), methodRect);
            gfx.DrawRectangle(new Pen(PrimaryColor), methodRect);

            DrawMethodsPropsSeparatorLine(gfx);

            FillRectangleWithText(gfx, methodRect, Methods);
        }

        /// <summary>
        /// Draws supporting elements
        /// </summary>
        /// <param name="gfx">Graphics used to draw</param>
        private void DrawOther(Graphics gfx)
        {

        }

        private void DrawBg(Graphics gfx)
        {
            gfx.FillRectangle(new SolidBrush(BackgroundColor), DisplayRectangle);
            //gfx.DrawRectangle(new Pen(PrimaryColor, 1), DisplayRectangle);
        }

        /// <summary>
        /// Paints the elements
        /// </summary>
        /// <param name="pe">Paint args</param>
        public override void Paint(Graphics gfx)
        {
            DrawHeader(gfx);
            DrawProps(gfx);
            DrawMethods(gfx);
            DrawOther(gfx);
        }

        private void Init()
        {
            this.MouseUp += (object o, MouseEventArgs a) => this.Focus();//TODO:Probably not needed
        }

        public LargeClass() : base()
        {
            Init();  
        }

        public LargeClass(ElementPanel Parent) : base(Parent)
        {
            Init();
        }
    }
}
