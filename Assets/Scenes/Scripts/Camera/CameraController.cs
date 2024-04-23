using System;
using UnityEngine;

namespace LearnGame.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 myFollowCameraOffset = Vector3.zero;
        [SerializeField]
        private Vector3 myRollCameraOffset = Vector3.zero;

        public PlayerCharacter Player { get; set; }

        protected private void Awake()
        {
        }

        protected void LateUpdate()
        {
            if (!Player)
            {
                return;
            }

            Vector3 targetRoll = myRollCameraOffset - myFollowCameraOffset;
            transform.position = Player.transform.position + myFollowCameraOffset;
            transform.rotation = Quaternion.LookRotation(
                targetRoll, Vector3.up);
        }
    }
}