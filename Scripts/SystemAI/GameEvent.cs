using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SystemAI
{
    public class GameEvent
    {
        //public string mJsonData { set; get; }
        public ConstructData ConstructData;
        private int mLevel;
        public int mCost;
        private string mType;
        public string mName;
        //private List<GameEventType> mCanAttachToUp;
        //private List<GameEventType> mCanAttachToForward;
        //private List<GameEventType> mCanAttachToDown;
        //private Dictionary<AttachmentType, List<GameEventType>> mAttachments;
        //private GameEvent mForward { get; set; }
        //private GameEvent mUp{ get; set; }
        public GameEvent m_Next{ get; set; }
        private int mScene;

        public GameEvent(string name, string mJsonData, int mLevel, int mCost, string mType, int offset=0)
        {
            this.mName = name;
            //this.mJsonData = mJsonData;
            this.mLevel = mLevel;
            this.mCost = mCost;
            this.mType = mType;

            ConstructData = Utils.ParseJsonData<ConstructData>(mJsonData);
            ConstructData.Offset = offset;
            //mCanAttachToUp = new List<GameEventType>() ;
            //mCanAttachToForward = new List<GameEventType>();
            //mCanAttachToDown = new List<GameEventType>();
            //mAttachments = new Dictionary<AttachmentType, List<GameEventType>>();
            //mAttachments.Add(AttachmentType.Up, mCanAttachToUp);
            //mAttachments.Add(AttachmentType.Forward, mCanAttachToForward);
            //mAttachments.Add(AttachmentType.Down, mCanAttachToDown);
        }

        //public List<GameEventType> GetAttachmentOptions(AttachmentType attachment)
        //{
        //    mAttachments.TryGetValue(attachment, out List<GameEventType> attachmentsList);

        //    return attachmentsList;
        //}

        internal int GetCost()
        {
            return mCost;
        }

        public int GetOffset()
        {
            return ConstructData.Offset;
        }

        public void SetOffset(int offset)
        {
            ConstructData.Offset = offset;
        }



        internal string GetEventType()
        {
            return mType;
        }

        internal void Attach(GameEvent gEventInspected)
        {
            m_Next = gEventInspected;

            //switch (attachment)
            //{
            //    case AttachmentType.Up:
            //        mUp = gEventInspected;
            //        break;
            //    case AttachmentType.Forward:
            //        mForward = gEventInspected;
            //        break;
            //    case AttachmentType.Down:
            //        m_Next = gEventInspected;
            //        break;
              
            //}
        }

        public bool HasNext()
        {


            //GameEvent curr = this;

            //int countloop = 100;
            //while (mForward != null && --countloop > 1)
            //{
            //    curr = mForward;
            //}
            //if(countloop == 1)
            //{
            //    Debug.Log("GetLength");
            //}

            return m_Next != null;
        }

        internal void SetScene(int currScene)
        {
            this.mScene = currScene;
        }

        public int GetSceneIndex()
        {
            return mScene;
        }
    }

    //TODO create a puzzle system

    public enum GameEventType { Obstacles, Goods };
}
