using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLer;
using UMLer.Special;

namespace ExampleMod
{
    public class Mod : ModCore
    {
        public override int MOD_ID => -957521;

        public override string MOD_NAME => "Example mod";

        public override int VERSION => 1;

        public override void AppShuttingDown()
        {

        }

        public override void AppStarting()
        {

        }

        public override void EnvironmentLoaded(Diagram diagram)
        {
            diagram.ElementPanel.Paintables.Add(new UMLer.Paintables.SimpleClass(diagram.ElementPanel)
            {
                Name = "This was created by a mod",
                BackgroundColor = Color.Red,
                PrimaryColor = Color.Blue,
                SecondaryColor = Color.Green,
                Location = new Point(100, 100),
                Size = new Size(100, 100),
                ZIndex = 999
            });
        }
    }
}
