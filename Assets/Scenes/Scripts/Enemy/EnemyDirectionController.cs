using UnityEngine;
using System.Collections;
using LearnGame.Movement;

namespace LearnGame.Enemy
{
    public class EnemyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        public Vector3 MovementDirection { get; private set; }
        public bool IsRunning { get; private set; } = false;
        public bool IsRetreating { get; set; } = false;

        public void UpdateMovementDirection(Vector3 targetPosition, bool isDirect = true)
        {
            int aScale = isDirect ? 1 : -1;
            var realDirection = aScale * (targetPosition - transform.position);
            MovementDirection = new Vector3 (realDirection.x, 0f, realDirection.z).normalized;
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