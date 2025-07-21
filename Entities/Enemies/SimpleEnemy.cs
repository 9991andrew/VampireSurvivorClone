using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vampire_Survivor.Entities.Enemies
{
    public class SimpleEnemy : Enemy

    {
        private readonly int minSpeed = 50;
        private readonly int maxSpeed = 100;
        private readonly int speedThreshold = 200;

        public SimpleEnemy(Player target, int initHealth, int damage, string attackPath, int framesPerSec, int frameCount)
            : base(target, initHealth, damage, attackPath, framesPerSec, frameCount){ }
        
        public override void Update(GameTime gameTime)
        {
            float distance = Vector2.Distance(Position, Target.Position);
            float t = MathHelper.Clamp(1f - (distance / speedThreshold), 0f, 1f);
            Speed = (int)MathHelper.Lerp(minSpeed, maxSpeed, t);
            base.Update(gameTime);
        }
    }


}
