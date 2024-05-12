using LearnGame.Timer;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections.Generic;

namespace LearnGame.UI
{
    public class OpenLevelView : MonoBehaviour
    {
        private Button myOpenLevelButton;

        [SerializeField]
        private List<int> myLevelList = new List<int>();

        private void Start()
        {
            myOpenLevelButton = GetComponent<Button>();
            myOpenLevelButton.onClick.RemoveAllListeners();
            myOpenLevelButton.onClick.AddListener (OpenRandomLevel);
        }

        private void OpenRandomLevel()
        {
            (new UnityTimer()).SetTimeScale (1.0f);
            int level = myLevelList [Random.Range (0, myLevelList.Count)];
            SceneManager.LoadScene(level);
        }
    }
}