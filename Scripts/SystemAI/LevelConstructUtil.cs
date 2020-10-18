using Assets.Scripts.Communication.BluePrints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SystemAI
{
    class LevelConstructUtil
    {

        public static BluePrintConstruct ConvertFromLevelConstructToBluePrint(GameEvent gameEvents)
        {
            BluePrintConstructBuilder builder = BluePrintConstructBuilder.CreateBluePrint();

            builder.AddConstructData(gameEvents.ConstructData);
            
            return builder.Build();
        }

        public static BluePrintConstruct ConvertFromLevelConstructToBluePrint(List<GameEvent> gameEvents)
        {
            BluePrintConstructBuilder builder = BluePrintConstructBuilder.CreateBluePrint();

            List<GameEvent> gameEventsToDeploy = gameEvents;

            foreach (GameEvent gEvent in gameEventsToDeploy)
            {
                ConstructData constructData = gEvent.ConstructData;

                builder.AddConstructData(constructData);
            }

            return builder.Build();
        }
    }
}
