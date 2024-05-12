using UnityEngine;

using System;

namespace LearnGame.Bonus
{
    [Serializable]
    public class AccelerationDescription : BonusDescription
    {
        [field: SerializeField]
        public float SpeedMultiplier { get; private set; } = 5f;

        [field: SerializeField]
        public float SpeedUpDuringSec { get; private set; } = 5f;
    }
}
