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
    public class MethodITF : InnerTextField
    {
        private IMethod RepresentingMethod;
        private ClazzHelper helper = new ClazzHelper();

        public MethodITF(IPaintable ParentPaintable,IMethod RepresentingMethod) : base(ParentPaintable)
        {
            this.RepresentingMethod = RepresentingMethod;
            TextWritten += MethodITF_TextWritten;
        }

        private void MethodITF_TextWritten(object sender, TextWrittenArgs e)
        {
            try
            {
                var res = helper.MakeMethodFromSyntax(e.NewText);//TODO:Shouldn't use this function
                this.RepresentingMethod.AccessModifier = res.AccessModifier;
                this.RepresentingMethod.ExtraModifiers = res.ExtraModifiers;
                this.RepresentingMethod.Name = res.Name;
                this.RepresentingMethod.Parameters = res.Parameters;
                this.RepresentingMethod.ReturnType = res.ReturnType;
                this.Text = RepresentingMethod.ToSyntax();
            }
            catch (ParseException ex)
            {//TODO:Better
                MessageBox.Show(ex.ToString());
                this.Text = e.PreviousText;
            }
        }
    }
}
