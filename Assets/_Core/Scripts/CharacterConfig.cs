using UnityEngine;

namespace LearnGame
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = nameof(CharacterConfig))]
    class CharacterConfig : ScriptableObject, ICharacterConfig
    {
        [field: SerializeField]
        public float Health { get; private set; }

        [field: SerializeField]
        [Tooltip("Linear speed")]
        public float Speed { get; private set; }

        [field: SerializeField]
        [Tooltip("Rotation speed")]
        public float RollSpeed { get; private set; }
    }
}
