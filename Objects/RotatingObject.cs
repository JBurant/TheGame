using Microsoft.Xna.Framework;
using TheGame.Animation;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class RotatingObject : Critter
    {
        private float angle;

        public RotatingObject(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo, 3f, 10, MoveDirectionType.RotateRight)
        {
        }

        protected override void Initialize(TextureInfo textureInfo)
        {
            animationResolver = new StaticAnimationResolver(textureInfo);
        }

        public override void Move(float deltaTime)
        {
            if (MoveDirection == MoveDirectionType.RotateRight)
            {
                angle += speed * deltaTime;

                if(angle > 1)
                {
                    angle -= 1;
                }
            }

            if (MoveDirection == MoveDirectionType.RotateLeft)
            {
                Position.X += (int)(speed * deltaTime);
            }
        }
    }
}
