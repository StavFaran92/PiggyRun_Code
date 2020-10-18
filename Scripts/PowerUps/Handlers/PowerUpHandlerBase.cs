using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PowerUps
{
    public class PowerUpHandlerBase : IPowerUpHandler
    {
        public static readonly PowerUpHandlerBase powerUpDefaultHandler = new PowerUpHandlerBase();

        protected WorldObject mWorldObject;
        protected bool mIsAffectedByMagnet { get; set; } = false;

        private PowerUpHandlerBase()
        {

        }

        public  PowerUpHandlerBase(WorldObject worldObject)
        {
            mWorldObject = worldObject;
        }

        public virtual void StartEffect(PowerUpManager.PowerUpEffect effect)
        {
        }

        public virtual void StopEffect(PowerUpManager.PowerUpEffect effect)
        {
        }

        public virtual void Update()
        {
        }
    }
}
