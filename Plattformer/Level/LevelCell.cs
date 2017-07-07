using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.Level
{
    public class LevelCell
    {
        public LevelCellImageType ImageType { get; set; }

        public bool Solid { get; set; }

        public bool Coin { get; set; }

        public void CopyFrom(LevelCell cell)
        {
            ImageType = cell.ImageType;
            Solid = cell.Solid;
            Coin = cell.Coin;
        }
    }

    public enum LevelCellImageType
    {
        None,
        Earth,
        EarthLeft,
        EarthRight
    }
}
