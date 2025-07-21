using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Vampire_Survivor.Entities.Enemies;

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
         * The direction of the entity
         */
        public enum Direction { Down = 0, Up = 1, Left = 2, Right = 3 };
        /*
        * The current direction of the player
        */
        public Direction CurrentDirection;

        /*
         * The idle texture of the entity
         */
        public AnimatedTexture IdleTexture { get; set; }
        /*
         * The moving texture of the entity
         */
        public AnimatedTexture MovingTexture { get; set; }
        /*
         * The current texture of the entity
        */
        public AnimatedTexture CurrentTexture { get; set; }
        /*
         * The pixel for debugging.
         */
        private Texture2D debugPixel;
        /*
         * Don't really need to add anything here but, the below abstract void 
         * methods just update, draw, and initialize the entities.
         */
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            /*  switch (state)
              {
                  case AnimationState.Idle:
                      IdleTexture?.DrawFrame(spriteBatch, (int)CurrentDirection, Position);
                      break;
                  case AnimationState.Moving:
                      MovingTexture?.DrawFrame(spriteBatch, (int)CurrentDirection, Position);
                      break;
              }
            */
            if(CurrentTexture != null)
            {
                CurrentTexture.DrawFrame(spriteBatch, (int)CurrentDirection, Position);
            }

        }
        public virtual void Initialize(ContentManager content, Vector2 pos, int speed, Vector2 origin, int healthValue, float rotation, float scale, float depth, string idleAnim, string walkAnim, int frameCount, int framesPerSec, int frameCount2, int framesPerSec2, Direction dir)
        {
            CurrentDirection = dir;
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
            get { 
            

                AnimatedTexture currTexture = CurrentTexture;
                if (currTexture == null)
                {
                    return Rectangle.Empty;
                }    
                int w = (int)(currTexture.FrameWidth * currTexture.Scale);
                int h = (int)(currTexture.FrameHeight * currTexture.Scale);
                Vector2 topLeft = Position - (currTexture.Origin * currTexture.Scale); 

                Rectangle b = new Rectangle((int)topLeft.X, (int)topLeft.Y, w, h);
                int pX = 34;
                int pY = 38;
                b.Inflate(-pX, -pY);
                return b;//(int)Position.X, (int)Position.Y, w, h);//currentTexture.FrameWidth, currentTexture.FrameHeight); 
            } 
        }
        public void DrawDebugBounds(SpriteBatch batch)
        {
            // ensure our 1×1 pixel exists
            if (debugPixel == null)
            {
                debugPixel = new Texture2D(batch.GraphicsDevice, 1, 1);
                debugPixel.SetData(new[] { Color.White });
            }
            // use the current frame index
            var box = Bounds;
            batch.Draw(debugPixel, new Rectangle(box.Left, box.Top, box.Width, 1), Color.Red);
            batch.Draw(debugPixel, new Rectangle(box.Left, box.Bottom, box.Width, 1), Color.Red);
            batch.Draw(debugPixel, new Rectangle(box.Left, box.Top, 1, box.Height), Color.Red);
            batch.Draw(debugPixel, new Rectangle(box.Right, box.Top, 1, box.Height), Color.Red);
        }

        public void ResolveOverlapCollision(Enemy other)
        {
            if (CollisionHelper.CheckCollision(this.Bounds, other.Bounds))
            {
                Vector2 direction = this.Position - other.Position;
                if (direction.LengthSquared() < 0.01f)  direction = new Vector2(1, 0);

                direction.Normalize();

                Rectangle intersection = Rectangle.Intersect(this.Bounds, other.Bounds);
                float overlap = MathF.Max(intersection.Width, intersection.Height) / 2f;

                if(overlap > 1f)
                {
                    float correction = overlap * 0.2f;
                    Vector2 push = direction * correction;
                    
                    this.Position += push;
                    other.Position -= push;
                }
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
