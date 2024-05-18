using LearnGame.FSM;
using LearnGame.Movement;

using System;
using System.Collections.Generic;


namespace LearnGame.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5f;
        public EnemyStateMachine (IMovementDirectionSource enemyDirectionController, INavigation navMesher, IEnemyTarget target, 
                                 float criticalPercent, float retreatChancePercent)
        {
            var idleState = new IdleState();
            var findWayState = new FindWayState (target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState (target, enemyDirectionController);
            var retreatState = new RetreatState (target, enemyDirectionController);

            Func<bool> retreatFunc = () => {
                return target.CurrentHealth() / target.MaxHealth <= criticalPercent &&
                       UnityEngine.Random.Range(0, 1) <= retreatChancePercent;
            };

            SetInitialState(idleState);
            AddState(state: idleState, transitions: new List<Transition>
            {
                new Transition (findWayState,
                () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition (moveForwardState,
                () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                new Transition (retreatState, retreatFunc)
            });
            AddState(state: findWayState, transitions: new List<Transition>
            {
                new Transition (idleState,
                () => target.Closest == null),
                new Transition (moveForwardState,
                () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
            });
            AddState(state: moveForwardState, transitions: new List<Transition>
            {
                new Transition (idleState,
                () => target.Closest == null),
                new Transition (findWayState,
                () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition (retreatState, retreatFunc)
            });
            AddState(state: retreatState, transitions: new List<Transition>
            {
                new Transition (idleState,
                () => target.Closest == null)
            });
        }
    }
}
