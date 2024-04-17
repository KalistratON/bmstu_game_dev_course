using UnityEngine;

namespace LearnGame.Movement
{ 

    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private UnityEngine.Camera myCamera;
        public Vector3 MovementDirection { get; private set; }
        // Use this for initialization


        protected void Awake()
        {
            myCamera = UnityEngine.Camera.main;
        }

        void Start()
        {

        }

        // Update is called once per frame
        protected void Update()
        {
            var hor = Input.GetAxis("Horizontal");  
            var ver = Input.GetAxis("Vertical");

            var direction = new Vector3(hor, 0, ver);
            direction = myCamera.transform.rotation * direction;
            direction.y = 0;

            MovementDirection = direction.normalized;
        }
    }

}
