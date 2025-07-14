using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace Vampire_Survivor.Screens
{
    public class MainMenuScreen : Screen
    {
        private readonly SpriteBatch spriteBatch;
        private readonly ScreenManager screenManager;
        private Microsoft.Xna.Framework.Game game;
        private SpriteFont font;
        public MainMenuScreen(Microsoft.Xna.Framework.Game game, ScreenManager screens)
        {
            this.game = game;
            this.screenManager = screens;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public override void LoadContent()
        {
            font = game.Content.Load<SpriteFont>("DefaultFont");

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                screenManager.LoadScreen(new GameplayScreen(game, screenManager));
            }
        }
        public override void Draw(GameTime gameTime)
        {
            int centerX = game.GraphicsDevice.Viewport.Width / 2;
            int centerY = game.GraphicsDevice.Viewport.Height / 2;
            
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Press ENTER to Start", new Vector2(centerX, centerY), Color.White);
            spriteBatch.End();
        }
    }
}