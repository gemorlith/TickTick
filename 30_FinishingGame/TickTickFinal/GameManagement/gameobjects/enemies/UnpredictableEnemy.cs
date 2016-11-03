using System;
using Microsoft.Xna.Framework;

class UnpredictableEnemy : PatrollingEnemy
{
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (waitTime > 0 || GameEnvironment.Random.NextDouble() > 0.01 || inJump)
        {
            return;
        }
        TurnAround();
        velocity.X = velocity.X * (float)GameEnvironment.Random.NextDouble() * 3.0f;       
    }
}

