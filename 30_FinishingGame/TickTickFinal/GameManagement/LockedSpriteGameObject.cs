using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class LockedSpriteGameObject:SpriteGameObject {
    public LockedSpriteGameObject(string assetName, int layer = 0, string id = "", int sheetIndex = 0):base(assetName,layer,id,sheetIndex) {

    }
    public override Vector2 CameraPosition {
        get {
            return GlobalPosition;
        }
    }

}

