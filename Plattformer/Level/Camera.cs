using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.Level
{
    public class Camera
    {

        public float X { get; set; }

        public float Y { get; set; }

        float zoom = 1; //[local zoom] in-game zoom, depends on surroundings
        public float Zoom
        {
            get => zoom;
            set
            {
                if (value < 1e-10f || value > 1e10f) throw new ArgumentOutOfRangeException("Zoom");
                zoom = value;
            }
        }

        float distance = 4; //[global zoom] all-game zoom, adjustable in settings, the normal hight of surface
        public float Distance
        {
            get => distance;
            set
            {
                if (value < 1e-5f || value > 1e5f) throw new ArgumentOutOfRangeException("Distance");
                distance = value;
            }
        }

        public void MeasureDisplay(float aspect, out float left, out float top, out float right, out float bottom)
        {
            //aspect / 1 = width / height
            //orientation: top +, bottom -, left -, right +
            var scal = Zoom * Distance;
            var off = scal * .5f;
            top = Y + off;
            bottom = Y - off;
            off *= aspect;
            left = X - off;
            right = X + off;
        }
    }
}
