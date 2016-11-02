using Microsoft.Xna.Framework;
using System;

class PatrollingEnemy : AnimatedGameObject {
    protected float waitTime;
    protected bool inJump;
    protected bool injump;
    protected float landingHeight;
    public PatrollingEnemy() {
        injump = false;
        waitTime = 0.0f;
        velocity.X = 120;
        LoadAnimation("Sprites/Flame/spr_flame@9", "default", true);
        PlayAnimation("default");
        inJump = false;
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
        if (injump) {
            velocity.Y += 2;
            if (position.Y > landingHeight) {
                position.Y = landingHeight;
                velocity.Y = 0;
                injump = false;
            }
        }
        if (waitTime > 0) {
            waitTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (waitTime <= 0.0f) {
                TurnAround();
            }
        }
        else {
            TileField tiles = GameWorld.Find("tiles") as TileField;
            float posX = BoundingBox.Left;
            if (!Mirror) {
                posX = BoundingBox.Right;
            }
            int tileX = (int)Math.Floor(posX / tiles.CellWidth);
            int tileY = (int)Math.Floor(position.Y / tiles.CellHeight);
            if ((tiles.GetTileType(tileX, tileY - 1) == TileType.Normal ||
                tiles.GetTileType(tileX, tileY) == TileType.Background) && !injump) {
                velocity.Y = -120;
                inJump = true;
                //waitTime = 0.5f;
                //velocity.X = 0.0f;
                float jumpLength = 0;
                float jumpHeight = 0;
                float vel = -120f;
                while (true) {
                    jumpLength += velocity.X / 60;
                    jumpHeight += vel / 60;
                    vel += 2;
                    if (jumpHeight >= 0) {
                        TileType t = tiles.GetTileType(tileX + (int)Math.Floor(jumpLength / tiles.CellWidth), tileY);
                        if (tiles.GetTileType(tileX + (int)Math.Floor(jumpLength / tiles.CellWidth), tileY) == TileType.Normal) {
                            velocity.Y = -120.0f;
                            injump = true;
                            landingHeight = position.Y;
                            break;
                        }
                        else {
                            waitTime = 0.5f;
                            velocity.X = 0.0f;
                            break;
                        }
                    }
                }
            }
        }

        if (inJump) {
            velocity.Y += 20;
        }
        CheckPlayerCollision();
        CheckBombCollision();
    }

    public void CheckBombCollision() {
        Bomb bomb = GameWorld.Find("bomb") as Bomb;
        if (bomb == null) {
            return;
        }
        if (CollidesWith(bomb)) {
            bomb.explode();
            visible = false;
        }
    }

    public void CheckPlayerCollision() {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player)) {
            player.Die(false);
        }
    }

    public void TurnAround() {
        Mirror = !Mirror;
        velocity.X = 120;
        if (Mirror) {
            velocity.X *= -1;
        }
    }
}