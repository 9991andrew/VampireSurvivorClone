using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vampire_Survivor.Misc
{
    public struct Config
    {
        public Vector2 pos;
        public int speed;
        public Vector2 origin;
        public int health;
        public float rotation;
        public float scale;
        public float depth;
        public string idleAnim;
        public string walkAnim;
        public int frameCountIdle;
        public int framesPerSecIdle;
        public int frameCountWalk;
        public int framesPerSecWalk;
    }
}
