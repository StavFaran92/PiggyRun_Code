using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameEventManager : MyEventManager
    {

        //-- Singleton
        public static GameEventManager Instance = new GameEventManager();

        //GameEventManager()
        //{
        //    mEventsMap.Add(ApplicationConstants.EVENT_PLAYER_JUMP, new List<Action<string, ActionParams>>());
        //    mEventsMap.Add(ApplicationConstants.EVENT_PLAYER_SLIDE, new List<Action<string, ActionParams>>());
        //    mEventsMap.Add(ApplicationConstants.EVENT_PLAYER_DOUBLE_JUMP, new List<Action<string, ActionParams>>());
        //    mEventsMap.Add(ApplicationConstants.EVENT_END_OF_TUTORIAL, new List<Action<string, ActionParams>>());
        //    mEventsMap.Add(ApplicationConstants.EVENT_DEPLOYMENT_TUBE_MANAGER_FINISH_INIT, new List<Action<string, ActionParams>>());
        //    mEventsMap.Add(ApplicationConstants.EVENT_GAME_CINEMATICS_OVER, new List<Action<string, ActionParams>>());
        //    mEventsMap.Add(ApplicationConstants.EVENT_CINEMATIC_DEATH_BY_HIT_OVER, new List<Action<string, ActionParams>>());
        //    mEventsMap.Add(ApplicationConstants.EVENT_PLAYER_DIE_BY_FALL, new List<Action<string, ActionParams>>());
        //    mEventsMap.Add(ApplicationConstants.EVENT_PLAYER_DIE_BY_HIT, new List<Action<string, ActionParams>>());
        //}
    }
}
