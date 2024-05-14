using LearnGame.FSM;
using LearnGame.Movement;

using UnityEngine;

namespace LearnGame.Enemy.States
{
    class MoveForwardState : BaseState
    {
        private readonly IEnemyTarget myTarget;
        private readonly IMovementDirectionSource myEnemyDirectionController;

        private Vector3 myCurrentPoint;

        public MoveForwardState (IEnemyTarget target, IMovementDirectionSource enemyDirectionController)
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
