using LearnGame.Movement;
using UnityEngine;
using LearnGame.Camera;

namespace LearnGame {

    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacterView : BaseCharacterView
    {
        private bool WereEventsSetted { get; set; } = false;


        protected void Awake()
        {
            // base.Awake();
            UnityEngine.Camera.main.GetComponent<CameraController>().Player = this;
        }

        protected new void LateUpdate ()
        {
            // error when set it in Start or Initialize
            if (!WereEventsSetted)
            {
                WereEventsSetted = true;
                myMovementDirSource.OnRunning += Model.SetSpeedScale;
            }

            base.LateUpdate();
        }
    }

}