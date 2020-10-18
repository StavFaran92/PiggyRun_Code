//using Assets.Scripts.Communication.BluePrints;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace Assets.Scripts.Commands
//{
//    /// <summary>
//    /// Command Creation Builder
//    /// </summary>
//    public class CommandCreationBuilder
//    {
//        private CommandCreation command = new CommandCreation();


//        public CommandCreationBuilder WithPosition(Vector3 position)
//        {
//            command.SetPosition(position);
//            return this;
//        }

//        public CommandCreationBuilder SetBluePrint(BluePrint bluePrint)
//        {
//            command.SetBluePrint(bluePrint);

//            return this;

//            //data = new BluePrintItem() { mType = ItemType.CoinGold }
//        }

//        public CommandCreationBuilder SetDelay(float delay)
//        {
//            command.SetDelay(delay);

//            return this;

//            //data = new BluePrintItem() { mType = ItemType.CoinGold }
//        }

//        public CommandCreationBuilder SetCountDownHolder(CountDownRoot holder)
//        {
//            command.SetCountDownHolder(holder);

//            return this;

//            //data = new BluePrintItem() { mType = ItemType.CoinGold }
//        }

//        //public CommandCreationBuilder OfElement(Element element)
//        //{
//        //    command.SetElement(element);
//        //    return this;
//        //}

//        public ICommand Build()
//        {
//            return command;
//        }
//    }

//    class CommandCreation : ICommand
//    {
//        Vector3 position;
//        BluePrint mBluePrint;
//        CountDownRoot holder;
//        float delay = 0;

//        private IBuilder builder;


//        public void SetPosition(Vector3 position)
//        {
//            this.position = position;
//        }

//        public void SetDelay(float delay)
//        {
//            this.delay = delay;
//        }

//        public void SetBluePrint(BluePrint bluePrint)
//        {
//            //builder = LevelBuilder.Instance;
//            this.mBluePrint = bluePrint;
//        }

//        public void SetCountDownHolder(CountDownRoot holder)
//        {
//            this.holder = holder;
//        }

//        public void Execute(IReceiver receiver)
//        {
//            builder.Build(mBluePrint, position, holder, delay);
//        }
//    }
//}
