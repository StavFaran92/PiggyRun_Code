using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PowerUps.Handlers
{
    class PowerUpHandlerCoin : PowerUpHandlerBase
    {
        public PowerUpHandlerCoin(WorldObject worldObject) : base(worldObject)
        {
        }

        


        public override void StartEffect(PowerUpManager.PowerUpEffect effect)
        {
            switch (effect)
            {
                case PowerUpManager.PowerUpEffect.Magnet:
                    mIsAffectedByMagnet = true;
                    break;
            }
            
        }

        public override void Update()
        {
            if (mWorldObject.IsRendered())
            {
                if (mIsAffectedByMagnet)
                {
                    ((CoinObject)mWorldObject).MoveTowardsPlayer(.3f);
                }
                //transform.position = Vector3.Lerp(transform.position, mPlayer.transform.position, Time.time);
            }
        }

        public override void StopEffect(PowerUpManager.PowerUpEffect effect)
        {
            switch (effect)
            {
                case PowerUpManager.PowerUpEffect.Magnet:
                    mIsAffectedByMagnet = false;
                    break;
            }
        }
    }
}
