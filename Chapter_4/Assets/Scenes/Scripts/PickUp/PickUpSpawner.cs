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

        private float myCurrentSpawnTimerSeconds;
        private int myCurrentCount;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        protected void Update()
        {
            if (myCurrentCount < myMaxCount)
            {
                myCurrentSpawnTimerSeconds += Time.deltaTime;
                if (myCurrentSpawnTimerSeconds > Random.Range(mySpawnMinIntervalSeconds, mySpawnMaxIntervalSeconds))
                {
                    myCurrentSpawnTimerSeconds = 0f;
                    myCurrentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * myRange;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0, randomPointInsideRange.y);
                    randomPosition += transform.position;

                    var pickUp = Instantiate(myPickUpPrefab, randomPosition, Quaternion.identity, transform);
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
