﻿using Microsoft.Xna.Framework;

class Sparky : AnimatedGameObject
{
    protected float idleTime;
    protected float yOffset;
    protected float initialY;

    public Sparky(float initialY)
    {
        LoadAnimation("Sprites/Sparky/spr_electrocute@6x5", "electrocute", false);
        LoadAnimation("Sprites/Sparky/spr_idle", "idle", true);
        PlayAnimation("idle");
        this.initialY = initialY;
        posReset = true;
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        softReset();
    }
    public void softReset()
    {
        idleTime = (float)GameEnvironment.Random.NextDouble() * 5;
        position.Y = initialY;
        yOffset = 120;
        velocity = Vector2.Zero;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (idleTime <= 0)
        {
            PlayAnimation("electrocute");
            if (velocity.Y != 0)
            {
                // falling down
                yOffset -= velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (yOffset <= 0)
                {
                    velocity.Y = 0;
                }
                else if (yOffset >= 120.0f)
                {
                    softReset();
                }
            }
            else if (Current.AnimationEnded)
            {
                velocity.Y = -60;
            }

            CheckPlayerCollision();
        }
        else
        {
            PlayAnimation("idle");
            idleTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Player player = GameWorld.Find("player") as Player;
            if (visible) {
                if (CollidesWithBottomOfOther(player))
                {
                    visible = false;
                    player.Jump(1000);
                }
                else
                {
                    CheckPlayerCollision();
                }
            }
            if (idleTime <= 0.0f)
            {
                velocity.Y = 300;
            }
        }
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

    public void CheckPlayerCollision()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player) && idleTime <= 0 && visible)
        {
            player.Die(false);
        }
    }
}
