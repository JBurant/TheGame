using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class BackgroundObject : ObjectInGame
    {
        public BackgroundObject(int x, int y, TextureInfo textureInfo, float scale) : base(x, y, textureInfo, scale)
        {
            Speed = 0;
        }

        public override void SetHitbox()
        {
            AdjustPosition();
        }

        public override void RecalculateHitBox()
        {

        }
    }
}
