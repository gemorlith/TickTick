using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

partial class Player : AnimatedGameObject
{
    protected Vector2 startPosition;
    protected bool isOnTheGround;
    protected float previousYPosition;
    protected bool isAlive;
    protected bool exploded;
    protected bool finished;
    protected bool walkingOnIce, walkingOnHot;
    protected Bomb myBomb;
    protected bool holdingUp, movingThroughPlatform;

    public Player(Vector2 start) : base(2, "player")
    {
        LoadAnimation("Sprites/Player/spr_idle", "idle", true); 
        LoadAnimation("Sprites/Player/spr_run@13", "run", true, 0.05f);
        LoadAnimation("Sprites/Player/spr_jump@14", "jump", false, 0.05f); 
        LoadAnimation("Sprites/Player/spr_celebrate@14", "celebrate", false, 0.05f);
        LoadAnimation("Sprites/Player/spr_die@5", "die", false);
        LoadAnimation("Sprites/Player/spr_explode@5x5", "explode", false, 0.04f);
        myBomb = new Bomb();
        startPosition = start;
        Reset();
    }

    public override void Reset()
    {
        position = startPosition;
        velocity = Vector2.Zero;
        isOnTheGround = true;
        isAlive = true;
        exploded = false;
        finished = false;
        walkingOnIce = false;
        walkingOnHot = false;
        movingThroughPlatform = false;
        holdingUp = false;
        PlayAnimation("idle");
        previousYPosition = BoundingBox.Bottom;
    }

    public override void HandleInput(InputHelper inputHelper) {
        float walkingSpeed = 400;
        if (walkingOnIce) {
            walkingSpeed *= 1.5f;
        }
        if (!isAlive) {
            return;
        }
        if (inputHelper.IsKeyDown(Keys.Left)) {
            velocity.X = -walkingSpeed;
        }
        else if (inputHelper.IsKeyDown(Keys.Right)) {
            velocity.X = walkingSpeed;
        }
        else if (!walkingOnIce && isOnTheGround) {
            velocity.X = 0.0f;
        }
        if (velocity.X != 0.0f) {
            Mirror = velocity.X < 0;
        }
        if ((inputHelper.KeyPressed(Keys.Space) || inputHelper.KeyPressed(Keys.Up))) {
            if (isOnTheGround) {
                Jump();
            }
        }
        if (inputHelper.IsKeyDown(Keys.Space) || inputHelper.IsKeyDown(Keys.Up)) {
            holdingUp = true;
        } else {
            holdingUp = false;
        }
        if (inputHelper.IsKeyDown(Keys.Down)) {
            movingThroughPlatform = true;
        }
        else {
            movingThroughPlatform = false;
        }
        if (inputHelper.KeyPressed(Keys.M)) {
            if (GameWorld.Find("bomb") != null) {
                Bomb bomb = GameWorld.Find("bomb") as Bomb;
                if (!bomb.remove){
                    bomb.explode();
                }
                bomb.Reset(GlobalPosition - new Vector2(0, Height / 4 * 3), Mirror);
            }
            else {
                Bomb bomb = new Bomb();
                GameWorld.Add(bomb);
                bomb.Reset(GlobalPosition - new Vector2(0, Height / 4 * 3),Mirror);
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (!finished && isAlive)
        {
            if (isOnTheGround)
            {
                if (velocity.X == 0)
                {
                    PlayAnimation("idle");
                }
                else
                {
                    PlayAnimation("run");
                }
            }
            else if (velocity.Y < 0)
            {
                PlayAnimation("jump");
            }

            TimerGameObject timer = GameWorld.Find("timer") as TimerGameObject;
            if (walkingOnHot)
            {
                timer.Multiplier = 2;
            }
            else if (walkingOnIce)
            {
                timer.Multiplier = 0.5;
            }
            else
            {
                timer.Multiplier = 1;
            }

            TileField tiles = GameWorld.Find("tiles") as TileField;
            if (BoundingBox.Top >= tiles.Rows * tiles.CellHeight)
            {
                Die(true);
            }
        }

        DoPhysics(holdingUp, movingThroughPlatform);
    }

    public void Explode()
    {
        if (!isAlive || finished)
        {
            return;
        }
        isAlive = false;
        exploded = true;
        velocity = Vector2.Zero;
        position.Y += 15;
        PlayAnimation("explode");
    }

    public void Die(bool falling)
    {
        if (!isAlive || finished)
        {
            return;
        }
        isAlive = false;
        velocity.X = 0.0f;
        if (falling)
        {
            GameEnvironment.AssetManager.PlaySound("Sounds/snd_player_fall");
        }
        else
        {
            velocity.Y = -900;
            GameEnvironment.AssetManager.PlaySound("Sounds/snd_player_die");
        }
        PlayAnimation("die");
    }

    public bool IsAlive
    {
        get { return isAlive; }
    }

    public bool Finished
    {
        get { return finished; }
    }

    public void LevelFinished()
    {
        finished = true;
        velocity.X = 0.0f;
        PlayAnimation("celebrate");
        GameEnvironment.AssetManager.PlaySound("Sounds/snd_player_won");
    }
}
