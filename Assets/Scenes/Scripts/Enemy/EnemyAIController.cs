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
        // Use this for initialization
        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();

            var navMesher = new NavMesher(transform);
            myTarget = new EnemyTarget(transform, player, myViewRadius, GetComponent<EnemyCharacter>().myHealth);

            myStateMachine = new EnemyStateMachine(enemyDirectionController, navMesher, myTarget, myCriticalHealthPercent, myRetreatChancePercent);
        }

        // Update is called once per frame
        protected void Update()
        {
            var anEnemy = GetComponent<EnemyCharacter>();
            myTarget.IsWeaponTaken = anEnemy.IsWeaponTaken;
            myTarget.CurrentHealth = anEnemy.myHealth;
            myTarget.FindClosest();
            myStateMachine.Update();
        }
    }
}