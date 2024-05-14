using LearnGame.Movement;
using LearnGame.FSM;

using UnityEngine;

namespace LearnGame.Enemy.States
{
    class RetreatState : BaseState
    {
        private readonly IEnemyTarget myTarget;
        private readonly IMovementDirectionSource myEnemyDirectionController;

        private Vector3 IEnemyTarget;
        public RetreatState (IEnemyTarget target, IMovementDirectionSource enemyDirectionController)
        {
            myTarget = target;
            myEnemyDirectionController = enemyDirectionController;
        }

        public override void Execute()
        {
            Vector3 targetPosition = myTarget.Closest.transform.position;
            myEnemyDirectionController.UpdateMovementDirection (targetPosition, false);

            myTarget.Retreat();
        }
    }
}
