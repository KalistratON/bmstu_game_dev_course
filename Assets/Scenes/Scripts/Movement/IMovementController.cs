using UnityEngine;

namespace LearnGame.Movement
{
    public interface IMovementController
    {
        Vector3 Translate (Vector3 theMovementDirection);
        Quaternion Rotate (Quaternion theCurrentRotation, Vector3 theLookDirection);
    }
}
