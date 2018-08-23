using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UMLer.Paintables;
using System.IO;
using UMLer.Special;
using System.Xml;

namespace UMLer.Loading
{
    public class Saver
    {
        public static readonly XmlSerializer PaintableSerializer;
        public string PathToFile;
        private static Type[] SerializableTypes = FetchAllIPaintables();

        private static Type[] FetchAllIPaintables()
        {
            var type = typeof(IPaintable);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes()) // All that are IPaintable
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract) // Non abstract classes
                .Where(p => p.GetCustomAttributes(typeof(NoSerializeAttribute), true).Length == 0)//No NoSerialize
                ;
            var type2 = typeof(DoSerialize);
            var types2 = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes()) // All that are IPaintable
                .Where(p => type2.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract) // Non abstract classes
                .Where(p => p.GetCustomAttributes(typeof(NoSerializeAttribute), true).Length == 0)//No NoSerialize
                ;
            return types.Concat(types2).ToArray();
        }

        static Saver()
        {
            PaintableSerializer = new XmlSerializer(typeof(List<object>), SerializableTypes);

        }

        private void CheckInternValues()
        {
            //TODO:Check values
        }

        private bool IsSerializable(object o)
        {
            return SerializableTypes.Any(t => o.GetType() == t);
        }

        private List<object> FilterIntoList(IEnumerable<IPaintable> paintables)
        {
            List<object> result = new List<object>();
            foreach (var paintable in paintables)
            {
                if (!IsSerializable(paintable))
                {
                    continue;
                }
                result.Add(paintable);
            }
            return result;
        }

        private void ValidateDiagram(Diagram diagram)
        {
            if(diagram.ElementPanel == null)
            {
                throw new ArgumentNullException("Diagram's ElementPanel can't be null");
            }
        }

        private void ValidatePaintables(HashSet<IPaintable> paintables)
        {
            var unique = paintables.Select(r => r.ID).Distinct();
            if (unique.Count() != paintables.Count())
            { // Unique ID check
                throw new InvalidOperationException("Paintables must have unique IDs");
            }
        }

        private void HandleLink(ILink link, HashSet<IPaintable> paintables)
        {//TODO:null chekc
            var start = paintables.Where(r => r.ID == link.StartID).FirstOrDefault();
            link.Start = start;
            var finish = paintables.Where(r => r.ID == link.FinishID).FirstOrDefault();
            link.Finish = finish;
            if (start == finish)
                throw new InvalidOperationException("Cant link an object to itself");
        }

        private void Link(HashSet<IPaintable> paintables, Diagram diagram)
        {
            foreach (var paintable in paintables)
            {
                paintable.Parent = diagram.ElementPanel;
                if (paintable is ILink)
                {
                    HandleLink((ILink)paintable, paintables);
                }
            }
        }

        private void RegenerateAll(HashSet<IPaintable> paintables, Diagram diagram)
        {
            foreach (var paintable in paintables)
            {
                paintable.Regenerate();
            }
        }

        public void LinkPaintables(HashSet<IPaintable> paintables,Diagram diagram)
        {
            ValidateDiagram(diagram);
            ValidatePaintables(paintables);

            Link(paintables, diagram);
            RegenerateAll(paintables, diagram);
        }

        public HashSet<IPaintable> LoadDiagram()
        {
            var objects = new HashSet<IPaintable>();
            try
            {
                Diagram.Deserializing = true;
                using (var StreamReader = new StreamReader(PathToFile))
                {
                    objects = new HashSet<IPaintable>(((IEnumerable<object>)PaintableSerializer.Deserialize(StreamReader)).Cast<IPaintable>());
                    
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                Diagram.Deserializing = false;
            }
            return objects;
        }

        public void SaveDiagram(Diagram diagram)
        {
            CheckInternValues();
            var paintables = FilterIntoList(diagram.ElementPanel.Paintables);

            using(var StreamWriter = new StreamWriter(PathToFile))
            {
                using (var XWriter = XmlWriter.Create(StreamWriter))
                {
                    PaintableSerializer.Serialize(XWriter, paintables);//TODO:Test this
                }
            }
            


        }
    }
}
