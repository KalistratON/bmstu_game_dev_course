using UnityEngine;

namespace LearnGame.Enemy.States
{
    [CreateAssetMenu(fileName = nameof(EnemyAIDescription), menuName = nameof(Enemy))]
    public class EnemyAIDescription : ScriptableObject
    {
        [field: SerializeField]
        public float RetreatSpeed { get; private set; }

        [field: SerializeField]
        public float ViewRadius { get; private set; }

        [field: SerializeField]
        public float CriticalHealthPercent { get; private set; }

        [field: SerializeField]
        public float RetreatChancePercent { get; private set; }
    }
}
