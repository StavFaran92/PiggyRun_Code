using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Commands
{
    class CommandBehaviour : ICommand
    {
        //protected IActor actor;
        PRActions action;

        //public void SetActor(IActor actor)
        //{
        //    this.actor = actor;
        //}

        public void SetAction(PRActions action)
        {
            this.action = action;
        }

        public void Execute(IReceiver recevier)
        {
            if (recevier != null)
            {
                recevier.TryPerform(action);
            }
            else
            {
                Debug.Log("Receiver is null");
            }
        }
    }

    /// <summary>
    /// Command Behaviour Builder
    /// </summary>
    public class CommandBehaviourBuilder
    {
        private CommandBehaviour command = new CommandBehaviour();

        //public CommandBehaviourBuilder WithActor(IActor actor)
        //{
        //    command.SetActor(actor);
        //    return this;
        //}

        public CommandBehaviourBuilder WithAction(PRActions action)
        {
            command.SetAction(action);
            return this;
        }

        public ICommand Build()
        {
            return command;
        }


    }

    ///// <summary>
    ///// An actor as an interface for objects that accept command behaviours,
    ///// it is an extension of receiver.
    ///// </summary>
    //public interface IActor : IReceiver
    //{
    //    //default void handle(ICommand command)
    //    //{
    //    //    TryPerform(((CommandActor)command).GetAbility());
    //    //}

    //    void TryPerform(int ability);
    //}
}
