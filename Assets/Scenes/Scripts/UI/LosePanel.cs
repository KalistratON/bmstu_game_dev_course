using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnGame
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField]
        private GameManager myGameManager;

        void Start()
        {
            myGameManager.Lose += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }
    }
}