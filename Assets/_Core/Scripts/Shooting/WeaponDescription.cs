using UnityEngine;

using System;

namespace LearnGame.Shooting
{
    [Serializable]
    public class WeaponDescription
    {
        [field: SerializeField]
        public float ShootRadius { get; private set; } = 5f;

        [field: SerializeField]
        public float ShootFrequencySec { get; private set; } = 1f;

        [field: SerializeField]
        public float Damage { get; private set; } = 1f;

        [field: SerializeField]
        public float BulletMaxDistance { get; private set; } = 10f;

        [field: SerializeField]
        public float BulletSpeed { get; private set; } = 10f;
    }
}
