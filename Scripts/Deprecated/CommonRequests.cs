using Assets.Scripts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class CommonRequests
    {
        //public static void RequestGameOver()
        //{
        //    ICommand command = CommandFactory.CreateCommandBehaviour()

        //        .WithAction(PRActions.GAME_OVER)
        //        .Build();

        //    Request request = Request.CreateRequest()
        //        .WithReceiver(GameManager.GetInstance())
        //        .WithCommand(command)
        //        .Build();

        //    GameManager.GetInstance().SendMessage("AddRequest", request);
        //}

        //public static void RequestPowerUpActivation(PowerUpManager.PowerUpEffect powerup, int sceneIndex)
        //{
        //    ICommand command = CommandFactory.CreateCommandBehaviour()

        //        .WithAction(PRActions.POWERUP_MAGNET)
        //        .Build();

        //    Request request = Request.CreateRequest()
        //        .WithReceiver(GameManager.GetInstance().GetReceiver(ReceiverId.Scenes, sceneIndex))
        //        .WithCommand(command)
        //        .Build();

        //    GameManager.GetInstance().SendMessage("AddRequest", request);
        //}
    }
}
