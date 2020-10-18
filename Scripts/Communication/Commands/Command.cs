using Assets.Scripts.Communication.BluePrints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Commands
{
    public interface ICommand
    {
        void Execute(IReceiver recevier);
    }


    public class CommandFactory
    {
        /// <summary>
        /// Command Behaviour Factory
        /// </summary>
        /// <returns></returns>
        public static CommandBehaviourBuilder CreateCommandBehaviour()
        {
            return new CommandBehaviourBuilder();
        }

        ///// <summary>
        ///// Command Creation Factory
        ///// </summary>
        ///// <returns></returns>
        //public static CommandCreationBuilder CreateCommandCreation()
        //{
        //    return new CommandCreationBuilder();
        //}
    }

    /// <summary>
    /// A receiver is an interface for objects that want to handle commands have to implement
    /// </summary>
    public interface IReceiver
    {
        void TryPerform(PRActions action);
    }
}
