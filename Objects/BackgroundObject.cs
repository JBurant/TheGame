using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.Objects
{
    public class BackgroundObject : ObjectInGame
    {
        public BackgroundObject(int x, int y, string textureFile) : base(x, y, textureFile, false)
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
