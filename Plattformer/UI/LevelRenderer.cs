using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.UI
{
    public abstract class LevelRenderer : RenderLayer
    {
        public Level.LevelContainer Level { get; set; }
    }
}
