using LearnGame.Movement;
using UnityEngine;
using LearnGame.Camera;

namespace LearnGame {
#if UNITY_ANDROID
    [RequireComponent(typeof(JoystickPlayerMovementDirectionController))]
#elif UNITY_STANDALONE || UNITY_EDITOR
    [RequireComponent(typeof(PlayerMovementDirectionController))]
#endif
    public class PlayerCharacterView : BaseCharacterView
    {
        protected override void Start ()
        {
            FindAnyObjectByType<CameraController>().SetCameraSpy (this);

            base.Start();

#if UNITY_ANDROID
            myMovementDirSource = GetComponent<JoystickPlayerMovementDirectionController>();
#elif UNITY_STANDALONE || UNITY_EDITOR
            myMovementDirSource = GetComponent<PlayerMovementDirectionController>();
#endif

#if UNITY_ANDROID
            if (myMovementDirSource as JoystickPlayerMovementDirectionController != null)
#elif UNITY_STANDALONE || UNITY_EDITOR
            if (myMovementDirSource as PlayerMovementDirectionController != null)
#endif
            {
                (myMovementDirSource as PlayerMovementDirectionController).OnRunning += Model.SetSpeedScale;
            }
        }
    }

}