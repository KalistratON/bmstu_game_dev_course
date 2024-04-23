using UnityEngine;
using UnityEditor;
using System.Collections;
using LearnGame.Enemy;

namespace LearnGame
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField]
        private float myRange = 2f;

        private static bool IsPlayerSpawned = false;

        // Use this for initialization
        void Start()
        {
            var randomPointInsideRange = Random.insideUnitCircle * myRange;
            var randomPosition = new Vector3(randomPointInsideRange.x, 1f, randomPointInsideRange.y);
            randomPosition += transform.position;

            float random = Random.Range(0f, 1f);
            if (random > 0 && !IsPlayerSpawned)
            {
                IsPlayerSpawned = true;
                Instantiate(Resources.Load("Player"), randomPosition, Quaternion.identity, transform);
            }
            else
            {
                Instantiate(Resources.Load("Enemy"), randomPosition, Quaternion.identity, transform);
            }
        }

        // Update is called once per frame
        protected void Update()
        {
        }

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, myRange);
            Handles.color = cashedColor;
        }
    }
}
