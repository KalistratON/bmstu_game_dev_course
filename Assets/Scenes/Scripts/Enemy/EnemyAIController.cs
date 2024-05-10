using UnityEngine;
using System.Collections;
using LearnGame.Enemy.States;

namespace LearnGame.Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField]
        private float myViewRadius = 20f;
        
        [SerializeField]
        private float myCriticalHealthPercent = 0.3f;
        
        [SerializeField]
        private float myRetreatChancePercent = 0.3f;
        
        private EnemyTarget myTarget;
        private EnemyStateMachine myStateMachine;

        protected void Start()
        {
            var aPlayer = GameManager.myInstance.Player;
            var anEnemyDirectionController = GetComponent<EnemyDirectionController>();

            var aNavMesher = new NavMesher (transform);
            myTarget = new EnemyTarget (
                            transform, aPlayer, myViewRadius, GetComponent<EnemyCharacterView>().Model.Health);

            myStateMachine = new EnemyStateMachine(
                                anEnemyDirectionController, aNavMesher, myTarget, myCriticalHealthPercent, myRetreatChancePercent);
        }

        protected void Update()
        {
            var anEnemy = GetComponent<EnemyCharacterView>();
            myTarget.FindClosest();
            myStateMachine.Update();
        }
    }
}