using UnityEngine;

namespace LearnGame.Enemy
{
    public interface IEnemyTarget
    {
        GameObject Closest { get; }
        float MaxHealth { get; }
        float CurrentHealth { get; }

        void Retreat();
        void FindClosest();
        float DistanceToClosestFromAgent();
    }
}
