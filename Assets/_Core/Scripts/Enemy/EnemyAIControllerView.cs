using LearnGame.Enemy.States;

using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyAIControllerView : MonoBehaviour
    {        
        private IEnemyTarget myTarget;
        private EnemyStateMachine myStateMachine;

        public void Initialize (IEnemyTarget theEnemyTarget, EnemyStateMachine theEnemyStateMachine)
        {
            myTarget = theEnemyTarget;
            myStateMachine = theEnemyStateMachine;
        }

        protected void Update()
        {
            if (myTarget == null)
            {
                return;
            }

            myTarget.FindClosest();
            myStateMachine.Update();
        }
    }
}