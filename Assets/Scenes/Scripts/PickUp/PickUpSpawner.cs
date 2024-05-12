using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LearnGame.PickUp
{ 
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private PickUpItem myPickUpPrefab;

        [SerializeField]
        private float myRange = 2f;

        [SerializeField]
        private int myMaxCount = 2;

        [SerializeField]
        private float mySpawnMaxIntervalSeconds = 15f;
        [SerializeField]
        private float mySpawnMinIntervalSeconds = 10f;

        private float mySpawnIntervalSeconds;

        private float myCurrentSpawnTimerSeconds;
        private int myCurrentCount;

        // Use this for initialization
        protected void Awake()
        {
            mySpawnIntervalSeconds = Random.Range (mySpawnMinIntervalSeconds, mySpawnMaxIntervalSeconds);
        }

        // Update is called once per frame
        protected void Update()
        {
            if (myCurrentCount < myMaxCount)
            {
                myCurrentSpawnTimerSeconds += Time.deltaTime;
                if (myCurrentSpawnTimerSeconds > mySpawnIntervalSeconds)
                {
                    myCurrentSpawnTimerSeconds = 0f;
                    mySpawnIntervalSeconds = Random.Range (mySpawnMinIntervalSeconds, mySpawnMaxIntervalSeconds);
                    myCurrentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * myRange;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0, randomPointInsideRange.y);
                    randomPosition += transform.position;

                    var pickUp = Instantiate (myPickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;
                }
            }
        }

        private void OnItemPickedUp (PickUpItem pickedUpItem)
        {
            myCurrentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
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
