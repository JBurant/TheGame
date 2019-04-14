using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.Enums;

namespace TheGame.Objects
{
    public class Critter : ObjectInGame
    {
        public MoveDirectionType MoveDirection { get; set; }

        public Critter(int x, int y, string textureFile, float speed) : base(x, y, textureFile, true)
        {
            Speed = speed;
            MoveDirection = MoveDirectionType.None;
        }

        public Critter(int x, int y, string textureFile, float speed, MoveDirectionType initialMoveDirection) : base(x, y, textureFile)
        {
            Speed = speed;
            MoveDirection = initialMoveDirection;
        }

        public override void Move(float deltaTime)
        {
            if(MoveDirection == MoveDirectionType.Left)
            {
                MoveLeft(deltaTime);
            }

            if(MoveDirection == MoveDirectionType.Right)
            {
                MoveRight(deltaTime);
            }
        }

        public override void NonLethalCollision(Rectangle hitbox)
        {
            if (MoveDirection == MoveDirectionType.Left)
            {
                MoveDirection = MoveDirectionType.Right;
            }
            else if(MoveDirection == MoveDirectionType.Right)
            {
                MoveDirection = MoveDirectionType.Left;
            }
        }
    }
}
