using LearnGame.Enemy.States;

using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyAIController : MonoBehaviour
    {        
        [field: SerializeField]
        public EnemyAIDescription Description { get; set; }
        
        private EnemyTarget myTarget;
        private EnemyStateMachine myStateMachine;

        protected void Start()
        {
            var aPlayer = GameManager.myInstance.Player;
            var anEnemyDirectionController = GetComponent<EnemyDirectionController>();

            var aNavMesher = new NavMesher (transform);
            myTarget = new EnemyTarget (
                            transform, aPlayer, Description.ViewRadius, GetComponent<EnemyCharacterView>());

            myStateMachine = new EnemyStateMachine(
                                anEnemyDirectionController, 
                                aNavMesher, 
                                myTarget, 
                                Description.CriticalHealthPercent, 
                                Description.RetreatChancePercent);
        }

        protected void Update()
        {
            var anEnemy = GetComponent<EnemyCharacterView>();

            if (anEnemy == null || anEnemy.Model == null)
            {
                return;
            }

            myTarget.CurrentHealth = anEnemy.Model.Health;
            myTarget.IsWeaponTaken = anEnemy.Model.HasWeapon;
            myTarget.FindClosest();
            myStateMachine.Update();
        }
    }
}