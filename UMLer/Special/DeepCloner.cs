using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UMLer.Paintables;

namespace UMLer.Special
{
    public static class DeepCloner
    {

        public static void CopyIPaintable(IPaintable from, IPaintable to)
        {
            to.BackgroundColor = from.BackgroundColor;
            to.Font = from.Font;
            to.Height = from.Height;
            to.Location = from.Location;
            to.Name = from.Name;
            to.Parent = from.Parent;
            to.PrimaryColor = from.PrimaryColor;
            to.SecondaryColor = from.SecondaryColor;
            to.Size = from.Size;
            to.Width = from.Width;
            to.ZIndex = from.ZIndex;
        }

        public static T DeepClone<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
