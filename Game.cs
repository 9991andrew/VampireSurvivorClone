using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Vampire_Survivor.Screens;

namespace Vampire_Survivor
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private ScreenManager screens;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // 1) Create ScreenManager with Services
            screens = new ScreenManager();

            // 2) Add it so MonoGame calls its Update/Draw automatically
            Components.Add(screens);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // 3) Kick off with the main‐menu screen
            var mainMenu = new MainMenuScreen(this, screens);
            screens.LoadScreen(mainMenu);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            // Global “escape to exit”
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Let the ScreenManager (and active screen) run their Update()
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Let the ScreenManager (and active screen) run their Draw()
            base.Draw(gameTime);
        }
    }
}
