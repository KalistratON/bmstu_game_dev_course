using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using LearnGame.Enemy;
using UnityEngine.UI;

namespace LearnGame {

    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Lose;

        public PlayerCharacter Player { get; private set; }
        public List<EnemyCharacter> Enemies { get; private set; }
        public List<EnemyCharacter> EnemyPointers { get; private set; }

        public TimerUI Timer { get; private set; }

        public void Start()
        {
            Player = FindObjectOfType<PlayerCharacter>();
            Enemies = FindObjectsOfType<EnemyCharacter>().ToList();
            Timer = FindObjectOfType<TimerUI>();

            Player.Dead += OnPlayerDead;

            foreach(var enemy in Enemies)
            {
                enemy.Dead += OnEnemyDead;
            }
            Timer.TimerEnd += PlayerLose;
        }

        private void OnPlayerDead(BaseCharacter sender)
        {
            Player.Dead -= OnPlayerDead;
            Lose?.Invoke();
            CharacterSpawner.IsPlayerSpawned = false;
            Time.timeScale = 0f;
        }

        private void OnEnemyDead(BaseCharacter sender)
        {
            var enemy = sender as EnemyCharacter;
            Enemies.Remove(enemy);
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