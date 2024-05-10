using UnityEngine;

using System;

namespace LearnGame.Shooting
{
    public class WeaponModel
    {
        public event Action <Vector3, WeaponDescription> Shot;
        public WeaponDescription Description { get; }

        public WeaponModel (WeaponDescription theDescription)
        {
            Description = theDescription;
        }

        public void Shoot (Vector3 theShotPosition, Vector3 theTargetPoint)
        {
            var aTargetDir = theTargetPoint - theShotPosition;
            aTargetDir.y = 0;
            aTargetDir.Normalize();

            Shot?.Invoke (aTargetDir, Description);
        }
    }
}