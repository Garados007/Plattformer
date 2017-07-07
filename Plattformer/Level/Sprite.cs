using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.Level
{
    public class Sprite
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public SpriteSkinType Skin { get; set; }
    }

    public enum SpriteSkinType
    {
        None,
        Player
    }
}
