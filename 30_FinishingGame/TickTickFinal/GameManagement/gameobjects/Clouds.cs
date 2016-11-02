using Microsoft.Xna.Framework;

class Clouds : GameObjectList
{
    int scalingFactor;
    int layerHeight;
    int minHeight;
    public Clouds(int layer = 0, string id = "",int layerHeight = 0, int minHeight = 0, int scalingFactor = 7)
        : base(layer, id)
    {
        this.scalingFactor = scalingFactor;
        this.layerHeight = layerHeight;
        this.minHeight = minHeight;
        for (int i = 0; i < 3; i++)
        {
            if (layerHeight == 0) {
                SpriteGameObject cloud = new HalfLockedSpriteGameObject("Backgrounds/spr_cloud_" + (GameEnvironment.Random.Next(5) + 1), 2, scalingFactor: scalingFactor);
                cloud.Position = new Vector2((float)GameEnvironment.Random.NextDouble() * GameEnvironment.Screen.X - cloud.Width / 2,
                    (float)GameEnvironment.Random.NextDouble() * GameEnvironment.Screen.Y - cloud.Height / 2);
                cloud.Velocity = new Vector2((float)((GameEnvironment.Random.NextDouble() * 2) - 1) * 20, 0);
                Add(cloud);
            }
            else {
                SpriteGameObject cloud = new HalfLockedSpriteGameObject("Backgrounds/spr_cloud_" + (GameEnvironment.Random.Next(5) + 1), 2, scalingFactor: scalingFactor);
                cloud.Position = new Vector2((float)GameEnvironment.Random.NextDouble() * GameEnvironment.Screen.X - cloud.Width / 2,
                    (float)GameEnvironment.Random.NextDouble() * (layerHeight - cloud.Height*1.3f) + minHeight-10 );
                cloud.Velocity = new Vector2((float)((GameEnvironment.Random.NextDouble() * 2) - 1) * 20, 0);
                Add(cloud);
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        foreach (GameObject obj in children)
        {
            SpriteGameObject c = obj as SpriteGameObject;
            if ((c.Velocity.X < 0 && c.Position.X + c.Width < 0) || (c.Velocity.X > 0 && c.Position.X > GameEnvironment.Screen.X))
            {
                Remove(c);
                SpriteGameObject cloud = new HalfLockedSpriteGameObject("Backgrounds/spr_cloud_" + (GameEnvironment.Random.Next(5) + 1),scalingFactor: scalingFactor);
                cloud.Velocity = new Vector2((float)((GameEnvironment.Random.NextDouble() * 2) - 1) * 20, 0);
                float cloudHeight = ((float)GameEnvironment.Random.NextDouble() * (layerHeight - cloud.Height * 1.3f) + minHeight - 10 );
                if (cloud.Velocity.X < 0)
                {
                    cloud.Position = new Vector2(GameEnvironment.Screen.X, cloudHeight);
                }
                else
                {
                    cloud.Position = new Vector2(-cloud.Width, cloudHeight);
                }
                Add(cloud);
                return;
            }
        }
    }
}

