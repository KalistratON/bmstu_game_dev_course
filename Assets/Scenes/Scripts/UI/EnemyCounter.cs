using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Linq;
using LearnGame.Enemy;

namespace LearnGame
{
    public class EnemyCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI myOutputText;
        private string myFormat;
        private int myEnemyCount;

        public List<EnemyCharacterView> Enemies { get; private set; }

        private void Start()
        {
            Enemies = FindObjectsOfType<EnemyCharacterView>().ToList();
            myEnemyCount = Enemies.Count;
            foreach (var enemy in Enemies)
            {
                enemy.Dead += OnEnemyDead;
            }
            myFormat = myOutputText.text;
            myOutputText.text = string.Format(myFormat, myEnemyCount);
        }

        void OnEnemyDead(BaseCharacterView theSender)
        {
            var enemy = theSender as EnemyCharacterView;
            Enemies.Remove(enemy);
            enemy.Dead -= OnEnemyDead;

            myOutputText.text = string.Format(myFormat, --myEnemyCount);
        }
    }
}