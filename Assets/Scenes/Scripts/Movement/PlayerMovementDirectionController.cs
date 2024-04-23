using UnityEngine;

namespace LearnGame.Movement
{ 

    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private UnityEngine.Camera myCamera;
        public Vector3 MovementDirection { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsRetreating { get; private set; } = false;
        // Use this for initialization

        protected void Awake()
        {
            myCamera = UnityEngine.Camera.main;
        }

        protected void Update()
        {
            var hor = Input.GetAxis("Horizontal");  
            var ver = Input.GetAxis("Vertical");

            IsRunning = Input.GetKey(KeyCode.Space);

            var direction = new Vector3(hor, 0, ver);
            direction = myCamera.transform.rotation * direction;
            direction.y = 0;

            MovementDirection = direction.normalized;
        }
    }

}
