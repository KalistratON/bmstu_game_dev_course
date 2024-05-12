using LearnGame.Enemy;

using TMPro;
using UnityEngine;

using System.Linq;

namespace LearnGame
{
    public class EnemyCounterView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI myOutputText;

        private string myFormat;
        private int myEnemyCount;

        private void Start()
        {
            var anEnemies = FindObjectsOfType<EnemyCharacterView>().ToList();
            foreach (var enemy in anEnemies)
            {
                enemy.Dead += OnEnemyDead;
            }

            myFormat = myOutputText.text;
            myEnemyCount = anEnemies.Count();
            myOutputText.text = string.Format (myFormat, myEnemyCount);
        }

        void OnEnemyDead (BaseCharacterView theSender)
        {
            theSender.Dead -= OnEnemyDead;
            myOutputText.text = string.Format (myFormat, --myEnemyCount);
        }
    }
}