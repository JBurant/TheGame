using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.Collision
{
    public static class CollisionResolver
    {
        public static bool DetectCollision(Rectangle hitbox1, Rectangle hitbox2)
        {
            if (hitbox2 == null)
            {
                return false;
            }

            if (hitbox1.Left < hitbox2.Right &&
                hitbox1.Right > hitbox2.Left &&
                hitbox1.Top < hitbox2.Bottom &&
                hitbox1.Bottom > hitbox2.Top)
            {
                return true;
            }
            return false;
        }

        public static Point SolidObjectsCollision(Rectangle hitbox1, Rectangle hitbox2, Point currentPosition, Point lastPosition)
        {
            var x = currentPosition.X;
            var y = currentPosition.Y;

            float alphaX = 0;
            float alphaY = 0;
            float moveX = currentPosition.X - lastPosition.X;
            float moveY = currentPosition.Y - lastPosition.Y;
            
            if (moveX != 0)
            {
                if (hitbox1.Right > hitbox2.Left && !(lastPosition.X + hitbox1.Size.X > hitbox2.Left))
                {
                    alphaX = (hitbox1.Right - hitbox2.Left) / moveX;
                }
                else if (hitbox1.Left < hitbox2.Right && !(lastPosition.X < hitbox2.Right))
                {
                    alphaX = (hitbox1.Left - hitbox2.Right) / moveX;
                }
            }

            if (moveY != 0)
            {
                if (hitbox1.Bottom > hitbox2.Top && !(lastPosition.Y + hitbox1.Size.Y > hitbox2.Top))
                {
                    alphaY = (hitbox1.Bottom - hitbox2.Top) / moveY;
                }
                else if (hitbox1.Top < hitbox2.Bottom && !(lastPosition.Y < hitbox2.Bottom))
                {
                    alphaY = (hitbox1.Top - hitbox2.Bottom) / moveY;
                }
            }

            if (alphaX == 0)
            {
                currentPosition.Y -= (int)(alphaY * moveY);
            }

            if (alphaY == 0)
            {
                currentPosition.X -= (int)(alphaX * moveX);
            }

            if (alphaX < alphaY)
            {
                currentPosition.X -= (int)(alphaX * moveX);
            }
            else
            {
                currentPosition.Y -= (int)(alphaY * moveY);
            }

            return currentPosition;
        }
    }
}
