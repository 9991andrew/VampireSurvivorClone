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

namespace Vampire_Survivor.Screens
{
    public class GameplayScreen : GameScreen
    {
        private readonly SpriteBatch spriteBatch;
        private readonly ScreenManager screenManager;

        private Player player;
        private SimpleEnemy enemy;
        private struct Config
        {
           public Vector2 pos;
            public int speed;
            public Vector2 origin;
            public int health;
            public float rotation;
            public float scale;
            public float depth;
            public string idleAnim;
            public string walkAnim;
            public int frameCountIdle;
            public int framesPerSecIdle;
            public int frameCountWalk;
            public int framesPerSecWalk;
        }
        private Config playerConfig;
        private Config enemyConfig;

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

            enemyConfig = new Config();
            enemyConfig.pos = new Vector2(200, 200);
            enemyConfig.origin = Vector2.Zero;
            enemyConfig.health = 10;
            enemyConfig.speed = 100;
            enemyConfig.rotation = 0f;
            enemyConfig.scale = 1.5f;
            enemyConfig.depth = 0;
            enemyConfig.idleAnim = "Vampires2_Idle_full";
            enemyConfig.walkAnim = "Vampires2_Walk_full";
            enemyConfig.frameCountIdle = 4;
            enemyConfig.framesPerSecIdle = 5;
            enemyConfig.frameCountWalk = 6;
            enemyConfig.framesPerSecWalk = 7;
        }
        public override void LoadContent()
        {
            player = new Player();
            player.Initialize(
                Game.Content,
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
                Entity.Direction.Down,         
                Entity.AnimationState.Idle         
            );
            //TODO: make 15 not 15 and some other value.
            enemy = new SimpleEnemy(player, enemyConfig.health, 15);
            enemy.Initialize(Game.Content,
                            enemyConfig.pos,
                            enemyConfig.speed,
                            enemyConfig.origin,
                            enemyConfig.health,
                            enemyConfig.rotation,
                            enemyConfig.scale,
                            enemyConfig.depth,
                            enemyConfig.idleAnim,
                            enemyConfig.walkAnim,
                            enemyConfig.frameCountIdle,
                            enemyConfig.framesPerSecIdle,
                            enemyConfig.frameCountWalk,
                            enemyConfig.framesPerSecWalk,
                            Entity.Direction.Down,
                            Entity.AnimationState.Idle);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            enemy.Update(gameTime);
        }
    }
}
