using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.WorldObjects
{
    class Parallax : MonoBehaviour
    {
        private float length, startpos;
        [SerializeField] private float parallaxEffect;
        private Transform mCameraTransform;

        private void Start()
        {
            startpos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;

            mCameraTransform = GetComponentInParent<SubScene>().GetServiceManager()
                .GetService<CameraController>(ApplicationConstants.SERVICE_CAMERA_CONTROLLER).transform;
        }

        private void Update()
        {
            float temp = (mCameraTransform.position.x * (1 - parallaxEffect));
            float dist = (mCameraTransform.position.x * parallaxEffect);

            transform.position = new Vector3(startpos + dist, transform.position.y);

            if (temp > startpos + length) startpos += length;
            else if (temp < startpos - length) startpos -= length;
        }
    }
}
