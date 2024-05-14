using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.Timer;

using UnityEngine;

namespace LearnGame.CompositionRoot
{
    public class CharacterCompositionRoot : CompositionRoot<BaseCharacterView>
    {
        [SerializeField]
        private CharacterConfig myCharacterConfig;

        [SerializeField]
        private BaseCharacterView myCharacterView;

        public override BaseCharacterView Compose (ITimer theTimer)
        {
            IMovementController aMovementController = 
                new CharacterMovementController (myCharacterConfig, theTimer);
            IShootingTarget aShootingTarget = new ShootingTargetGo (myCharacterView.gameObject);

            var aShootingController = new ShootingController (theTimer, aShootingTarget);
            var aCharacter = new BaseCharacterModel (
                                aMovementController, aShootingController, myCharacterConfig);
            myCharacterView.Initialize (aCharacter);
            return myCharacterView;
        }
    }
}
