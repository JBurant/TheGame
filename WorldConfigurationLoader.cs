using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.Configuration;

namespace TheGame
{
    public static class WorldConfigurationLoader
    {
        public static WorldConfiguration GetConfiguration()
        {
            return new WorldConfiguration();
        }
    }
}
