﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Vampire_Survivor.Entities
{
    public class Player : Entity
    {
        /*
         * The animation state of the player
         */
        public enum AnimationState { Idle, Moving };
        /*
         *The current animation state of the player
        */
        public AnimationState state;
        public override void Initialize(ContentManager content, Vector2 pos, int speed, Vector2 origin, int healthValue, float rotation,
                                float scale, float depth, string idleAnim, string walkAnim, int frameCount,
                                int framesPerSec, int frameCount2, int framesPerSec2, Direction dir)

        {
            base.Initialize
                (
                content,
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
                dir   
                );
        }

        public override void Update(GameTime gameTime)
        {
            //   base.Update(gameTime);
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keys = Keyboard.GetState();
            Vector2 input = new Vector2((keys.IsKeyDown(Keys.D) ? 1 : 0) - (keys.IsKeyDown(Keys.A) ? 1 : 0), (keys.IsKeyDown(Keys.S) ? 1 : 0) - (keys.IsKeyDown(Keys.W) ? 1 : 0));
            if (input != Vector2.Zero)
            {
                input.Normalize();
                state = AnimationState.Moving;
                if (Math.Abs(input.X) > Math.Abs(input.Y))
                    CurrentDirection = input.X > 0 ? Direction.Right : Direction.Left;
                else
                    CurrentDirection = input.Y > 0 ? Direction.Down : Direction.Up;
            }
            else
            {
                state = AnimationState.Idle;
            }
            setPlayerTexture(state);
            Position += input * Speed * elapsedTime;
            if(CurrentTexture != null)
            {
                CurrentTexture = (state == AnimationState.Moving) ? MovingTexture : IdleTexture;
                CurrentTexture.Play();
                CurrentTexture.UpdateFrame(elapsedTime);
            }
        }

        private void setPlayerTexture(AnimationState animationState)
        {
            switch (animationState)
            {
                case AnimationState.Moving:
                    CurrentTexture = MovingTexture;
                    break;
                case AnimationState.Idle:
                    CurrentTexture = IdleTexture;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            DrawDebugBounds(spriteBatch);
        }

    }
}
