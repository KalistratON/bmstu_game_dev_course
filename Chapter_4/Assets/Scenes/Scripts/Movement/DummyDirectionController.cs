using UnityEngine;
using System.Collections;

namespace LearnGame.Movement
{

    public class DummyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        public Vector3 MovementDirection { get; private set; }
        public bool IsRunning { get; private set; }

        protected void Awake()
        {
            MovementDirection = Vector3.zero;
            IsRunning = false;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}