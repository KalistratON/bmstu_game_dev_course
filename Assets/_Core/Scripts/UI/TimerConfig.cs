using UnityEngine;

namespace LearnGame.UI
{
    [CreateAssetMenu(fileName = nameof(TimerUIConfig), menuName = nameof(UI))]
    public class TimerUIConfig : ScriptableObject
    {
        [field: SerializeField]
        public string Format { get; private set; }

        [field: SerializeField]
        public float GameDurationSeconds { get; private set; }
    }
}
