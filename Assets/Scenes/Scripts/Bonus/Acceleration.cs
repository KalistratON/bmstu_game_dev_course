using UnityEngine;
using System.Collections;

namespace LearnGame.Bonus
{
    public class Acceleration : MonoBehaviour
    {
        [field: SerializeField]
        public float Scale { get; private set; } = 2f;
        [field: SerializeField]
        public float Seconds { get; private set; } = 10f;
    }
}