using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheGame.Animation;
using TheGame.Collision;
using TheGame.Enums;
using TheGame.Move;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Critter : ForegroundObject
    {
        protected CritterMoveHandler moveHandler;

        public Critter(int x, int y, TextureInfo textureInfo, float scale, int speed, MoveDirectionType initialMoveDirection, int score) : base(x, y, textureInfo, scale, score)
        {
            moveHandler = new CritterMoveHandler(speed, initialMoveDirection);
        }

        protected override void Initialize(TextureInfo textureInfo)
        {
            animationResolver = new MovingAnimationResolver(textureInfo, new int[] { 0, 2 }, new int[] { 1, 3 });
        }

        public void Move(float deltaTime)
        {
            Position.X += moveHandler.Move(deltaTime);

            if (Position.X < 0)
            {
                Position.X = 0;
            }
        }

        public void LandscapeCollision(Landscape landscape)
        {
            if (CollisionResolver.DetectCollision(Hitbox, landscape.Hitbox))
            {
                moveHandler.TurnAround();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, int worldPosition)
        {
            var sourceAnimation = animationResolver.GetAnimation(Position.X, moveHandler.moveDirection);
            spriteBatch.Draw(Texture, new Vector2(Position.X - worldPosition, Position.Y), sourceAnimation, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}
