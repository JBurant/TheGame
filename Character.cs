using Microsoft.Xna.Framework;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame
{
    public class Character : ObjectInGame
    {
        public bool isJumping;
        private float jumpTime;
        private float lastJumpTime;

        private int animationAlteration;

        private readonly float jumpSpeed;
        private readonly float fallSpeed;
        private const float maxJumpTime = 0.75f;
        private const int spacer = 1;

        private readonly int[] animateLeftColumns;
        private readonly int[] animateRightColumns;

        public Character(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo, 2)
        {
            Speed = 100f;
            jumpSpeed = 300f;
            fallSpeed = jumpSpeed;

            animateLeftColumns = new int[] { 1, 5 };
            animateRightColumns = new int[] { 2, 6 };
        }

        public void MoveVertically(bool isUp, bool isDown, float deltaTime)
        {
            lastJumpTime += deltaTime;

            if (isDown)
            {
                MoveDirection = MoveDirectionType.Down;
            }
            else
            {
                MoveDirection = MoveDirectionType.None;
            }

            if (isUp && (lastJumpTime > maxJumpTime * 2))
            {
                isJumping = true;
                lastJumpTime = 0;
            }

            if (isJumping)
            {
                Y -= (int)(jumpSpeed * deltaTime);
                jumpTime += deltaTime;

                if (jumpTime > maxJumpTime)
                {
                    isJumping = false;
                    jumpTime = 0;
                }

                MoveDirection = MoveDirectionType.Up;
            }
            else
            {
                Fall(deltaTime);
            }
        }

        protected virtual void Fall(float deltaTime)
        {
            Y += (int)(fallSpeed * deltaTime);
        }

        public void LethalCollision(ObjectInGame enemy, int frameWhenDied)
        {
            if (!(LastPosition.Y < Y))
            {
                Die(frameWhenDied);
            }
            else
            {
                enemy.Die(frameWhenDied);
            }
        }

        public void MoveHorizontally(bool isRight, bool isLeft, float deltaTime)
        {
            if (isRight)
            {
                MoveRight(deltaTime);
                MoveDirection = MoveDirectionType.Right;
            }

            if (isLeft)
            {
                MoveLeft(deltaTime);
                MoveDirection = MoveDirectionType.Left;
            }
        }

        public void AdjustAnimations(FrameCounter frameCounter)
        {
            animationAlteration = (frameCounter.Frame / 20) % 2;
        }

        private int GetAnimationLeftColumn()
        {
            if ((X / 20) % 2 == 0)
            {
                return animateLeftColumns[0];
            }
            else
            {
                return animateLeftColumns[1];
            }
        }

        private int GetAnimationRightColumn()
        {
            if ((X / 20) % 2 == 0)
            {
                return animateRightColumns[0];
            }
            else
            {
                return animateRightColumns[1];
            }
        }

        protected override Rectangle GetAnimation()
        {
            switch (MoveDirection)
            {
                case MoveDirectionType.None:
                    column = 0;
                    break;

                case MoveDirectionType.Left:
                    column = GetAnimationLeftColumn();
                    break;

                case MoveDirectionType.Right:
                    column = GetAnimationRightColumn();
                    break;

                case MoveDirectionType.Up:
                    column = 3;
                    break;

                case MoveDirectionType.Down:
                    column = 4;
                    break;
            }

            return new Rectangle((TextureInfo.Width + spacer) * column, 0, TextureInfo.Width, TextureInfo.Height);
        }
    }
}