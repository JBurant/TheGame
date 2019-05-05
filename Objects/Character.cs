using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheGame.Animation;
using TheGame.Enums;
using TheGame.Move;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Character : ForegroundObject
    {
        private CharacterMoveHandler moveHandler;

        public Character(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo, 2)
        {
            animationResolver = new CharacterAnimationResolver(textureInfo, new int[] { 1, 5 }, new int[] { 2, 6 });
            moveHandler = new CharacterMoveHandler(300, 300, 300);
        }

        public void Move(bool isUp, bool isDown, bool isRight, bool isLeft, float deltaTime)
        {
            SaveLastPosition();
            Position.Y += moveHandler.MoveVertically(isUp, isDown, deltaTime);
            Position.X += moveHandler.MoveHorizontally(isRight, isLeft, deltaTime);

            if (Position.X < 0)
            {
                Position.X = 0;
            }
        }

        public void LethalCollision(ForegroundObject enemy, int frameWhenDied)
        {
            if (!(LastPosition.Y < Position.Y))
            {
                Die(frameWhenDied);
            }
            else
            {
                enemy.Die(frameWhenDied);
            }
        }

        public void CheckForFallDeath(int currentFrame, int maxHeight)
        {
            if(Position.Y > maxHeight)
            {
                Die(currentFrame);
            }
        }

        public void PickUp(Item item, int frame)
        {
            if(moveHandler.MoveDirection == MoveDirectionType.Down)
            {
                item.Die(frame);
            }            
        }

        public override void Draw(SpriteBatch spriteBatch, int worldPosition)
        {
            var sourceAnimation = animationResolver.GetAnimation(Position.X, moveHandler.MoveDirection);
            spriteBatch.Draw(Texture, new Vector2(Position.X - worldPosition, Position.Y), sourceAnimation, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}