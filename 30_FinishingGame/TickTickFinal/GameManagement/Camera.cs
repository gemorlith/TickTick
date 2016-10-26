using Microsoft.Xna.Framework;

public class Camera {
    protected Rectangle cameraPosition;
    public Camera() {
        cameraPosition = new Rectangle(100, 100, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
    }
    public Vector2 CameraPosition {
        get {
            return new Vector2(cameraPosition.X,cameraPosition.Y);
        }
        set {
            cameraPosition.X = (int)value.X;
            cameraPosition.Y = (int)value.Y;
        }
    }
}
