using UnityEngine;

namespace LearnGame.Enemy
{
    public interface INavigation
    {
        bool IsPathCalculated { get; }

        void CalculatePath (Vector3 targetPosition);
        Vector3 GetCurrentPoint();
        float DistanceToTargetPointFrom (Vector3 position);
    }
}
