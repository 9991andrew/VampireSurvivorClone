using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Vampire_Survivor.Entities
{
   
    public abstract class Entity
    {
        /*
         * Position of the entity
         */
        public Vector2 Position { get; set; }
        /*
         * Speed of the entity 
         */
        public int Speed { get; set; }
        /*
         * Health of the entity
         */
        public int Health { get; set; }
        /*
         * The animation state of the entity
         */
        public enum AnimationState { Idle, Moving };
        /*
         * The direction of the entity
         */
        public enum Direction { Down = 0, Up = 1, Left = 2, Right = 3 };
        /*
        * The current direction of the player
        */
        public Direction CurrentDirection;
        /*
         *The current animation state of the player
         */
        public AnimationState state;
        /*
         * The idle texture of the entity
         */
        public AnimatedTexture IdleTexture { get; set; }
        /*
         * The moving texture of the entity
         */
        public AnimatedTexture MovingTexture { get; set; }
        /*
         * Don't really need to add anything here but, the below abstract void 
         * methods just update, draw, and initialize the entities.
         */
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case AnimationState.Idle:
                    IdleTexture?.DrawFrame(spriteBatch, (int)CurrentDirection, Position);
                    break;
                case AnimationState.Moving:
                    MovingTexture?.DrawFrame(spriteBatch, (int)CurrentDirection, Position);
                    break;
            }
        }
        public virtual void Initialize(ContentManager content, Vector2 pos, int speed, Vector2 origin, int healthValue, float rotation, float scale, float depth, string idleAnim, string walkAnim, int frameCount, int framesPerSec, int frameCount2, int framesPerSec2, Direction dir, AnimationState animState)
        {
            CurrentDirection = dir;
            state = animState;
            Position = pos;
            Speed = speed;
            Health = healthValue;

            IdleTexture = new AnimatedTexture(origin, rotation, scale, depth);
            if (IdleTexture == null)
                throw new Exception("Failed to load idle player texture.");
            IdleTexture.Load(content, idleAnim, frameCount, framesPerSec);

            MovingTexture = new AnimatedTexture(origin, rotation, scale, depth);
            if (MovingTexture == null)
                throw new Exception("failed to load moving player texture. ");
            MovingTexture.Load(content, walkAnim, frameCount2, framesPerSec2);
        }
        public abstract void Update(GameTime gameTime);
        
        public virtual Rectangle Bounds 
        { 
            get 
            {
                AnimatedTexture currentTexture = (state == AnimationState.Idle) ? IdleTexture : MovingTexture;
                if (currentTexture == null)
                {
                    return Rectangle.Empty;
                }
              
                return new Rectangle((int) Position.X, (int) Position.Y, currentTexture.FrameWidth, currentTexture.FrameHeight); 
            } 
        }
    }
   //This is the collision helper static sub class for detecting collision
    public static class CollisionHelper
    {
        public static bool CheckCollision(Rectangle a, Rectangle b)
        {
            return a.Intersects(b);
        }
    }

}
