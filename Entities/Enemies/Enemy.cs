using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        protected enum AIState{ Patrol, Chase, Attack};
        //The current AI State of the enemy
        private AIState aistate;
        //Constructor of enemy
        protected Enemy(Player target, int initHealth, int damage)
        {
            Target = target;
            Health = initHealth;
            Damage = damage;
            aistate = AIState.Chase;
        }
        //Initialize the enemy.
        public override void Initialize(ContentManager content, Vector2 pos, int speed, Vector2 origin, int healthValue, float rotation,
                                          float scale, float depth, string idleAnim, string walkAnim, int frameCount,
                                          int framesPerSec, int frameCount2, int framesPerSec2, Direction dir, AnimationState animState)
        {
            base.Initialize(content,pos,speed,origin,healthValue,rotation,scale, depth,idleAnim,walkAnim,frameCount,framesPerSec,frameCount2,framesPerSec2,dir,animState);
        }
        //Update the enemy
        public override void Update(GameTime gameTime)
        {
            if (Health <= 0)
                return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Vector pointing from me → player
            Vector2 toPlayer = Target.Position - Position;
            Vector2 prevPos = Position;
            toPlayer.Normalize();

            if (toPlayer.LengthSquared() > 0.001f)
            {
                
                Position += toPlayer * Speed * dt;                
                if (Math.Abs(toPlayer.X) > Math.Abs(toPlayer.Y))
                    CurrentDirection = toPlayer.X > 0 ? Direction.Right : Direction.Left;
                else
                    CurrentDirection = toPlayer.Y > 0 ? Direction.Down : Direction.Up;
                state = AnimationState.Moving;

            }


            if (CollisionHelper.CheckCollision(Bounds,Target.Bounds))
            {
                Position = prevPos;
               // TakeDamage();
                state = AnimationState.Idle;
            }


            // Advance whichever animation is active
            var anim = (state == AnimationState.Moving) ? MovingTexture : IdleTexture;
            anim.UpdateFrame(dt);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
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

