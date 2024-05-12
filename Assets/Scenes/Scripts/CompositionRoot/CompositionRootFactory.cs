using LearnGame.Timer;
using LearnGame.Enemy;

using UnityEngine;

using System.Collections.Generic;
using System.Linq;
using System;


namespace LearnGame.CompositionRoot
{ 
    public class CompositionRootFactory : MonoBehaviour
    {
        public static PlayerCharacterView InitPlayer (ITimer theTimer)
        {
            var aPlayer = FindObjectOfType<PlayerCharacterView>().GetComponentInChildren<CharacterCompositionRoot>();            
            return (PlayerCharacterView) aPlayer.Compose (theTimer);
        }

        public static List<EnemyCharacterView> InitEnemies (ITimer theTimer)
        {
            var anEnemies = FindObjectsOfType<EnemyCharacterView>().ToList();
            var anEnemiesCR = new List<CharacterCompositionRoot> (anEnemies.Count);
            foreach (var anEnemy in anEnemies)
            {
                var anEnemyCompositionRoot = anEnemy.GetComponentInChildren<CharacterCompositionRoot>();
                anEnemiesCR.Add (anEnemyCompositionRoot);
            }

            anEnemies = new List<EnemyCharacterView> (anEnemiesCR.Count);
            foreach (var anEnemyRoot in anEnemiesCR)
            {
                anEnemies.Add ((EnemyCharacterView) anEnemyRoot.Compose (theTimer));
            }

            return anEnemies;
        }
    }
}