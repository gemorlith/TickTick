using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class LockedSpriteGameObject:SpriteGameObject {
    public LockedSpriteGameObject(string assetName, int layer, string id, int sheetIndex):base(assetName,layer,id,sheetIndex) {

    }
    public override Vector2 CameraPosition {
        get {
            return GlobalPosition - GameEnvironment.camera.CameraPosition;
        }
    }

}

