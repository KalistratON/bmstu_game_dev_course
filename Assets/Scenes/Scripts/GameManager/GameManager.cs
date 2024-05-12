using LearnGame.CompositionRoot;
using LearnGame.Enemy;
using LearnGame.Timer;

using UnityEngine;

using System.Collections.Generic;
using System;


namespace LearnGame {

    [DefaultExecutionOrder(-20)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager myInstance { get; private set; }


        public event Action Win;
        public event Action Lose;


        public PlayerCharacterView Player { get; private set; }
        public List<EnemyCharacterView> Enemies { get; private set; }


        public TimerUIView Timer { get; private set; }


        public bool IsPlayerExist => Player != null;


        private void Awake()
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

            CharacterSpawner.OnPlayerSpawn += InitPlayer;
        }

        protected void Start()
        {
            ITimer aTimer = new UnityTimer();
            Enemies = CompositionRootFactory.InitEnemies (aTimer);

            Player.Dead += OnPlayerDead;
            foreach(var enemy in Enemies)
            {
                enemy.Dead += OnEnemyDead;
            }

            Timer = FindObjectOfType<TimerUIView>();
            Timer.myTimerUIModel.TimerEnd += PlayerLose;
        }

        protected void OnDestroy()
        {
            CharacterSpawner.OnPlayerSpawn -= InitPlayer;

            if (Player == null)
            {
                return;
            }

            Player.Dead -= OnPlayerDead;
            foreach(var enemy in Enemies)
            {
                enemy.Dead -= OnEnemyDead;
            }
            Timer.myTimerUIModel.TimerEnd -= PlayerLose;
        }

        private void OnPlayerDead (BaseCharacterView theSender)
        {
            Player.Dead -= OnPlayerDead;
            Lose?.Invoke();
        }

        private void OnEnemyDead (BaseCharacterView theSender)
        {
            var enemy = theSender as EnemyCharacterView;
            Enemies.Remove (enemy);
            enemy.Dead -= OnEnemyDead;

            if (Enemies.Count == 0)
            {
                Win?.Invoke();

                Timer.myTimerUIModel.TimerEnd -= PlayerLose;
            }
        }

        private void PlayerLose()
        {
            Timer.myTimerUIModel.TimerEnd -= PlayerLose;
            Lose?.Invoke();
        }

        public void InitPlayer()
        {
            ITimer aTimer = new UnityTimer();
            Player = CompositionRootFactory.InitPlayer (aTimer);
        }
    }
}