using LearnGame.Timer;

using UnityEngine;

using System;

namespace LearnGame.Shooting
{
    public class BulletModel
    {
        public event Action OnBulletDestoy;


        private ITimer myTimer;
        private Vector3 myDirection;
        private float mySpeed;
        private float myMaxDistance;
        private float myCurDistance;


        public float Damage { get; private set; }


        public BulletModel (Vector3 Direction, float MaxDistance, float Speed, float theDamage, ITimer theTimer)
        {
            myDirection = Direction;
            myMaxDistance = MaxDistance;
            mySpeed = Speed;
            Damage = theDamage;

            myTimer = theTimer;
        }

        public Vector3 Move()
        {
            var aDelta = mySpeed * myTimer.DeltaTime;
            myCurDistance += aDelta;

            if (myCurDistance >= myMaxDistance)
            {
                OnBulletDestoy?.Invoke();
            }

            return myDirection * aDelta;
        }
    }
}
