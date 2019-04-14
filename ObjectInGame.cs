using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame
{
    public class ObjectInGame
    {
        private readonly float scale;

        public MoveDirectionType MoveDirection { get; set; }
        public ObjectStateType State { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle Hitbox { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }
        public TextureInfo TextureInfo { get; }
        public bool IsDeadly { get; set; }
        public int LastFrameChange { get; set; }
        public int FrameWhenDied { get; set; }
        public Vector2 LastPosition { get; set; }

        protected int column;

        private static int MaxX;
        private static int MinX;

        public ObjectInGame(int x, int y, TextureInfo textureInfo)
        {
            State = ObjectStateType.Asleep;
            X = x;
            Y = y;
            TextureInfo = textureInfo;
            MoveDirection = MoveDirectionType.None;
            IsDeadly = false;
            column = 0;

            scale = 3; //Harcoded for now
        }

        public ObjectInGame(int x, int y, TextureInfo textureInfo, bool isDeadly, MoveDirectionType initialMoveDirection)
        {
            State = ObjectStateType.Asleep;
            X = x;
            Y = y;
            IsDeadly = isDeadly;
            TextureInfo = textureInfo;
            MoveDirection = initialMoveDirection;
            column = 0;

            scale = 3; //Harcoded for now
        }

        public ObjectInGame(int x, int y, TextureInfo textureInfo, float scale)
        {
            State = ObjectStateType.Asleep;
            X = x;
            Y = y;
            IsDeadly = false;
            TextureInfo = textureInfo;
            MoveDirection = MoveDirectionType.None;
            column = 0;

            this.scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceAnimation = GetAnimation();
            spriteBatch.Draw(Texture, new Vector2(X, Y), sourceAnimation, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }

        public virtual void Move(float deltaTime)
        {
        }

        public void MoveRight(float deltaTime)
        {
            X += (int)(Speed * scale * deltaTime);

            if (X > MaxX - TextureInfo.Width * scale)
            {
                X = (int)(MaxX - TextureInfo.Width * scale);
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
            AdjustPosition();
            Hitbox = new Rectangle(X, Y, (int)(TextureInfo.Width * scale), (int)(TextureInfo.Height * scale));
        }

        public virtual void RecalculateHitBox()
        {
            Hitbox = new Rectangle(X, Y, (int)(TextureInfo.Width * scale), (int)(TextureInfo.Height * scale));
        }

        protected void AdjustPosition()
        {
            Y -= (int)(TextureInfo.Height * scale);
        }

        public static void SetXBoundaries(int minX, int maxX)
        {
            MaxX = maxX;
            MinX = minX;
        }

        public virtual void NonLethalCollision(Rectangle box2)
        {
            var newPosition = GetAdjustedPosition(box2);

            X = newPosition.X;
            Y = newPosition.Y;
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

        protected virtual void SetRightAnimation()
        {
            
        }

        protected virtual void SetLeftAnimation()
        {

        }

        protected Vector2Int GetAdjustedPosition(Rectangle box2)
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

            if(moveY != 0)
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

            if(alphaX == 0)
            {
                return new Vector2Int(X , Y - (int)(alphaY * moveY));
            }

            if (alphaY == 0)
            {
                return new Vector2Int(X - (int)(alphaX * moveX), Y);
            }

            if (alphaX < alphaY)
            {
                return new Vector2Int(X - (int)(alphaX * moveX), Y);
            }
            else
            {
                return new Vector2Int(X, Y - (int)(alphaY * moveY));
            }
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

        #region Animation
        protected virtual Rectangle GetAnimation()
        {
            if (MoveDirection == MoveDirectionType.None) column = 0;
            if (MoveDirection == MoveDirectionType.Left) column = 0;
            if (MoveDirection == MoveDirectionType.Right) column = 1;

            return new Rectangle(TextureInfo.Width * column, 0, TextureInfo.Width, TextureInfo.Height);
        }
        #endregion Animation
    }
}