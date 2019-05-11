using TheGame.Animation;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class RotatingObject : ForegroundObject
    {
        // private readonly float angle;

        public RotatingObject(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo, 3f)
        {
        }

        protected override void Initialize(TextureInfo textureInfo)
        {
            animationResolver = new StaticAnimationResolver(textureInfo);
        }

        public void Move(float deltaTime)
        {/*
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
            }*/
        }
    }
}
