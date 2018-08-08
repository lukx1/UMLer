using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Special
{
    public class ModLoader
    {
        public ISet<ModCore> Mods { get; private set; } = new HashSet<ModCore>();
        public TimeSpan TimeOutStart = TimeSpan.FromSeconds(10);
        public TimeSpan TimeOutEnv = TimeSpan.FromSeconds(10);
        public TimeSpan TimeOutShut = TimeSpan.FromSeconds(3);

        public void LoadMods(string folder)
        {
            List<string> mods = new List<string>();
            foreach(var mod in Directory.EnumerateFiles(folder))
            {
                if (Path.GetExtension(mod).ToUpper() != ".DLL")
                    throw new FileLoadException($"Mod {mod} is not a .dll");
                mods.Add(mod);
            }
            mods.ForEach(r => LoadMod(r));
        }

        public void AppStarting()
        {
            foreach (var mod in Mods)
            {
                var task = Task.Run(() => mod.AppStarting());
                if (!task.Wait(TimeOutStart))
                    throw new TimeoutException($"Mod {mod.MOD_ID}-{mod.MOD_NAME} didn't finish starting in {TimeOutStart.Seconds} seconds");
            }
        }

        public void EnvironmentLoaded(Diagram diagram)
        {
            foreach (var mod in Mods)
            {
                var task = Task.Run(() => mod.EnvironmentLoaded(diagram));
                if (!task.Wait(TimeOutEnv))
                    throw new TimeoutException($"Mod {mod.MOD_ID}-{mod.MOD_NAME} didn't finish loading in {TimeOutEnv.Seconds} seconds");
            }
        }

        public void AppShuttingDown()
        {
            foreach (var mod in Mods)
            {
                var task = Task.Run(() => mod.AppShuttingDown());
                if (!task.Wait(TimeOutShut))
                    throw new TimeoutException($"Mod {mod.MOD_ID}-{mod.MOD_NAME} didn't finish shutting down in {TimeOutShut.Seconds} seconds");
            }
        }

        private void LoadMod(string path)
        {
            var DLL = Assembly.LoadFile(path);

            var loadClasses = DLL.GetTypes().
                Where(r => r.Name.ToUpper() == "MOD" &&
                typeof(ModCore).IsAssignableFrom(r) &&
                r.IsClass &&
                !r.IsAbstract).ToList();
            if (loadClasses.Count() == 0)
                throw new FileLoadException("Couldn't find ModCore class named 'Mod'");
            if (loadClasses.Count() > 1)
                throw new FileLoadException("Unexpected amount of ModCore classes found");
            var loadClass = loadClasses[0];
            try
            {
                var mod = (ModCore)Activator.CreateInstance(loadClass);
                if (Mods.Any(r => r.MOD_ID == mod.MOD_ID))
                    throw new FileLoadException($"Mod ID conflict {mod.MOD_ID} is a duplicate");
                Mods.Add(mod);
            }
            catch(Exception e)
            {
                throw new FieldAccessException("Mod could not be loaded\r\n"+e);
            }
        }

    }
}
