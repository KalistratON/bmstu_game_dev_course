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

        [SerializeField]
        private PlayerCharacter myPlayer;

        protected private void Awake()
        {
            if (myPlayer == null)
            {
                throw new NullReferenceException($"Can't follow null player");
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        protected void LateUpdate()
        {
            Vector3 targetRoll = myRollCameraOffset - myFollowCameraOffset;
            transform.position = myPlayer.transform.position + myFollowCameraOffset;
            transform.rotation = Quaternion.LookRotation(
                targetRoll, Vector3.up);

        }
    }
}