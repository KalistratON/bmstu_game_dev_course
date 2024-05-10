using UnityEngine;

namespace LearnGame.Shooting
{
    [CreateAssetMenu(fileName = nameof(WeaponFactory), menuName = nameof(WeaponFactory))]
    public class WeaponFactory : ScriptableObject
    {
        [SerializeField]
        private WeaponView myWeaponPrefab;

        [SerializeField]
        WeaponDescription myWeaponDescription;

        public WeaponView Create (Transform theWeaponParent)
        {
            var aWeapon = Instantiate (myWeaponPrefab, theWeaponParent);

            var aModel = new WeaponModel (myWeaponDescription);
            aWeapon.Initialize (aModel);

            return aWeapon;
        }
    }
}
