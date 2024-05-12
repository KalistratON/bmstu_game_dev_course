using LearnGame.Timer;

using UnityEngine;

using System;

namespace LearnGame.Bonus
{
    public abstract class Bonus : MonoBehaviour
    {
        public abstract event Action<Bonus> OnBonusDestory;
        protected ITimer myTimer;
        public abstract void SetBonus (BaseCharacterView theCharacter);
    }
}
