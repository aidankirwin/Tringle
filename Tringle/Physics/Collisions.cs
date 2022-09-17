using OpenTK.Mathematics;

namespace Tringle
{
    public class Collisions
    {
        public static bool Intersect(AABB a, AABB b)
        {
            return (a.Min.X <= b.Max.X && a.Max.X >= b.Min.X) &&
                   (a.Min.Y <= b.Max.Y && a.Max.Y >= b.Min.Y) &&
                   (a.Min.Z <= b.Max.Z && a.Max.Z >= b.Min.Z);
        }

        /// <summary>
        /// Apply transformations to AABB; consider position, size, and rotation while maintaining axis-alignment.
        /// </summary>
        /// <param name="transform">A <see cref="Transforms"/> object.</param>
        /// <param name="mesh">A <see cref="Mesh"/> object.</param>
        /// <param name="box">A non-transformed AABB.</param>
        /// <returns>A transformed AABB.</returns>
        public static AABB TransformAABB(Transforms transform, Mesh mesh, AABB box)
        {
            // Create transformation matrix
            var model = Matrix4.Identity;

            // Get translation for later
            var translate = transform.Position;

            // Get transformations
            var rotate = transform.Rotation;
            var size = transform.Size;

            // Get transform matrices
            var rotation = Model.Matrix4Rotate(rotate, mesh);
            var scale = Model.Matrix4Scale(size);

            // Apply transformations
            var transformations = rotation * scale;
            model *= transformations;

            // Create temp vals to hold each new transformed min / max
            float tempMin, tempMax;

            // Declare min and max arrays for easy reference
            float[] min = { box.Min.X, box.Min.Y, box.Min.Z };
            float[] max = { box.Max.X, box.Max.Y, box.Max.Z };

            // Create min and max arrays for the new AABB
            float[] newMin = new float[3];
            float[] newMax = new float[3];

            // Apply translations to new min / max
            newMin[0] = newMax[0] = translate.X;
            newMin[1] = newMax[1] = translate.Y;
            newMin[2] = newMax[2] = translate.Z;

            // Find new extreme points using Jim Arvo's Transforming AABB algorithm
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tempMin = model[j, i] * min[j];
                    tempMax = model[j, i] * max[j];

                    if (tempMin < tempMax)
                    {
                        newMin[i] += tempMin;
                        newMax[i] += tempMax;
                    }
                    else
                    {
                        newMin[i] += tempMax;
                        newMax[i] += tempMin;
                    }
                }
            }

            // Copy results into new box and return
            return new AABB(new Vector3(newMax[0], newMax[1], newMax[2]),
                            new Vector3(newMin[0], newMin[1], newMin[2]));
        }
    }
}
