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
        EarthRight,
        EarthPlant,
        EarthBottom,
        EarthBottomLeft,
        EarthBottomRight,
        EarthBottomPlant,
        Door,
        Step1,
        Step2,
        StepLeft,
        StepLeftStart,
        StepRight,
        StepRightStart,
        EarthTop,
        EarthTopLeft,
        EarthTopRight,
        EarthTopPlant,
        EarthTopLeftStart,
        EarthTopRightStart,
        Water,
        WaterTop,
        WaterEarth1,
        WaterEarth2,
        WaterEarth3,
        WaterEarth4,
        WaterEarthTop1,
        WaterEarthTop2,
        WaterEarthTop3,
        WaterEarthTop4,
        SmallBush,
        Bush,
        Flower,
        StatueFaceTopLeft,
        StatueFaceTopRight,
        StatueFaceBottomLeft,
        StatueFaceBottomRight,
        StatueTile1,
        StatueTile2,
        StatueTile3,
        StatueTile4,
        StatueTile5,
        StatueBushTopLeft,
        StatueBushTop,
        StatueBushTopRight,
        StatueBushLeft,
        StatueBushMiddle,
        StatueBushRight,
        StatueBushBottomLeft,
        StatueBushBottom,
        StatueBushBottomRight,
        StatueLeaves1,
        StatueLeaves2,
        StatueSpecBush1,
        StatueSpecBush2,
    }
}
