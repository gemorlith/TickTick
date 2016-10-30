using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

class Bomb : SpriteGameObject {
    TimeSpan timer;
    int lifeSpan;
    bool remove;
    public Bomb() : base("sprites/Bomb",id: "bomb") {
        remove = false;
        lifeSpan = 3;
    }
    public void Reset(Vector2 start) {
        base.Reset();
        position = start;
        velocity = new Vector2(300, -100);
        timer = new TimeSpan();
        remove = false;
    }
    public override void Update(GameTime gameTime) {
        if (!remove) {
            timer += gameTime.ElapsedGameTime;
            if (timer.Seconds > lifeSpan) {
                remove = true;
                visible = false;
                timer = new TimeSpan();
            }
            base.Update(gameTime);
            velocity.Y += 5;
            TileField tiles = GameWorld.Find("tiles") as TileField;
            for (int y = 0; y < tiles.CellHeight - 1; y++) {
                for (int x = 0; x < tiles.CellWidth - 1; x++) {
                    if (tiles.GetTileType(x, y) != TileType.Background && tiles.Get(x, y) != null) {
                        if (CollidesWith(tiles.Get(x, y) as SpriteGameObject)) {
                            velocity.Y = -velocity.Y*0.8f;
                        }
                    }
                }
            }
        }
    }
}
