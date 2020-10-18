using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public enum PRActions {
        //Null
        NULL,

    //Touch
    TOUCH_SWIPE_UP,
    TOUCH_SWIPE_DOWN,
    TOUCH_SWIPE_HOLD,

    //Mouse
    MOUSE_LEFT_BUTTON,
    MOUSE_RIGHT_BUTTON,

    //Player
    PLAYER_HIT_GROUND,
    PLAYER_REACH_MAX_ALT,
    PLAYER_LEAPING,
    PLAYER_SLIDE_OVER,
    PLAYER_DIE,

    //Scene
        SCENE_CREATE_HOLE,

    //GameManager
    GAME_OVER,

            //Powerup Manager
            POWERUP_NONE,
            POWERUP_MAGNET
        
    }

    //class PRActions
    //{

    //    //Null
    //    public const int NULL = 0;

    //    //Touch
    //    public const int TOUCH_SWIPE_UP = 1;
    //    public const int TOUCH_SWIPE_DOWN = 2;
    //    public const int TOUCH_SWIPE_HOLD = 3;
    //    public const int TOUCH_SWIPE_HOLD = 4;
    //    public const int TOUCH_SWIPE_HOLD = 5;

    //    //Player
    //    public const int PLAYER_HIT_GROUND = 6;
    //    public const int PLAYER_REACH_MAX_ALT = 7;
    //    public const int PLAYER_LEAPING = 8;

    //    //Scene
    //    public const int SCENE_CREATE_HOLE = 9;



    //}
}
