using UnityEngine;
using System.Collections;

namespace LearnGame.Shooting {

    public class ShootingController : MonoBehaviour
    {
        public bool HasTarget => myTarget != null;
        public Vector3 TargetPosition => myTarget.transform.position;

        private Weapon myWeapon;

        private Collider[] myColliders = new Collider[2];
        private float myNextShootTimeSec;
        private GameObject myTarget;

        [SerializeField]
        private string myEnemyLayerName;

        protected void Update()
        {

            myTarget = GetTarget();

            myNextShootTimeSec -= Time.deltaTime;
            if (myNextShootTimeSec < 0)
            {
                if (HasTarget)
                {
                    myWeapon.Shoot(TargetPosition);
                }
                myNextShootTimeSec = myWeapon.ShootFrequencySec;
            }
        }

        public void SetWeapon(Weapon weaponPrefab, Transform hand)
        {
            if (myWeapon != null)
            {
                Destroy(myWeapon.gameObject);
            }
            myWeapon = Instantiate(weaponPrefab, hand);
            myWeapon.transform.localPosition = Vector3.zero;
            myWeapon.transform.localRotation = Quaternion.identity;
        }

        private GameObject GetTarget()
        {
            GameObject target = null;

            var position = myWeapon.transform.position;
            var radius = myWeapon.ShootRadius;

            var mask = LayerMask.GetMask(myEnemyLayerName); ;

            var size = Physics.OverlapSphereNonAlloc(position, radius, myColliders, mask);
            if (size > 0)
            {
                for (int i = 0; i < size; ++i)
                {
                    if (myColliders[i].gameObject != gameObject)
                    {
                        target = myColliders[i].gameObject;
                        break;
                    }
                }
            }
            return target;
        }
    }
}