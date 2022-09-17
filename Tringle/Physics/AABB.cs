using OpenTK.Mathematics;

namespace Tringle
{
    public class AABB : Component
    {
        public Vector3 Max;
        public Vector3 Min;
        
        public AABB(Vector3 max, Vector3 min)
        {
            this.Max = max;
            this.Min = min;
        }

        public AABB()
        {
            Max = new Vector3(1.0f, 1.0f, 0.0f);
            Min = new Vector3(0.0f);
        }
    }
}
