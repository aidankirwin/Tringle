using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tringle
{
    public struct Rotation
    {
        public float Angle;
        public Axes Axis;

        public enum Axes
        {
            X,
            Y,
            Z
        }

        public Rotation(float angle, Axes axis)
        {
            this.Angle = angle;
            this.Axis = axis;
        }

        public Rotation(float angle)
        {
            this.Angle = angle;
            this.Axis = Axes.Z;
        }
    }
}
