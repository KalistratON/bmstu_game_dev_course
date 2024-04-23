using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace LearnGame.Enemy
{
    public class NavMesher
    {
        private const float DistanceEps = 1.5f;
        public bool IsPathCalculated { get; private set; } = false;

        private readonly NavMeshQueryFilter myFileter;
        private readonly Transform myAgentTransform;

        private NavMeshPath myNavMeshPath;
        private NavMeshHit myTargetHit;
        private int myCurrentPathPointIndex;

        public NavMesher (Transform agentTransform)
        {
            myFileter = new NavMeshQueryFilter { areaMask = NavMesh.AllAreas };
            myAgentTransform = agentTransform;
            myNavMeshPath = new NavMeshPath();
        }

        public void CalculatePath(Vector3 targetPosition)
        {
            NavMesh.SamplePosition(myAgentTransform.position, out var agentHit, 10f, myFileter);
            NavMesh.SamplePosition(targetPosition, out var myTargetHit, 10f, myFileter);

            IsPathCalculated = NavMesh.CalculatePath(agentHit.position, myTargetHit.position, myFileter, myNavMeshPath);
            myCurrentPathPointIndex = 0;
        }

        public Vector3 GetCurrentPoint()
        {
            var currentPoint = myNavMeshPath.corners[myCurrentPathPointIndex];
            var distatance = (myAgentTransform.position - currentPoint).magnitude;

            if (distatance < DistanceEps)
            {
                myCurrentPathPointIndex++;

                if (myCurrentPathPointIndex >= myNavMeshPath.corners.Length)
                {
                    IsPathCalculated = false;
                }
                else
                {
                    currentPoint = myNavMeshPath.corners[myCurrentPathPointIndex];
                }
            }
            return currentPoint;
        }

        public float DistanceToTargetPointFrom(Vector3 position) => (myTargetHit.position - position).magnitude;

    }
}
