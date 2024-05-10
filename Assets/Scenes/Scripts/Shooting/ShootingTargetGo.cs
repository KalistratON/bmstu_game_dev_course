using UnityEngine;

namespace LearnGame.Shooting
{
    public class ShootingTargetGo : IShootingTarget
    {
        private readonly Collider[] myColliders = new Collider [2];
        private readonly GameObject myShooter;

        public ShootingTargetGo (GameObject theShooter)
        {
            myShooter = theShooter;
        }

        public BaseCharacterModel GetTarget (Vector3 thePosition, float theRadius)
        {
            BaseCharacterModel target = null;
            var aMask = LayerUtils.CharacterMask;

            var aSize = Physics.OverlapSphereNonAlloc (thePosition, theRadius, myColliders, aMask);
            if (aSize > 1)
            {
                for (int i = 0; i < aSize; ++i)
                {
                    if (myColliders[i].gameObject != myShooter)
                    {
                        target = myColliders[i].gameObject.GetComponent<BaseCharacterView>().Model;
                        break;
                    }
                }
            }
            return target;
        }
    }
}
