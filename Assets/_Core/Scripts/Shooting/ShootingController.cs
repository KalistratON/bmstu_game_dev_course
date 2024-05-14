using LearnGame.Timer;

using UnityEngine;

namespace LearnGame.Shooting {

    public class ShootingController
    {
        private readonly ITimer myTimer;
        private IShootingTarget myShootingTarget;

        private BaseCharacterModel myTarget;
        private WeaponModel myWeapon;
        private float myNextShootTimeSec;

        public bool HasTarget => myTarget != null;
        public bool HasWeapon => myWeapon != null;
        public Vector3 GetTargetPosition => myTarget.Transform.Position;

        public ShootingController (ITimer theTimer, IShootingTarget theShootingTarget)
        {
            myTimer = theTimer;
            myShootingTarget = theShootingTarget;
        }

        public void TryShoot (Vector3 thePosition)
        {
            myNextShootTimeSec -= myTimer.DeltaTime;
            if (myNextShootTimeSec >= 0f)
            {
                return;
            }

            myTarget = myShootingTarget.GetTarget (thePosition, myWeapon.Description.ShootRadius);
            if (HasTarget)
            {
                myWeapon.Shoot (thePosition, GetTargetPosition);
            }
            myNextShootTimeSec = myWeapon.Description.ShootFrequencySec;
        }

        public void SetWeapon(WeaponModel theWeapon)
        {
            myWeapon = theWeapon;
        }
    }
}