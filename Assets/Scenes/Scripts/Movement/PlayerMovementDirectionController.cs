using UnityEngine;

using System;

namespace LearnGame.Movement
{ 

    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        public event Action<float> OnRunning;


        [SerializeField]
        private float myRunSpeedMulty = 1.0f;


        public Vector3 MovementDirection { get; private set; }
        public bool IsRunning { get; private set; } = false;


        private UnityEngine.Camera myCamera;


        protected void Awake()
        {
            myCamera = UnityEngine.Camera.main;
        }

        protected void Update()
        {
            var aHor = Input.GetAxis ("Horizontal");  
            var aVer = Input.GetAxis ("Vertical");

            if (IsRunning != Input.GetKey (KeyCode.Space))
            {
                IsRunning = Input.GetKey (KeyCode.Space);
                OnRunning?.Invoke (IsRunning ? myRunSpeedMulty : 1/myRunSpeedMulty);
            }

            var aDirection = new Vector3 (aHor, 0, aVer);
            aDirection = myCamera.transform.rotation * aDirection;
            aDirection.y = 0;

            MovementDirection = aDirection.normalized;
        }
    }

}
