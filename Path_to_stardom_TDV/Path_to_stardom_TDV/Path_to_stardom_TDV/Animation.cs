using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Path_to_stardom_TDV
{
    public enum AnimationState
    {
        Idle,
        Walk,
        Attack1,
        Attack2,
        Attack3,
        Jump
    }

    public class Animation
    {
        public Texture2D SpriteSheet { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public int NumberOfFrames { get; private set; }
        public float FrameDuration { get; private set; }

        public Animation(Texture2D spriteSheet, int frameWidth, int frameHeight, int numberOfFrames, float frameDuration)
        {
            SpriteSheet = spriteSheet;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            NumberOfFrames = numberOfFrames;
            FrameDuration = frameDuration;
        }
    }

    public class AttackInfo
    {
        public string Name { get; }
        public AnimationState AnimationState { get; }
        public int Damage { get; }
        public List<Rectangle> Hitboxes { get; }
        public int ActiveStartFrame { get; }
        public int ActiveEndFrame { get; }

        public AttackInfo(string name, AnimationState animationState, int damage,
                         List<Rectangle> hitboxes, int activeStart, int activeEnd)
        {
            Name = name;
            AnimationState = animationState;
            Damage = damage;
            Hitboxes = hitboxes;
            ActiveStartFrame = activeStart;
            ActiveEndFrame = activeEnd;
        }
    }
}

