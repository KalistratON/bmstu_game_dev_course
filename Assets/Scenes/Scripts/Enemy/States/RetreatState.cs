using LearnGame.Property;
using LearnGame.FSM;

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
            myEnemyDirectionController.UpdateMovementDirection (targetPosition, false);

            myTarget.mySelf.Retreat();
        }
    }
}
