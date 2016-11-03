using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

class Bomb : SpriteGameObject {
    TimeSpan timer;
    int lifeSpan;
    public bool remove;
    bool frozen;
    public Bomb() : base("sprites/Bomb", id: "bomb") {
        PerPixelCollisionDetection = true;
        remove = false;
        lifeSpan = 5;
    }
    public override void Reset() {
        base.Reset();
        remove = true;
    }
    public void Reset(Vector2 start, bool mirror) {
        velocity = new Vector2(500, -200);
        if (mirror) {
            velocity.X *= -1;
        }
        base.Reset();
        position = start;
        frozen = false;
        timer = new TimeSpan();
        remove = false;
    }
    public override void Update(GameTime gameTime) {
        if (!remove) {
            timer += gameTime.ElapsedGameTime;
            if (timer.Seconds > lifeSpan) {
                explode();
                visible = false;
                timer = new TimeSpan();
            }
            base.Update(gameTime);
            if (!frozen)
            {
                velocity.Y += 20;
            }
            tileCollide();
        }
        else {
            visible = false;
        }
        }
    
    public void explode() {
        remove = true;
        Explosion explosion = new Explosion(Position+Center);
        GameWorld.AddNew(explosion);
    }


    public void tileCollide() {
        TileField tiles = GameWorld.Find("tiles") as TileField;
        for (int y = 0; y < tiles.CellHeight - 1; y++) {
            for (int x = 0; x < tiles.CellWidth - 1; x++) {
                if (tiles.GetTileType(x, y) != TileType.Background && tiles.Get(x, y) != null) {
                    Tile collisionTile = tiles.Get(x, y) as Tile;
                    if (CollidesWith(collisionTile)) {
                        if (!collisionTile.Ice && !collisionTile.Hot)
                        {
                            Vector2 vectorFromTile = (collisionTile.Center + collisionTile.GlobalPosition) - (this.Center + this.GlobalPosition);
                            if (Math.Abs(vectorFromTile.X) > Math.Abs(vectorFromTile.Y) * (((float)collisionTile.Width) / (float)collisionTile.Height))
                            {
                                if(Math.Sign(vectorFromTile.X) == Math.Sign(velocity.X))
                                    velocity.X *= -.9f;
                                if (velocity.X > 0)
                                {
                                    position.X = collisionTile.Position.X + collisionTile.Width;
                                }
                                else
                                {
                                    position.X = collisionTile.Position.X - this.Width;
                                }
                            }
                            else
                            {
                                if (Math.Sign(vectorFromTile.Y) == Math.Sign(velocity.Y))
                                    velocity.Y *= -.9f;
                                if (velocity.Y > 0)
                                {
                                    position.Y = collisionTile.Position.Y + collisionTile.Width;
                                }
                                else
                                {
                                    position.Y = collisionTile.Position.Y - this.Height;
                                }
                            }
                            return;
                        }
                        else if (collisionTile.Ice)
                        {
                            velocity = Vector2.Zero;
                            frozen = true;
                        }
                        else if (collisionTile.Hot)
                        {
                            explode();
                        }
                    }
                }
            }
        }
    }
}
