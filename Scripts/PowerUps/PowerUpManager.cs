using Assets.Scripts.Commands;
using Assets.Scripts.PowerUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// This is the Bridge between The Game Manager and the PowerUps objects.
    /// It handles the current PowerUp equiped
    /// </summary>
    public class PowerUpManager
    {
        public enum PowerUpEffect
        {
            Magnet,
            Freeze,
            None,
            DoubleCash,
            Speed,
            Shield,
            Health
        }

        //private PowerUpType _powerUpSlot = PowerUpType.POWERUP_NULL;

            private PRActions TypeToAction(PowerUpEffect effect)
        {
            switch (effect)
            {
                case PowerUpEffect.None:
                    return PRActions.POWERUP_NONE;
                case PowerUpEffect.Magnet:
                    return PRActions.POWERUP_MAGNET;
                default:
                    return PRActions.NULL;
            }
        }

        public void CollectedPowerUp(PowerUpEffectBase powerUpEffect, int index)
        {
            List<int> affectedSubScenes = powerUpEffect.GetAffectedSubScenes(index);

            foreach( int i in affectedSubScenes)
            {
                if (i > 2 || i < 0)
                    throw new Exception("Not a valid subscene!");

                PRActions action = TypeToAction(powerUpEffect.Effect);

                //todo fix

                //ICommand command = CommandFactory
                //    .CreateCommandBehaviour()
                //    .WithAction(action)
                //    .Build();

                //Request request = Request
                //    .CreateRequest()
                //    .WithReceiver(GameManager.GetInstance().GetReceiver(ReceiverId.Scenes, i))
                //    .WithCommand(command)
                //    .Build();

                //GameManager.GetInstance().SendMessage("AddRequest", request);
            }
        }

        //public void CollectedPowerUpOfType(PowerUpType type)
        //{
        //    if(_powerUpSlot == PowerUpType.POWERUP_NULL)
        //    {
        //        _powerUpSlot = type;
        //    }
        //}

        //public void ActivatePowerUp()
        //{

        //}
    }
}
