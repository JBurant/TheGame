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
        public Point LastPosition { get; set; }

        private readonly int score;

        public ForegroundObject(int x, int y, TextureInfo textureInfo, float scale) : base(x, y, textureInfo, scale)
        {
            score = 0;
            Initialize(textureInfo);
        }

        public ForegroundObject(int x, int y, TextureInfo textureInfo, float scale, int score) : base(x, y, textureInfo, scale)
        {
            this.score = score;
            Initialize(textureInfo);
        }

        protected override void Initialize(TextureInfo textureInfo)
        {
            State = ObjectStateType.Asleep;
            animationResolver = new MovingAnimationResolver(textureInfo, new int[] { 0, 0 }, new int[] { 0, 0 });
        }

        public void SetHitbox()
        {
            Hitbox = new Rectangle(Position,Size);
        }

        public virtual int Die(int frameWhenDied)
        {
            FrameWhenDied = frameWhenDied;
            State = ObjectStateType.Dead;
            return score;
        }

        public void SaveLastPosition()
        {
            LastPosition = Position;
        }

        public override void Draw(SpriteBatch spriteBatch, int worldPosition)
        {
            var sourceAnimation = animationResolver.GetAnimation(Position.X, MoveDirectionType.None);
            spriteBatch.Draw(Texture, new Vector2(Position.X - worldPosition, Position.Y), sourceAnimation, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}
