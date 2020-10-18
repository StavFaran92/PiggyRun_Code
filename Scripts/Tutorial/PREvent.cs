using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tutorial
{
    [CreateAssetMenu(fileName = "PR Event", menuName = "Tutorial/PR Event")]
    class PREvent : ScriptableObject
    {
        

        [Header("Blueprint Attributes")]
        [SerializeField]
        public GameEventFactory.PREventType type;

        [SerializeField]
        public int level;

        [SerializeField]
        public int index;

        [Header("Deployment Attributes")]
        [SerializeField]
        public int scene;


    }
}
