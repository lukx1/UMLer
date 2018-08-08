using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLer.Special
{
    public abstract class ModCore
    {
        public abstract int MOD_ID { get; }
        public abstract string MOD_NAME { get; }
        public abstract int VERSION { get; }

        public abstract void AppStarting();

        public abstract void EnvironmentLoaded(Diagram diagram);

        public abstract void AppShuttingDown();

    }
}
