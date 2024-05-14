using LearnGame.Timer;

using UnityEngine;

using System;

namespace LearnGame.Bonus
{
    public class Acceleration : Bonus
    {
        public override event Action<Bonus> OnBonusDestory;

        [SerializeField]
        public AccelerationDescription myAccelerationDescription;

        public override void SetBonus (BaseCharacterView theCharacter)
        {}

        private void Awake()
        {
            myTimer = new UnityTimer();
        }

        private void Update()
        {
            myAccelerationDescription.RemainingTimeSec -= myTimer.DeltaTime;

            if (myAccelerationDescription.RemainingTimeSec <= 0.0f)
            {
                OnBonusDestory?.Invoke (this);
                Destroy (this);
            }
        }

    }
}