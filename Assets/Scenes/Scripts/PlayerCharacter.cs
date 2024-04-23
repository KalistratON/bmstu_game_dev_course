using LearnGame.Movement;
using UnityEngine;
using LearnGame.Camera;

namespace LearnGame {

    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        protected new void Awake()
        {
            base.Awake();
            UnityEngine.Camera.main.GetComponent<CameraController>().Player = this;
        }
    }

}