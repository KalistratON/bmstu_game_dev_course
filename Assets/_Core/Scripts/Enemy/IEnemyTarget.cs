using UnityEngine;

namespace LearnGame.Enemy
{
    public interface IEnemyTarget
    {
        GameObject Closest { get; }
        float MaxHealth { get; }

        void Retreat();
        void FindClosest();
        float CurrentHealth();
        float DistanceToClosestFromAgent();
    }
}
