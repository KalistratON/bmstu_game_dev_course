using UnityEngine;

using System;

namespace LearnGame.Bonus
{
    [Serializable]
    public class BonusDescription
    {
        [field: SerializeField]
        public float RemainingTimeSec { get; set; } = 5f;
    }
}
