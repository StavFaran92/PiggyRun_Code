using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SystemAI
{
    class DifficultyManager
    {

        private static int mGameDifficulty = 1; 

        public static DifficultyManager Instance { get; } = new DifficultyManager();

        public void GiveMoneyTo(EventSelector eventSelector)
        {
            eventSelector.GiveMoney(mGameDifficulty * 3); 
        }
    }
}
