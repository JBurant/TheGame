using Microsoft.Xna.Framework;
using TheGame.Animation;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Character : ObjectInGame
    {
        public bool isJumping;
        private float jumpTime;
        private float lastJumpTime;

        private readonly float jumpSpeed;
        private readonly float fallSpeed;
        private const float maxJumpTime = 0.75f;

        public Character(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo, 2)
        {
            Speed = 100f;
            jumpSpeed = 300f;
            fallSpeed = jumpSpeed;

            animationResolver = new CharacterAnimationResolver(textureInfo, new int[] { 1, 5 }, new int[] { 2, 6 });
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
    }
}