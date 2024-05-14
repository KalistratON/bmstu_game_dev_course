using UnityEngine;

using System;

namespace LearnGame.Movement
{

    public class DummyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        public Vector3 MovementDirection { get; private set; }

        public void UpdateMovementDirection (Vector3 targetPosition, bool isDirect) { }

        protected void Awake()
        {
            MovementDirection = Vector3.zero;
        }
    }
}