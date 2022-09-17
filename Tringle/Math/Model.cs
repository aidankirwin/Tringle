using OpenTK.Mathematics;

namespace Tringle
{
    public class Model
    {
        public static Matrix4 Matrix4Translate(Vector3 translation)
        {
            var translatedMatrix = Matrix4.CreateTranslation(translation);

            return translatedMatrix;
        }

        public static Matrix4 Matrix4Rotate(Rotation rotation, Mesh mesh)
        {
            // Rotations are about the origin

            // Get max and min
            Vector3 max = mesh.MaxVertex;
            Vector3 min = mesh.MinVertex;

            // Get average of each component
            float avgX = (max.X + min.X) / 2;
            float avgY = (max.Y + min.Y) / 2;
            float avgZ = (max.Z + min.Z) / 2;

            // Move object to center
            var moveToCenter = Model.Matrix4Translate(new Vector3(-avgX, -avgY, -avgZ));

            // Rotate
            Matrix4 rotate;
            if(rotation.Axis == Rotation.Axes.Z)
            {
                rotate = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Angle));
            }
            else if(rotation.Axis == Rotation.Axes.X)
            {
                rotate = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.Angle));
            }
            else
            {
                rotate = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Angle));
            }

            // Shift back to original spot
            var moveBack = Model.Matrix4Translate(new Vector3(avgX, avgY, avgZ));

            // Get rotation matrix
            var rotatedMatrix = moveToCenter * rotate * moveBack;

            return rotatedMatrix;
        }

        public static Matrix4 Matrix4Scale(Vector2 size)
        {
            var scaledMatrix = Matrix4.CreateScale(size.X, size.Y, 1.0f);

            return scaledMatrix;
        }
    }
}