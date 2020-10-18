using Assets.Scripts.Commands;

namespace Assets.Scripts
{
    public class Request
    {
        enum Status { Created, Pending, Approved, Declined }
        Status status { get; set; }
        IReceiver receiver;
        ICommand command;

        private Request()
        {
            status = Status.Created;
        }

        void SetReceiver(IReceiver receiver)
        {
            this.receiver = receiver;
        }

        void SetCommand(ICommand command)
        {
            this.command = command;
        }

        public void Execute()
        {
            handle(command);
        }

        private void handle(ICommand command)
        {
            command.Execute(receiver);
        }

        /// <summary>
        /// Request Builder
        /// </summary>
        public class RequestBuilder
        {
            private Request request = new Request();

            public RequestBuilder WithReceiver(IReceiver receiver)
            {
                request.SetReceiver(receiver);
                return this;
            }

            public RequestBuilder WithCommand(ICommand command)
            {
                request.SetCommand(command);
                return this;
            }

            public Request Build()
            {
                return request;
            }
        }

        /// <summary>
        /// Request Factory
        /// </summary>
        /// <returns></returns>
        public static RequestBuilder CreateRequest()
        {
            return new RequestBuilder();
        }
    }

    

    

    

    //class CommandCreateElement : CommandCreation
    //{
    //    Element element;
    //    Vector3 position;

    //    private CommandCreateElement(Element element, Vector3 position)
    //    {
    //        this.element = element;
    //        this.position = position;
    //    }

    //    public override void Execute()
    //    {
    //        builder.Build(element, position);
    //    }
    //}


    //class CommandActor : CommandBehaviour
    //{
    //    Action action;

    //    public CommandActor(Action action)
    //    {
    //        this.action = action;
    //    }

    //    public Action GetAction()
    //    {
    //        return action;
    //    }

    //    public override void Execute()
    //    {
    //        actor.TryPerform(action);
    //    }



    //}

    //public enum PlayerAction
    //{
    //    TouchPressed,
    //    TouchReleased,
    //    HitGround,
        
    //    CreateHole,
    //    ReachMaxCharge,
    //    ReachMaxAltitude,
    //    Leaping
    //}

    public enum ItemType
    {
        CoinGold,
        CoinGreen,
        CoinRed
    }

    

    //public class Element<T> where T : BluePrint
    //{
    //    Vector3 position;
    //    BluePrintHolder<T> bph;
    //}


    

    

    

    

    
}
