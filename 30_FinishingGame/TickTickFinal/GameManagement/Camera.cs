using Microsoft.Xna.Framework;

public class Camera {
    protected Rectangle cameraPosition;
    protected SpriteGameObject followObject;
    public int levelwidth;
    public int levelheight;
    public Camera() {
        cameraPosition = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
        levelwidth = GameEnvironment.Screen.X;
        levelheight = GameEnvironment.Screen.Y;
    }
    
    public void setlevelsize(int width, int height) {
        levelwidth = width;
        levelheight = height;
    }
    public SpriteGameObject FollowObject{
        set{
            followObject = value;
        }
        }
    public Vector2 CameraPosition {
        get {
            return new Vector2(cameraPosition.X,cameraPosition.Y);
        }
        set {
            followObject = null;
            cameraPosition.X = (int)value.X;
            cameraPosition.Y = (int)value.Y;
        }
    }
    public void Reset() {
        CameraPosition = Vector2.Zero;
    }

    public void Update(GameTime gameTime) {
        if (followObject != null) {
            if ((followObject.Position.X < /*levelwidth -*/ 0.5 * GameEnvironment.Screen.X - followObject.Width / 2) &&
                followObject.Position.X > 0.5 * GameEnvironment.Screen.X - followObject.Width / 2) {
                cameraPosition.X = (int)followObject.Position.X;
            }
            if ((followObject.Position.Y < /*levelheight -*/ 0.5 * GameEnvironment.Screen.Y - followObject.Height / 2) &&
                followObject.Position.Y > 0.5 * GameEnvironment.Screen.Y - followObject.Height / 2) {
                cameraPosition.X = (int)followObject.Position.Y;
            }
        }
    }
}
