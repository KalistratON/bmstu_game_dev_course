using UnityEngine;
using System.Collections;

namespace LearnGame.Shooting
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField]
        public Bullet BulletPrefab { get; private set; }
        [field: SerializeField]
        public float ShootRadius { get; private set; }
        [field: SerializeField]
        public float ShootFrequencySec { get; private set; } = 1f;

        [SerializeField]
        private float myBulletMaxDistance = 10f;

        [SerializeField]
        private float myDamage = 1f;

        [SerializeField]
        private float myBulletSpeed = 10f;

        [SerializeField]
        private Transform myBulletSpawnPos;

        public void Shoot(Vector3 targetPoint)
        {
            var bullet = Instantiate(BulletPrefab, myBulletSpawnPos.position, Quaternion.identity);

            var target = targetPoint - myBulletSpawnPos.position;
            target.y = 0;
            target.Normalize();

            bullet.Initialize(target, myBulletMaxDistance, myBulletSpeed, myDamage);
        }
    }
}