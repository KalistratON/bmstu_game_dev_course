using UnityEngine;
using UnityEditor;

using System;


namespace LearnGame
{
    public class CharacterSpawner : MonoBehaviour
    {
        public static event Action OnPlayerSpawn;


        [SerializeField]
        private float myRange = 2f;

        void Awake()
        {
            var randomPointInsideRange = UnityEngine.Random.insideUnitCircle * myRange;
            var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y);
            randomPosition += transform.position;

            float random = UnityEngine.Random.Range (0f, 1f);
            if (random > 0 && !GameManager.myInstance.IsPlayerExist)
            {
                Instantiate (Resources.Load ("Player"), randomPosition, Quaternion.identity, transform);

                OnPlayerSpawn?.Invoke();
            }
            else
            {
                Instantiate (Resources.Load ("Enemy"), randomPosition, Quaternion.identity, transform);
            }
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
