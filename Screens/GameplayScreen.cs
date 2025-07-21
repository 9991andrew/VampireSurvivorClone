using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vampire_Survivor.Entities;
using Vampire_Survivor.Entities.Enemies;
using Vampire_Survivor.Misc;

namespace Vampire_Survivor.Screens
{
    public class GameplayScreen : GameScreen
    {
        private readonly SpriteBatch spriteBatch;
        private readonly ScreenManager screenManager;

        private Player player;
        private EnemySpawner enemySpawner;

        private Config playerConfig;

        public GameplayScreen(Microsoft.Xna.Framework.Game game, ScreenManager screens) : base(game)
        {
            screenManager = screens;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            
            playerConfig = new Config();
            playerConfig.pos = new Vector2(200, 200);
            playerConfig.origin = Vector2.Zero;
            playerConfig.health = 100;
            playerConfig.speed = 100;
            playerConfig.rotation =0f;
            playerConfig.scale = 1.5f;
            playerConfig.depth = 0;
            playerConfig.idleAnim = "Vampires1_Idle_full";
            playerConfig.walkAnim = "Vampires1_Walk_full";
            playerConfig.frameCountIdle = 4;
            playerConfig.framesPerSecIdle = 5;
            playerConfig.frameCountWalk = 6;
            playerConfig.framesPerSecWalk =7;
        }
        public override void LoadContent()
        {
            player = new Player();
            player.Initialize(Game.Content,
                playerConfig.pos,
                playerConfig.speed,
                playerConfig.origin,
                playerConfig.health,
                playerConfig.rotation,
                playerConfig.scale,
                playerConfig.depth,
                playerConfig.idleAnim,
                playerConfig.walkAnim,
                playerConfig.frameCountIdle,
                playerConfig.framesPerSecIdle,
                playerConfig.frameCountWalk,
                playerConfig.framesPerSecWalk,
                Entity.Direction.Down
            );
            enemySpawner = new EnemySpawner(player, Game.Content);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            enemySpawner.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            enemySpawner.Update(gameTime);
        }
    }
}
