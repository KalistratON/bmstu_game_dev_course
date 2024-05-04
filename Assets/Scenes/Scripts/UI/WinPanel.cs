using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnGame
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField]
        private GameManager myGameManager;

        void Start()
        {
            myGameManager.Win += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }
    }
}