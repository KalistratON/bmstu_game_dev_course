using LearnGame.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LearnGame.Enemy.States
{
    class MoveForwardState : BaseState
    {
        private readonly EnemyTarget myTarget;
        private readonly EnemyDirectionController myEnemyDirectionController;

        private Vector3 myCurrentPoint;

        public MoveForwardState (EnemyTarget target, EnemyDirectionController enemyDirectionController)
        {
            myTarget = target;
            myEnemyDirectionController = enemyDirectionController;
        }

        public override void Execute()
        {
            if (myTarget == null || myTarget.Closest == null)
            {
                return;
            }

            Vector3 targetPosition = myTarget.Closest.transform.position;
            if (myCurrentPoint != targetPosition)
            {
                myCurrentPoint = targetPosition;
                myEnemyDirectionController.UpdateMovementDirection(targetPosition);
            }
        }
    }
}
