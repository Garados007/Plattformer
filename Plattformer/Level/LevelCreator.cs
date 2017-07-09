using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.Level
{
    public abstract class LevelCreator
    {
        public abstract void SetParams(LevelCreationParam parameters);

        public abstract LevelContainer Generate();
    }

    public class LevelCreationParam
    {
        public int Width { get; set; }

        public int Height { get; set; }
    }
}
