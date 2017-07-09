using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.Level
{
    public class LevelContainer
    {
        public LevelContainer()
        {
            this.Sprites = new List<Sprite>();
            this.Camera = new Camera();
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public LevelCell[,] Cells { get; private set; }

        public List<Sprite> Sprites { get; private set; }

        public Camera Camera { get; private set; }

        public void SetDimension(int width, int height)
        {
            var cells = new LevelCell[width, height];
            for (int x = 0; x < width; ++x)
                for (int y = 0; y < height; ++y)
                    cells[x, y] = new LevelCell();
            if (Cells != null)
            {
                int w = Math.Min(width, Width), h = Math.Min(height, Height);
                for (int x = 0; x < w; ++x)
                    for (int y = 0; y < h; ++y)
                        cells[x, y].CopyFrom(Cells[x, y]);
            }
            Cells = cells;
            Width = width;
            Height = height;
        }
    }
}
