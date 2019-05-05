using TheGame.Enums;

namespace TheGame.Move
{
    public class CharacterMoveHandler
    {
        public MoveDirectionType MoveDirection{ get; private set; }

        private const float maxJumpTime = 0.75f;

        private readonly int speed;
        private readonly int jumpSpeed;
        private readonly int fallSpeed;

        private bool isJumping;

        private float jumpTime;
        private float lastJumpTime;

        public CharacterMoveHandler(int speed, int jumpSpeed, int fallSpeed)
        {
            this.speed = speed;
            this.jumpSpeed = jumpSpeed;
            this.fallSpeed = fallSpeed;
        }

        public int MoveHorizontally(bool isRight, bool isLeft, float deltaTime)
        {
            if (isRight)
            {
                MoveDirection = MoveDirectionType.Right;
                return (int)(speed * deltaTime);
            }

            if (isLeft)
            {
                MoveDirection = MoveDirectionType.Left;
                return -(int)(speed * deltaTime);
            }

            return 0;
        }

        public int MoveVertically(bool isUp, bool isDown, float deltaTime)
        {
            int move = 0;

            lastJumpTime += deltaTime;

            if (isDown)
            {
                MoveDirection = MoveDirectionType.Down;
            }
            else
            {
                MoveDirection = MoveDirectionType.None;
            }

            if (isUp)
            {
                HandleJump(deltaTime);
            }

            if (isJumping)
            {
                move = - (int)(jumpSpeed * deltaTime);
                jumpTime += deltaTime;
                MoveDirection = MoveDirectionType.Up;

                if (jumpTime > maxJumpTime)
                {
                    isJumping = false;
                    jumpTime = 0;
                }
            }
            else
            {
                move = (int)(fallSpeed * deltaTime);
            }

            return move;
        }

        private void HandleJump(float deltaTime)
        {
            if (lastJumpTime > maxJumpTime * 2)
            {
                isJumping = true;
                lastJumpTime = 0;
            }
        }
    }
}
