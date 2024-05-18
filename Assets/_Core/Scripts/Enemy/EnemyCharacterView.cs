using LearnGame.Property;

using UnityEngine;

namespace LearnGame.Enemy {

    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAIControllerView))]
    public class EnemyCharacterView : BaseCharacterView
    {
        public float RetreatSpeed { get; set; }
        private bool IsRetreating { get; set; } = false;

        public void Retreat()
        {
            if (Model != null && !IsRetreating) { 
                Model.AddProperty (new Retreating (RetreatSpeed));
                IsRetreating = true;
            }
        }
    }

}