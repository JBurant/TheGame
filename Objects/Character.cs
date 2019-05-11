using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheGame.Animation;
using TheGame.Collision;
using TheGame.Enums;
using TheGame.Move;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Character : ForegroundObject
    {
        private CharacterMoveHandler moveHandler;

        public Character(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo, 2, -300)
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

        public int LethalCollision(ForegroundObject enemy, int frameWhenDied)
        {
            if (CollisionResolver.DetectCollision(Hitbox, enemy.Hitbox))
            {
                if (!(LastPosition.Y < Position.Y))
                {
                    return Die(frameWhenDied);
                }
                else
                {
                    return enemy.Die(frameWhenDied);
                }
            }
            return 0;
        }

        public int ItemCollision(Item item, int frame)
        {
            if (CollisionResolver.DetectCollision(Hitbox, item.Hitbox) && moveHandler.IsPickingUp())
            {
                return item.Die(frame);
            }
            return 0;
        }

        public virtual void LandscapeCollision(Landscape landscape)
        {
            if (CollisionResolver.DetectCollision(Hitbox, landscape.Hitbox))
            {
                Position = CollisionResolver.SolidObjectsCollision(Hitbox, landscape.Hitbox, Position, LastPosition);
            }
        }

        public int CheckForFallDeath(int currentFrame, int maxHeight)
        {
            if (Position.Y > maxHeight)
            {
                return Die(currentFrame);
            }
            return 0;
        }

        public override void Draw(SpriteBatch spriteBatch, int worldPosition)
        {
            var sourceAnimation = animationResolver.GetAnimation(Position.X, moveHandler.moveDirection);
            spriteBatch.Draw(Texture, new Vector2(Position.X - worldPosition, Position.Y), sourceAnimation, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}