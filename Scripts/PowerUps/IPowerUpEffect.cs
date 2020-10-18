using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PowerUps
{
    interface IPowerUpEffect
    {

        void OnStart();
        void Effect();
        void OnFinish();
        List<int> GetAffectedSubScenes();
    }
}
