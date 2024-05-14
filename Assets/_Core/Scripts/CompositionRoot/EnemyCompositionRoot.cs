using LearnGame.Enemy;
using LearnGame.Enemy.States;
using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.Timer;

using UnityEngine;

namespace LearnGame.CompositionRoot
{
    public class EnemyCompositionRoot : CompositionRoot<EnemyCharacterView>
    {
        [SerializeField]
        private CharacterConfig myCharacterConfig;

        [SerializeField]
        private EnemyAIDescription myAIConfig;

        [SerializeField]
        private EnemyCharacterView myCharacterView;

        public override EnemyCharacterView Compose (ITimer theTimer)
        {
            IMovementController aMovementController = 
                new CharacterMovementController (myCharacterConfig, theTimer);
            IShootingTarget aShootingTarget = new ShootingTargetGo (myCharacterView.gameObject);

            var aShootingController = new ShootingController (theTimer, aShootingTarget);
            var aCharacter = new BaseCharacterModel (
                                aMovementController, aShootingController, myCharacterConfig);
            myCharacterView.Initialize (aCharacter);

            myCharacterView.RetreatSpeed = myAIConfig.RetreatSpeed;
            IMovementDirectionSource anEnemyDirectionController = GetComponent<EnemyDirectionController>();
            IEnemyTarget aTarget = new EnemyTarget (transform, GameManager.myInstance.Player, myAIConfig.ViewRadius, myCharacterView);
            INavigation aNavigation = new NavMesher (transform);

            var aStateMachine = new EnemyStateMachine(
                                                      anEnemyDirectionController, 
                                                      aNavigation, 
                                                      aTarget, 
                                                      myAIConfig.CriticalHealthPercent, 
                                                      myAIConfig.RetreatChancePercent);

            GetComponent<EnemyAIControllerView>().Initialize (aTarget, aStateMachine);

            return myCharacterView;
        }
    }
}
