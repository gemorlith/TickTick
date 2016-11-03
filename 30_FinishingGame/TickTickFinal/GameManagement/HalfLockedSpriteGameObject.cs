using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class HalfLockedSpriteGameObject:SpriteGameObject {
    int scalingFactor;
    public HalfLockedSpriteGameObject(string assetName, int layer = 0, string id = "", int sheetIndex = 0, int scalingFactor = 7):base(assetName,layer,id,sheetIndex) {
        this.scalingFactor = scalingFactor;
    }
    public override Vector2 CameraPosition {
        get {
            return GlobalPosition-(GameEnvironment.camera.CameraPosition)/scalingFactor;
        }
    }


}

