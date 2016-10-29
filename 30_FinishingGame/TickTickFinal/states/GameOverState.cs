using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameOverState : GameObjectList
{
    protected PlayingState playingState;
    Button quitButton;

    public GameOverState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState") as PlayingState;
        SpriteGameObject overlay = new LockedSpriteGameObject("Overlays/spr_gameover");
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        Add(overlay);
        quitButton = new Button("Sprites/spr_button_quit", 100);
        quitButton.Position = new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 10);
    }

    public void setFocus() {
        playingState.setFocus();
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
        playingState.Reset();
        GameEnvironment.GameStateManager.SwitchTo("playingState");
    }

    public override void Update(GameTime gameTime)
    {
        playingState.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }
}