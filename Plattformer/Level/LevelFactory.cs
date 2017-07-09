using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.Level
{
    public static class LevelFactory
    {
#if DEBUG
        public static LevelContainer CreateTestLevel(LevelCreationParam param)
        {
            var creator = new Debug_TestLevelCreator();
            creator.SetParams(param);
            return creator.Generate();
        }
#endif
    }
}
