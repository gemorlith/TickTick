using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class LevelFinishedState : GameObjectList
{
    protected PlayingState playingState;
    Button quitButton;

    public LevelFinishedState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
        SpriteGameObject overlay = new LockedSpriteGameObject("Overlays/spr_welldone");
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        Add(overlay);
        quitButton = new Button("Sprites/spr_button_quit", 100);
        quitButton.Position = new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 10);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        quitButton.HandleInput(inputHelper);
        if (quitButton.Pressed) {
            Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }
        if (!inputHelper.KeyPressed(Keys.Space))
        {
            return;
        }
        GameEnvironment.GameStateManager.SwitchTo("playingState");
        (playingState as PlayingState).NextLevel();

    }

    public void setFocus() {
        playingState.setFocus();
    }

    public override void Update(GameTime gameTime)
    {
        //playingState.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }
}