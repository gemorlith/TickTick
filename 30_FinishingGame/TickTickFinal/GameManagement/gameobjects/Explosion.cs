using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

class Explosion:AnimatedGameObject {
    TimeSpan timer;
    public Explosion(Vector2 Position) : base(2, "explosion") {
        LoadAnimation("Sprites/Player/spr_explode@5x5", "explode", false, 0.04f);
        PlayAnimation("explode");
        position = Position - Center;
        //position = Position;
        timer = new TimeSpan();
        this.Origin = Vector2.Zero;
    }
    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
        timer += gameTime.ElapsedGameTime;
        if (timer.TotalSeconds >= 1f) {
            GameWorld.Remove(this);
        }
    }
}
