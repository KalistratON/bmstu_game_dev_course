using LearnGame.CompositionRoot;
using LearnGame.Enemy;
using LearnGame.Timer;
using LearnGame.UI;

using UnityEngine;

using System.Collections.Generic;
using System;


namespace LearnGame {

    [DefaultExecutionOrder(-20)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager myInstance { get; private set; }


        public event Action <ITimer> Win;
        public event Action <ITimer> Lose;


        public PlayerCharacterView Player { get; private set; }
        public List<EnemyCharacterView> Enemies { get; private set; }


        public TimerUIView Timer { get; private set; }


        public bool IsPlayerExist => Player != null;
        private ITimer myTimer;


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
            myTimer = new UnityTimer();
            Enemies = CompositionRootFactory.InitEnemies (myTimer);

            Player.Dead += OnPlayerDead;
            foreach(var enemy in Enemies)
            {
                enemy.Dead += OnEnemyDead;
            }

            Timer = FindObjectOfType<TimerUIView>();
            Timer.Initialize (myTimer);
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
            Lose?.Invoke (myTimer);
        }

        private void OnEnemyDead (BaseCharacterView theSender)
        {
            var enemy = theSender as EnemyCharacterView;
            Enemies.Remove (enemy);
            enemy.Dead -= OnEnemyDead;

            if (Enemies.Count == 0)
            {
                Win?.Invoke (myTimer);

                Timer.myTimerUIModel.TimerEnd -= PlayerLose;
            }
        }

        private void PlayerLose()
        {
            Timer.myTimerUIModel.TimerEnd -= PlayerLose;
            Lose?.Invoke (myTimer);
        }

        public void InitPlayer()
        {
            myTimer = new UnityTimer();
            Player = CompositionRootFactory.InitPlayer (myTimer);
        }
    }
}