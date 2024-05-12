using LearnGame.Property;

using UnityEngine;

namespace LearnGame.Enemy {

    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAIController))]
    public class EnemyCharacterView : BaseCharacterView
    {
        [SerializeField]
        private float myRetreatSpeed = 0.1f;

        public void Retreat()
        {
            Model.AddProperty (new Retreating (myRetreatSpeed));
        }
    }

}