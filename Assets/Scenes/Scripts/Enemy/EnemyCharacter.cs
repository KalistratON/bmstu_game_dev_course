using LearnGame.Movement;
using LearnGame.Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnGame.Enemy {

    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAIController))]
    public class EnemyCharacter : BaseCharacter
    {
        [SerializeField]
        private float myRetreatSpeed = 0.1f;

        protected override void Update()
        {
            if (myCharacterMovementController)
            {
                myCharacterMovementController.SpeedAddition = (myIMovementDirSource as EnemyDirectionController).IsRetreating
                                                          ? myRetreatSpeed : 0.0f;
            }
            base.Update();
        }
    }

}