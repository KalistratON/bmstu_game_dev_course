using LearnGame.Movement;
using UnityEngine;
using LearnGame.Camera;

namespace LearnGame {

    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacterView : BaseCharacterView
    {
        protected new void Start ()
        {
            FindAnyObjectByType<CameraController>().SetCameraSpy (this);

            base.Start();

            if (myMovementDirSource as PlayerMovementDirectionController != null)
            {
                (myMovementDirSource as PlayerMovementDirectionController).OnRunning += Model.SetSpeedScale;
            }
        }
    }

}