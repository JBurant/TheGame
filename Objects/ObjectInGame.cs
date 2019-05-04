using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheGame.Animation;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class ObjectInGame
    {
        private readonly float scale;

        protected MoveDirectionType MoveDirection { get; set; }
        public ObjectStateType State { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Rectangle Hitbox { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsDeadly { get; set; }
        public int LastFrameChange { get; set; }
        public int FrameWhenDied { get; set; }
        public Vector2 LastPosition { get; set; }

        protected IAnimationResolver animationResolver;
        private static int MaxX;
        private static int MinX;
        private static int MaxY;
        private static int MinY;

        public ObjectInGame(int x, int y, TextureInfo textureInfo, bool isDeadly, MoveDirectionType initialMoveDirection)
        {
            State = ObjectStateType.Asleep;
            X = x;
            Y = y;
            IsDeadly = isDeadly;
            animationResolver = new StaticAnimationResolver(textureInfo);
            MoveDirection = initialMoveDirection;

            scale = 3; //Harcoded for now
        }

        public ObjectInGame(int x, int y, TextureInfo textureInfo, float scale)
        {
            State = ObjectStateType.Asleep;
            X = x;
            Y = y;
            IsDeadly = false;
            animationResolver = new StaticAnimationResolver(textureInfo);
            MoveDirection = MoveDirectionType.None;

            this.scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceAnimation = animationResolver.GetAnimation(X, MoveDirection);
            spriteBatch.Draw(Texture, new Vector2(X - MinX, Y), sourceAnimation, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }

        public virtual void Move(float deltaTime)
        {
        }

        public void MoveRight(float deltaTime)
        {
            X += (int)(Speed * scale * deltaTime);

            if (X > MaxX - animationResolver.TextureInfo.Width * scale)
            {
                X = (int)(MaxX - animationResolver.TextureInfo.Width * scale);
                MoveDirection = MoveDirectionType.Left;
            }
        }

        public void MoveLeft(float deltaTime)
        {
            X -= (int)(Speed * scale * deltaTime);

            if (X < MinX)
            {
                X = MinX;
                MoveDirection = MoveDirectionType.Right;
            }
        }

        public virtual void SetHitbox()
        {
            Y -= (int)(animationResolver.TextureInfo.Height * scale);
            Hitbox = new Rectangle(X, Y, (int)(animationResolver.TextureInfo.Width * scale), (int)(animationResolver.TextureInfo.Height * scale));
        }

        public virtual void RecalculateHitBox()
        {
            Hitbox = new Rectangle(X, Y, (int)(animationResolver.TextureInfo.Width * scale), (int)(animationResolver.TextureInfo.Height * scale));
        }

        public static void SetXBoundaries(int minX, int maxX)
        {
            MaxX = maxX;
            MinX = minX;
        }

        public static void SetYBoundaries(int minY, int maxY)
        {
            MaxY = maxY;
            MinY = minY;
        }

        public void CheckFallDeath(int frameWhenDied)
        {
            if (Y > MaxY)
            {
                Die(frameWhenDied);
            }
        }

        public virtual void NonLethalCollision(Rectangle box2)
        {
            var moveX = X - LastPosition.X;
            var moveY = Y - LastPosition.Y;

            float alphaX = 0;
            float alphaY = 0;

            if (moveX != 0)
            {
                if (Hitbox.Right > box2.Left && !(Math.Floor(LastPosition.X) + Hitbox.Size.X > box2.Left))
                {
                    alphaX = (Hitbox.Right - box2.Left) / moveX;
                }
                else if (Hitbox.Left < box2.Right && !(Math.Floor(LastPosition.X) < box2.Right))
                {
                    alphaX = (Hitbox.Left - box2.Right) / moveX;
                }
            }

            if (moveY != 0)
            {
                if (Hitbox.Bottom > box2.Top && !(Math.Floor(LastPosition.Y) + Hitbox.Size.Y > box2.Top))
                {
                    alphaY = (Hitbox.Bottom - box2.Top) / moveY;
                }
                else if (Hitbox.Top < box2.Bottom && !(Math.Floor(LastPosition.Y) < box2.Bottom))
                {
                    alphaY = (Hitbox.Top - box2.Bottom) / moveY;
                }
            }

            if (alphaX == 0)
            {
                Y = Y - (int)(alphaY * moveY);
            }

            if (alphaY == 0)
            {
                X = X - (int)(alphaX * moveX);
            }

            if (alphaX < alphaY)
            {
                X = X - (int)(alphaX * moveX);
            }
            else
            {
                Y = Y - (int)(alphaY * moveY);
            }
        }

        public virtual bool DetectCollision(Rectangle box2)
        {
            if(box2 == null)
            {
                return false;
            }

            if (Hitbox.Left < box2.Right &&
                Hitbox.Right > box2.Left &&
                Hitbox.Top < box2.Bottom &&
                Hitbox.Bottom > box2.Top)
            {
                return true;
            }
            return false;
        }

        public virtual void Die(int frameWhenDied)
        {
            FrameWhenDied = frameWhenDied;
            State = ObjectStateType.Dead;
        }

        public void SaveLastPosition()
        {
            LastPosition = new Vector2(X, Y);
        }

        public string GetTextureFile()
        {
            return animationResolver.TextureInfo.TextureFile;
        }
    }
}