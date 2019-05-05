using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheGame.Animation;
using TheGame.Collision;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class ForegroundObject : ObjectInGame
    {
        public ObjectStateType State { get; set; }
        public int FrameWhenDied { get; set; }

        public Rectangle Hitbox { get; set; }

        protected MoveDirectionType MoveDirection { get; set; }
        public Point LastPosition { get; set; }

        public ForegroundObject(int x, int y, TextureInfo textureInfo, float scale, MoveDirectionType initialMoveDirection) : base(x, y, textureInfo, scale)
        {
            MoveDirection = initialMoveDirection;
            Initialize(textureInfo);
        }

        public ForegroundObject(int x, int y, TextureInfo textureInfo, float scale) : base(x, y, textureInfo, scale)
        {
            MoveDirection = MoveDirectionType.None;
            Initialize(textureInfo);
        }

        protected override void Initialize(TextureInfo textureInfo)
        {
            State = ObjectStateType.Asleep;
            animationResolver = new MovingAnimationResolver(textureInfo, new int[] { 0, 0 }, new int[] { 0, 0 });
        }

        public virtual void Move(float deltaTime)
        {
        }

        public void SetHitbox()
        {
            Hitbox = new Rectangle(Position,Size);
        }

        public virtual void NonLethalCollision(Rectangle hitbox2)
        {
            Position = CollisionResolver.SolidObjectsCollision(Hitbox, hitbox2, Position, LastPosition);
        }

        public virtual bool DetectCollision(Rectangle hitbox2)
        {
            return CollisionResolver.DetectCollision(Hitbox, hitbox2);
        }

        public virtual void Die(int frameWhenDied)
        {
            FrameWhenDied = frameWhenDied;
            State = ObjectStateType.Dead;
        }

        public void SaveLastPosition()
        {
            LastPosition = Position;
        }

        public override void Draw(SpriteBatch spriteBatch, int worldPosition)
        {
            var sourceAnimation = animationResolver.GetAnimation(Position.X, MoveDirection);
            spriteBatch.Draw(Texture, new Vector2(Position.X - worldPosition, Position.Y), sourceAnimation, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}
