using UnityEngine;

namespace LearnGame.Bonus
{
    [CreateAssetMenu(fileName = nameof(BonusFactory), menuName = nameof(BonusFactory))]
    public class BonusFactory : ScriptableObject
    {
        [SerializeField]
        private Bonus myBonusPrefab;

        public Bonus Create (Transform theParent)
        {
            var aBonus = Instantiate (myBonusPrefab, theParent);
            return aBonus;
        }
    }
}
