using Assets.Scripts.SystemAI;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameCinematics : MonoBehaviour
    {

        [SerializeField]
        private GameObject player;
        //[SerializeField]
        //private GameObject foreground;
        [SerializeField]
        private Transform camera;
        [SerializeField]
        private Transform wolf_1;
        [SerializeField]
        private Transform wolf_2;
        [SerializeField]
        private Transform HUD;

        SubScene subScene;



        private Transform mPlayerTransform;
        //private GroundObject mForeground;



        private void Start()
        {
            //--Register as game cinematics in scene
            subScene = GetComponentInParent<SubScene>();
            subScene.SetGameCinematics(this);

            mPlayerTransform = player.transform;
            //mForeground = foreground.GetComponent<GroundObject>();
            DOTween.Init();

            PlayStartCinematics();

            //--Listen to events
            subScene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_DIE_BY_HIT, PlayDeathByHitCinematics);
        }

        public void PlayStartCinematics()
        {
            //mForeground.LocalSpeed = 4;
            //DOTween.To(() => mForeground.LocalSpeed, x => mForeground.LocalSpeed = x, 1, 5)
            //    .SetEase(Ease.InCubic);
            mPlayerTransform.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            mPlayerTransform.DOMoveX(-15.5f, 5)
                .SetEase(Ease.InOutCubic)
                .OnComplete( () => {
                    GameEventManager.Instance.CallEvent(ApplicationConstants.EVENT_GAME_CINEMATICS_OVER, ActionParams.EmptyParams);
                    HUD.DOMoveX(-6, 2);
                    mPlayerTransform.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    });

            

        }

        public void PlayDeathByHitCinematics(string eventName, ActionParams actionParams)
        {
            Debug.Log("game over cinematics");
            subScene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_PLAYER_DIE_BY_HIT, PlayDeathByHitCinematics);


            camera.DOMoveX(-10, 4)
                .SetEase(Ease.OutCubic)
                .OnComplete(()=> {
                    GameEventManager.Instance.CallEvent(ApplicationConstants.EVENT_CINEMATIC_DEATH_BY_HIT_OVER, ActionParams.EmptyParams);
                    //CommonRequests.RequestGameOver();
                    });

            wolf_1.DOMoveX(-17.5f, 2);
            wolf_2.DOMoveX(-12.1f, 2f).OnComplete(()=>wolf_2.localScale = new Vector3(-1,1,0));


        }

    }
}

