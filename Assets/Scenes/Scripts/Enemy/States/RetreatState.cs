using LearnGame.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LearnGame.Enemy.States
{
    class RetreatState : BaseState
    {
        private readonly EnemyTarget myTarget;
        private readonly EnemyDirectionController myEnemyDirectionController;

        private Vector3 myCurrentPoint;
        public RetreatState(EnemyTarget target, EnemyDirectionController enemyDirectionController)
        {
            myTarget = target;
            myEnemyDirectionController = enemyDirectionController;
        }

        public override void Execute()
        {
            Vector3 targetPosition = myTarget.Closest.transform.position;
            myEnemyDirectionController.UpdateMovementDirection(targetPosition, false);
            myEnemyDirectionController.IsRetreating = true;
        }
    }
}
