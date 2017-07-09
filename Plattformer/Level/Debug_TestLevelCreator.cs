using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.Level
{
#if DEBUG
    public class Debug_TestLevelCreator : LevelCreator
    {
        LevelCreationParam param;

        public override LevelContainer Generate()
        {
            var level = new LevelContainer();
            level.SetDimension(param.Width, param.Height);
            for (int y = 0; y < param.Height / 5; ++y)
                for (int x = 0; x < param.Width; ++x)
                    level.Cells[x, y].ImageType = LevelCellImageType.Earth;

            return level;
        }

        public override void SetParams(LevelCreationParam parameters)
        {
            param = parameters;
        }
    }
#endif
}
