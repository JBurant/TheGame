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

        public ObjectStateType State { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Rectangle Hitbox { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }
        public string TextureFile { get; }
        public bool IsDeadly { get; set; }

        public Vector2 LastPosition { get; set; }

        private static int MaxX;
        private static int MinX;

        public ObjectInGame(int x, int y, string textureFile, bool isDeadly)
        {
            State = ObjectStateType.Asleep;
            X = x;
            Y = y;
            IsDeadly = isDeadly;
            TextureFile = textureFile;

            scale = 3; //Harcoded for now
        }

        public ObjectInGame(int x, int y, string textureFile, bool isDeadly, float scale)
        {
            State = ObjectStateType.Asleep;
            X = x;
            Y = y;
            TextureFile = textureFile;

            this.scale = scale;
        }

        public ObjectInGame(int x, int y, string textureFile)
        {
            State = ObjectStateType.Asleep;
            X = x;
            Y = y;
            TextureFile = textureFile;

            scale = 3; //Harcoded for now
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }

        public virtual void Move(float deltaTime)
        {
        }

        public void MoveRight(float deltaTime)
        {
            X += (int)(Speed * scale * deltaTime);

            if (X > MaxX - Texture.Width * scale)
            {
                X = (int)(MaxX - Texture.Width * scale);
            }
        }

        public void MoveLeft(float deltaTime)
        {
            X -= (int)(Speed * scale * deltaTime);

            if (X < MinX)
            {
                X = MinX;
            }
        }

        public virtual void SetHitbox()
        {
            AdjustPosition();
            Hitbox = new Rectangle((int)X, (int)Y, (int)(Texture.Width * scale), (int)(Texture.Height * scale));
        }

        public virtual void RecalculateHitBox()
        {
            Hitbox = new Rectangle((int)X, (int)Y, (int)(Texture.Width * scale), (int)(Texture.Height * scale));
        }

        protected void AdjustPosition()
        {
            Y -= (int)(Texture.Height * scale);
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

        public virtual void LethalCollision(ObjectInGame enemy)
        {

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

        /*
        protected virtual void MoveVertically(float deltaTime)
        {
            if (!LowerCollisionDetected())
            {
                Fall(deltaTime);
            }
        }*/

        protected virtual bool LowerCollisionDetected()
        {

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

            if (Hitbox.Right > box2.Left && !(Math.Floor(LastPosition.X) + Hitbox.Size.X > box2.Left))
            {
                alphaX = (Hitbox.Right - box2.Left) / moveX;
            }
            else if (Hitbox.Left < box2.Right && !(Math.Floor(LastPosition.X) < box2.Right))
            {
                alphaX = (Hitbox.Left - box2.Right) / moveX;
            }

            if (Hitbox.Bottom > box2.Top && !(Math.Floor(LastPosition.Y) + Hitbox.Size.Y > box2.Top))
            {
                alphaY = (Hitbox.Bottom - box2.Top) / moveY;
            }
            else if (Hitbox.Top < box2.Bottom && !(Math.Floor(LastPosition.Y) < box2.Bottom))
            {
                alphaY = (Hitbox.Top - box2.Bottom) / moveY;
            }

            if(alphaX == 0)
            {
                return new Vector2Int(X - (int)(alphaY * moveX), Y - (int)(alphaY * moveY));
            }

            if (alphaY == 0)
            {
                return new Vector2Int(X - (int)(alphaX * moveX), Y - (int)(alphaX * moveY));
            }

            if (alphaX < alphaY)
            {
                return new Vector2Int(X - (int)(alphaX * moveX), Y - (int)(alphaX * moveY));
            }
            else
            {
                return new Vector2Int(X - (int)(alphaY * moveX), Y - (int)(alphaY * moveY));
            }
        }

        public virtual void Die()
        {
            State = ObjectStateType.Dead;
        }


        public void SaveLastPosition()
        {
            LastPosition = new Vector2(X, Y);
        }
    }
}