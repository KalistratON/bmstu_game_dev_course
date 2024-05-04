using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LearnGame
{
    public class Home : MonoBehaviour
    {
        private Button myHomeButton;

        private void Start()
        {
            myHomeButton = GetComponent<Button>();
            myHomeButton.onClick.RemoveAllListeners();
            myHomeButton.onClick.AddListener(OpenMainLevel);
        }

        private void OpenMainLevel()
        {
            Time.timeScale = 1f;
            CharacterSpawner.IsPlayerSpawned = false;
            SceneManager.LoadScene(0);
        }
    }
}