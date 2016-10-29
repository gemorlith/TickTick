﻿using Microsoft.Xna.Framework;

public class Camera {

    protected SpriteGameObject followObject;
    public int levelwidth;
    protected Vector2 cameraPosition;
    public int levelheight;
    public Camera() {
        cameraPosition = new Vector2(0,0);
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
            cameraPosition = value;
        }
    }
    public void Reset() {
        CameraPosition = Vector2.Zero;
    }

    public void Update(GameTime gameTime) {
        if (followObject != null) {
            if ((followObject.GlobalPosition.X < levelwidth - 0.5 * GameEnvironment.Screen.X - followObject.Width * 0.5) &&
                followObject.GlobalPosition.X > 0.5 * GameEnvironment.Screen.X - followObject.Width * 0.5) {
                cameraPosition.X = (int)followObject.GlobalPosition.X - (GameEnvironment.Screen.X * 0.5f - followObject.Width * 0.5f);
            }
            if ((followObject.GlobalPosition.Y < levelheight - 0.5 * GameEnvironment.Screen.Y - followObject.Height * 0.5) &&
                followObject.GlobalPosition.Y > 0.5 * GameEnvironment.Screen.Y - followObject.Height * 0.5) {
                cameraPosition.Y = (int)followObject.GlobalPosition.Y - (GameEnvironment.Screen.Y* 0.5f - followObject.Height * 0.5f);
            }
        }
    }
}
