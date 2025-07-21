using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vampire_Survivor.Misc;

namespace Vampire_Survivor.Entities.Enemies
{
    public class EnemySpawner
    {
        private readonly List<Enemy> enemies = new();
        private readonly Player target;
        private readonly ContentManager content;
        private readonly Random random = new();
        private float spawnTimer = 0f;
        private float spawnInterval = 2f; 
        private IReadOnlyList<Enemy> Enemies => enemies;

        private Config enemyConfig;

        public EnemySpawner(Player target, ContentManager content)
        {
            this.target = target;
            this.content = content;

            enemyConfig = new Config();
            enemyConfig.pos = new Vector2(50, 50);
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
        public void Update(GameTime gameTime)
        {
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }

            // Update all enemies
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);
                if (enemies[i].Health <= 0)
                    enemies.RemoveAt(i);
            }

            for(int i = 0; i < enemies.Count; i++)
            {
                for(int j =i+1; j < enemies.Count; j++)
                {
                    enemies[i].ResolveOverlapCollision(enemies[j]);
                }
            }

        }
        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (var enemy in enemies) enemy.Draw(spriteBatch);
        }
        private void SpawnEnemy()
        {
            float distance = 300f;
            double angle = random.NextDouble() * Math.PI * 2;
            Vector2 spawnPos = target.Position + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * distance;

            var enemy = new SimpleEnemy(target, 100, 10, "Vampires2_Attack_full", 47, 12);
            enemy.Initialize(content,
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
                             Entity.Direction.Down);
            enemies.Add(enemy);
        }
    }
}
