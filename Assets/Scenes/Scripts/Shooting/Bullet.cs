using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnGame.Shooting
{

    public class Bullet : MonoBehaviour
    {
        private Vector3 myDirection;
        private float mySpeed;
        private float myMaxDistance;
        private float myCurDistance;

        public float Damage { get; private set; }

        public void Initialize(Vector3 Direction, float MaxDistance, float Speed, float theDamage)
        {
            myDirection = Direction;
            myMaxDistance = MaxDistance;
            mySpeed = Speed;
            Damage = theDamage;
        }

        protected void Update()
        {
            var delta = mySpeed * Time.deltaTime;
            myCurDistance += delta;
            transform.Translate(myDirection * delta);

            if (myCurDistance >= myMaxDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}
