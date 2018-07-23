using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.DiagramData;
using UMLer.Special;

namespace UMLer.Paintables
{
    public class FieldITF : InnerTextField
    {
        public override string Text { get => base.Text; set => base.Text = value; }
        private IField RepresentingField;
        private ClazzHelper helper = new ClazzHelper();

        public FieldITF(IPaintable ParentPaintable,IField RepresentingField) : base(ParentPaintable)
        {
            TextWritten += FieldITF_TextWritten;
            this.RepresentingField = RepresentingField;
        }

        private void FieldITF_TextWritten(object sender, TextWrittenArgs e)
        {
            try
            {
                var res = helper.MakeFieldFromSyntax(e.NewText);//TODO:Shouldn't use this function
                this.RepresentingField.AccessModifier = res.AccessModifier;
                this.RepresentingField.ExtraModifiers = res.ExtraModifiers;
                this.RepresentingField.Name = res.Name;
                this.RepresentingField.Type = res.Type;
                this.Text = RepresentingField.ToSyntax();
            }
            catch(ParseException ex)
            {//TODO:Better
                MessageBox.Show(ex.ToString());
                this.Text = e.PreviousText;
            }
        }
    }
}
