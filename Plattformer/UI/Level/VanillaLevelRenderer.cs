using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plattformer.Level;

namespace Plattformer.UI.Level
{
    public class VanillaLevelRenderer : LevelRendererBase
    {
        Bitmap tileset;
#if DEBUG
        Pen debug_framepen;
#endif

        public override void LoadRessources()
        {
            base.LoadRessources();
            tileset = Properties.Resources.Image_Tileset;
#if DEBUG
            debug_framepen = new Pen(Color.FromArgb(0x80, Color.Black), 1);
#endif
        }

        public override void Dispose()
        {
            base.Dispose();
#if DEBUG
            debug_framepen.Dispose();
#endif
        }

        protected override void renderLevelCell(Graphics g, LevelCell cell, RectangleF bounds, int time)
        {
            switch (cell.ImageType)
            {
                case LevelCellImageType.EarthTopLeftStart:
                    g.DrawImage(tileset, bounds, new RectangleF(66, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthTopRightStart:
                    g.DrawImage(tileset, bounds, new RectangleF(130, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthLeft:
                    g.DrawImage(tileset, bounds, new RectangleF(194, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthPlant:
                    g.DrawImage(tileset, bounds, new RectangleF(258, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.Earth:
                    g.DrawImage(tileset, bounds, new RectangleF(322, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthRight:
                    g.DrawImage(tileset, bounds, new RectangleF(386, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthBottomLeft:
                    g.DrawImage(tileset, bounds, new RectangleF(514, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthBottomPlant:
                    g.DrawImage(tileset, bounds, new RectangleF(578, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthBottom:
                    g.DrawImage(tileset, bounds, new RectangleF(642, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthBottomRight:
                    g.DrawImage(tileset, bounds, new RectangleF(706, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.Door:
                    g.DrawImage(tileset, bounds, new RectangleF(770, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.SmallBush:
                    g.DrawImage(tileset, bounds, new RectangleF(834, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.Bush:
                    g.DrawImage(tileset, bounds, new RectangleF(898, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.Flower:
                    g.DrawImage(tileset, bounds, new RectangleF(962, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StepLeftStart:
                    g.DrawImage(tileset, bounds, new RectangleF(1026, 2, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StepRightStart:
                    g.DrawImage(tileset, bounds, new RectangleF(1090, 2, 62, 62), GraphicsUnit.Pixel);
                    break;

                case LevelCellImageType.EarthTopLeft:
                    g.DrawImage(tileset, bounds, new RectangleF(130, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthTopPlant:
                    g.DrawImage(tileset, bounds, new RectangleF(194, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthTop:
                    g.DrawImage(tileset, bounds, new RectangleF(258, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.EarthTopRight:
                    g.DrawImage(tileset, bounds, new RectangleF(322, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StatueTile1:
                    g.DrawImage(tileset, bounds, new RectangleF(386, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StatueTile2:
                    g.DrawImage(tileset, bounds, new RectangleF(450, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StatueTile3:
                    g.DrawImage(tileset, bounds, new RectangleF(514, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StatueTile4:
                    g.DrawImage(tileset, bounds, new RectangleF(578, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StatueTile5:
                    g.DrawImage(tileset, bounds, new RectangleF(642, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StatueFaceTopLeft:
                    g.DrawImage(tileset, bounds, new RectangleF(706, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StatueFaceTopRight:
                    g.DrawImage(tileset, bounds, new RectangleF(770, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StatueFaceBottomLeft:
                    g.DrawImage(tileset, bounds, new RectangleF(834, 130, 62, 62), GraphicsUnit.Pixel);
                    break;
                case LevelCellImageType.StatueFaceBottomRight:
                    g.DrawImage(tileset, bounds, new RectangleF(898, 130, 62, 62), GraphicsUnit.Pixel);
                    break;

            }

#if DEBUG
            g.DrawRectangle(debug_framepen, Rectangle.Round(bounds));
#endif
        }

        protected override void renderSprite(Graphics g, Sprite sprite, RectangleF bounds, int time)
        {
        }
    }
}
