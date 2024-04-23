using LearnGame.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LearnGame.Enemy.States
{
    class FindWayState : BaseState
    {
        private const float MaxDistanceDetweenRealAndCalculated = 3f;
        private readonly EnemyTarget myTarget;
        private readonly NavMesher myNavMesher;
        private readonly EnemyDirectionController myEnemyDirectionController;

        private Vector3 myCurrentPoint;
        public FindWayState (EnemyTarget target, NavMesher navMesher, EnemyDirectionController EnemyDirectionController)
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
                myEnemyDirectionController.UpdateMovementDirection(currentPoint);
            }
        }
    }
}
