using LearnGame.CompositionRoot;
using LearnGame.Enemy;
using LearnGame.Timer;

using UnityEngine;

using System.Collections.Generic;
using System.Linq;
using System;


namespace LearnGame {

    [DefaultExecutionOrder(-20)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager myInstance { get; private set; }

        public event Action Win;
        public event Action Lose;

        private CharacterCompositionRoot myPlayer;
        private List<CharacterCompositionRoot> myEnemies;

        public PlayerCharacterView Player { get; private set; }
        public List<EnemyCharacterView> Enemies { get; private set; }

        public TimerUI Timer { get; private set; }

        protected void Start()
        {
            if (myInstance == null)
            {
                myInstance = this;
            }
            else
            {
                Destroy (this);
                return;
            }
            ITimer aTimer = new UnityTimer();
            myPlayer = FindObjectOfType<PlayerCharacterView>().GetComponentInChildren<CharacterCompositionRoot>();            
            
            Enemies = FindObjectsOfType<EnemyCharacterView>().ToList();
            myEnemies = new List<CharacterCompositionRoot>(Enemies.Count);
            foreach (var anEnemy in Enemies)
            {
                var anEnemyCompositionRoot = anEnemy.GetComponentInChildren<CharacterCompositionRoot>();
                myEnemies.Add (anEnemyCompositionRoot);
            }

            Player = (PlayerCharacterView) myPlayer.Compose (aTimer);
            Enemies = new List<EnemyCharacterView>(myEnemies.Count);
            foreach (var anEnemyRoot in myEnemies)
            {
                Enemies.Add ((EnemyCharacterView) anEnemyRoot.Compose (aTimer));
            }

            Player.Dead += OnPlayerDead;

            foreach(var enemy in Enemies)
            {
                enemy.Dead += OnEnemyDead;
            }

            Timer = FindObjectOfType<TimerUI>();
            Timer.TimerEnd += PlayerLose;
        }

        protected void OnDestroy()
        {
            Player.Dead -= OnPlayerDead;
            foreach(var enemy in Enemies)
            {
                enemy.Dead -= OnEnemyDead;
            }
            Timer.TimerEnd -= PlayerLose;
        }

        private void OnPlayerDead (BaseCharacterView theSender)
        {
            Player.Dead -= OnPlayerDead;
            Lose?.Invoke();
            CharacterSpawner.IsPlayerSpawned = false;
            Time.timeScale = 0f;
        }

        private void OnEnemyDead (BaseCharacterView theSender)
        {
            var enemy = theSender as EnemyCharacterView;
            Enemies.Remove (enemy);
            enemy.Dead -= OnEnemyDead;

            if (Enemies.Count == 0)
            {
                Win?.Invoke();
                CharacterSpawner.IsPlayerSpawned = false;
                Time.timeScale = 0f;
            }
        }

        private void PlayerLose()
        {
            Timer.TimerEnd -= PlayerLose;
            Lose?.Invoke();
            Time.timeScale = 0f;
            CharacterSpawner.IsPlayerSpawned = false;
        }
    }
}