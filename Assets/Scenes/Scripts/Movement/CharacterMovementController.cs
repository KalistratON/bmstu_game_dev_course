using UnityEngine;
using LearnGame.Timer;

namespace LearnGame.Movement {

    public class CharacterMovementController : IMovementController
    {
        private static readonly float mySqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        private readonly ITimer myTimer;

        private readonly float mySpeed;
        private readonly float myRollSpeed;

        public CharacterMovementController (ICharacterConfig theConfig, ITimer theTimer)
        {
            mySpeed = theConfig.Speed;
            myRollSpeed = theConfig.RollSpeed;

            myTimer = theTimer;
        }

        public Vector3 Translate(Vector3 theMovementDirection)
        {
            return theMovementDirection * mySpeed * myTimer.DeltaTime;
        }

        public Quaternion Rotate (Quaternion theCurrentRotation, Vector3 theLookDirection)
        {
            if (myRollSpeed <= 0f || theLookDirection == Vector3.zero)
            {
                return theCurrentRotation;
            }

            var aCurLookDir = theCurrentRotation * Vector3.forward;
            float aSqrMagnitude = (aCurLookDir - theLookDirection).sqrMagnitude;
            if (aSqrMagnitude > mySqrEpsilon)
            {
                var aRotation = Quaternion.Slerp (theCurrentRotation,
                                                  Quaternion.LookRotation (theLookDirection, Vector3.up),
                                                  myRollSpeed * myTimer.DeltaTime);
                return aRotation;
            }
            return theCurrentRotation;
        }
    }

}
