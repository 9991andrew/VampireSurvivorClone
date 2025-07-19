using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ECS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vampire_Survivor.Entities.Enemies
{
    public abstract class Enemy : Entity
    {
        //Who the enemy is targeting
        protected Player Target { get; set; }
        //Damage the enemy deals
        public int Damage { get; set; }
        //AI state of the enemy
        protected enum AIState{ Patrol, Chase, Attack, Death};
        //The current AI State of the enemy
        private AIState aistate;
        //Attack texture path name
        private string attackAnim;
        //Frames per sec for attack texture
        private int framesPerSecAttack;
        //Frame count for attack texture
        private int frameCountAttack;
        //Texture for attacking the player
        private AnimatedTexture AttackTexture;




        //Constructor of enemy
        protected Enemy(Player target, int initHealth, int damage, string animPath, int framesPerSec, int frameCount)
        {
            Target = target;
            Health = initHealth;
            Damage = damage;
            aistate = AIState.Patrol;
            
            attackAnim = animPath;
            framesPerSecAttack = framesPerSec;
            frameCountAttack = frameCount;
        }
        //Initialize the enemy.
        public void Initialize(ContentManager content, Vector2 pos, int speed, Vector2 origin, int healthValue, float rotation,
                                          float scale, float depth, string idleAnim, string walkAnim, int frameCount,
                                          int framesPerSec, int frameCount2, int framesPerSec2, Direction dir)
        {
            base.Initialize(content,
                            pos,
                            speed,
                            origin,
                            healthValue,
                            rotation,
                            scale,
                            depth,
                            idleAnim,
                            walkAnim,
                            frameCount,
                            framesPerSec,
                            frameCount2,
                            framesPerSec2,
                            dir);
            AttackTexture = new AnimatedTexture(origin, rotation, scale, depth);
            if(AttackTexture == null) throw new Exception("Failed to load the attack animation texture");
            AttackTexture.Load(content, attackAnim, frameCountAttack, framesPerSecAttack);
        }
        //Update the enemy
        public override void Update(GameTime gameTime)
        {
            if (Health <= 0) return;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Direction vector to the player
            Vector2 toPlayer = Target.Position - Position;
            toPlayer.Normalize();
            //Previous position of the player
            Vector2 prevPos = Position;
            if (toPlayer.LengthSquared() > 0.001f)
            {
                Position += toPlayer * (Speed * dt);                
                if (Math.Abs(toPlayer.X) > Math.Abs(toPlayer.Y))
                    CurrentDirection = toPlayer.X > 0 ? Direction.Right : Direction.Left;
                else
                    CurrentDirection = toPlayer.Y > 0 ? Direction.Down : Direction.Up;
            }
            //This is where we detect collision between the enemy and the player. Not the other way around.
            //When this happens, we set the position of the enemy to be whatever it was before it interacts with the player in whatever way.
            //Then we set the animationstate of the enemy to Idle and the AIState to Attack.
            if (CollisionHelper.CheckCollision(Bounds,Target.Bounds))
            {
                Position = prevPos;
                aistate = AIState.Attack;      
            } 
            else
            {
                aistate = AIState.Patrol;
            }
            HandleAIState(aistate, dt);
        }


        protected virtual void HandleAIState(AIState state, float dt)
        {
            switch(state)
            {
                case AIState.Patrol: 
                    CurrentTexture = MovingTexture;
                    break;
                case AIState.Chase: 
                    CurrentTexture = IdleTexture;
                    break;
                case AIState.Attack:
                    CurrentTexture = AttackTexture;
                    break;
                case AIState.Death:
                    CurrentTexture = IdleTexture;
                    break;
            }
            if (CurrentTexture != null)
            {
                Console.WriteLine("Handling AI State, current state is: " + state);
                CurrentTexture.UpdateFrame(dt);         
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            DrawDebugBounds(spriteBatch);
        }
        //Method for having the enemy take damage.
        public void TakeDamage()
        {
            Debug.WriteLine("Taking damage");
            Health -= Damage;
            if (Health <= 0)
                OnDeath();
        }
        
        //What happens when the Enemy dies.
        protected virtual void OnDeath() { }
        // These abstract props force each enemy to supply its own art & stats
    }

}

