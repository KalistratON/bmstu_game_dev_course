using LearnGame.FSM;
using LearnGame.Movement;

using UnityEngine;

namespace LearnGame.Enemy.States
{
    class FindWayState : BaseState
    {
        private const float MaxDistanceDetweenRealAndCalculated = 3f;
        private readonly IEnemyTarget myTarget;
        private readonly INavigation myNavMesher;
        private readonly IMovementDirectionSource myEnemyDirectionController;

        private Vector3 myCurrentPoint;
        public FindWayState (IEnemyTarget target, INavigation navMesher, IMovementDirectionSource EnemyDirectionController)
        {
            myTarget = target;
            myNavMesher = navMesher;
            myEnemyDirectionController = EnemyDirectionController;
        }

        public override void Execute()
        {
            Vector3 targetPosition = myTarget.Closest.transform.position;

            if (!myNavMesher.IsPathCalculated || 
                myNavMesher.DistanceToTargetPointFrom(targetPosition) > MaxDistanceDetweenRealAndCalculated)
            {
                myNavMesher.CalculatePath(targetPosition);
            }

            var currentPoint = myNavMesher.GetCurrentPoint();
            if (myCurrentPoint != currentPoint)
            {
                myCurrentPoint = currentPoint;
                myEnemyDirectionController.UpdateMovementDirection (currentPoint);
            }
        }
    }
}
