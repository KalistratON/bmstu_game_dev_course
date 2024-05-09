using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LearnGame
{
    public class OpenLevel : MonoBehaviour
    {
        private Button myOpenLevelButton;

        [SerializeField]
        private List<int> myLevelList = new List<int>();

        private void Start()
        {
            myOpenLevelButton = GetComponent<Button>();
            myOpenLevelButton.onClick.RemoveAllListeners();
            myOpenLevelButton.onClick.AddListener(OpenRandomLevel);
        }

        private void OpenRandomLevel()
        {
            Time.timeScale = 1f;
            int level = myLevelList[Random.Range(0, myLevelList.Count)];
            SceneManager.LoadScene(level);
        }
    }
}