using System;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame
{
    public class Character : ObjectInGame
    {
        public bool isJumping;
        private bool isFalling;
        private float jumpTime;
        private readonly float jumpSpeed;
        private readonly float fallSpeed;
        private const float maxJumpTime = 1f;

        public Character(int x, int y, string textureFile) : base(x, y, textureFile, false, 2)
        {
            Speed = 50f;
            jumpSpeed = 80f;
            fallSpeed = 100f;
        }

        public void MoveVertically(bool isUp, float deltaTime)
        {
            if(isUp)
            {
                Jump(deltaTime);
            }

            if (isJumping)
            {
                Y -= (int)(jumpSpeed * deltaTime);
                jumpTime += deltaTime;
                isFalling = false;

                if (jumpTime > maxJumpTime)
                {
                    isJumping = false;
                    jumpTime = 0;
                }
            }
            else
            {
                if(!LowerCollisionDetected())
                {
                    Fall(deltaTime);
                    isFalling = true;
                }
                else
                {
                    isFalling = false;
                }
            }
        }

        protected virtual void Fall(float deltaTime)
        {
            Y += (int)(fallSpeed * deltaTime);
        }

        public void Jump(float deltaTime)
        {
            if (!isJumping)
            {
                isJumping = true;
                Y -= (int)(jumpSpeed * deltaTime);
                jumpTime += deltaTime;
            }
        }

        public override void LethalCollision(ObjectInGame enemy)
        {
            if(!isFalling)
            {
                Die();
            }
            else
            {
                enemy.Die();
            }
        }

        public void MoveHorizontally(bool right, bool left, float deltaTime)
        {
            if(right)
            {
                MoveRight(deltaTime);
            }

            if(left)
            {
                MoveLeft(deltaTime);
            }
        }
    }
}
