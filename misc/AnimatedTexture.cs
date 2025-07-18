﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

/// <summary>
/// A helper class for handling animated textures.
/// </summary>
public class AnimatedTexture
{
    // Number of frames in the animation.
    private int frameCount;

    // The animation spritesheet.
    private Texture2D myTexture;

    // The number of frames to draw per second.
    private float timePerFrame;

    // The current frame being drawn.
    private int frame;

    // Total amount of time the animation has been running.
    private float totalElapsed;

    // Is the animation currently running?
    private bool isPaused;

    // The current rotation, scale and draw depth for the animation.
    public float Rotation, Scale, Depth;

    // The origin point of the animated texture.
    public Vector2 Origin;
    //Width of the frame of the texture.
    public int FrameWidth { get { return myTexture.Width / frameCount; } }
    //Height of the frame of the texture.
    public int FrameHeight { get { return myTexture.Height / 4; } }

    public AnimatedTexture(Vector2 origin, float rotation, float scale, float depth)
    {
        this.Origin = origin;
        this.Rotation = rotation;
        this.Scale = scale;
        this.Depth = depth;
    }

    public void Load(ContentManager content, string asset, int frameCount, int framesPerSec)
    {
        this.frameCount = frameCount;
        myTexture = content.Load<Texture2D>(asset);
        timePerFrame = (float) 1 / framesPerSec;
        frame = 0;
        totalElapsed = 0;
    }

    public void UpdateFrame(float elapsed)
    {
        if (isPaused) return;
        totalElapsed += elapsed;
        if (totalElapsed > timePerFrame)
        {
            frame++;
            // Keep the Frame between 0 and the total frames, minus one.
            frame %= frameCount;
            totalElapsed -= timePerFrame;
        }
    }

    public void DrawFrame(SpriteBatch batch, int row, Vector2 screenPos)
    {
        DrawFrame(batch, frame, row, screenPos);
    }

    public void DrawFrame(SpriteBatch batch, int frame, int row, Vector2 screenPos)
    {
        Rectangle sourcerect = new Rectangle(FrameWidth * frame, row * FrameHeight, FrameWidth, FrameHeight);
        batch.Draw(myTexture, screenPos, sourcerect, Color.White, Rotation, Origin, Scale, SpriteEffects.None, Depth);
    }



    public bool IsPaused
    {
        get { return isPaused; }
    }

    public void Reset()
    {
        frame = 0;
        totalElapsed = 0f;
    }

    public void Stop()
    {
        Pause();
        Reset();
    }

    public void Play()
    {
        isPaused = false;
    }

    public void Pause()
    {
        isPaused = true;
    }

}