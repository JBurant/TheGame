using TheGame.Enums;

namespace TheGame.Move
{
    public class CritterMoveHandler
    {
        private readonly int speed;
        public MoveDirectionType moveDirection;

        public CritterMoveHandler(int speed, MoveDirectionType initialMoveDirection)
        {
            this.speed = speed;
            moveDirection = initialMoveDirection;
        }

        public int Move(float deltaTime)
        {
            if (moveDirection == MoveDirectionType.Left)
            {
                return (int)(- speed * deltaTime);
            }

            if (moveDirection == MoveDirectionType.Right)
            {
                return (int)(speed * deltaTime);
            }

            return 0;
        }

        public void TurnAround()
        {
            if (moveDirection == MoveDirectionType.Left)
            {
                moveDirection = MoveDirectionType.Right;
            }
            else if (moveDirection == MoveDirectionType.Right)
            {
                moveDirection = MoveDirectionType.Left;
            }
        }
    }
}
