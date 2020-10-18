using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PowerUps
{
    public interface IPowerUpHandler
    {
        void StartEffect(PowerUpManager.PowerUpEffect effect);
        void StopEffect(PowerUpManager.PowerUpEffect effect);
        void Update();
    }
}
