using Assets.Scripts.SceneData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class GameFinishMenu : MonoBehaviour
    {
        public TextMeshProUGUI mLevelNumber;
        public TextMeshProUGUI mCoinsCollected;

        private void Start()
        {
            PauseGame();
            CalculateScore();


        }

        private void CalculateScore()
        {
            int coinsSum = 0;

            SubScene[] subScenes = SubSceneMultiton.Instance.GetAll();
            foreach (SubScene subScene in subScenes)
            {
                HUD score = subScene.GetScore();
                coinsSum += score.GetCoins();
            }

            mCoinsCollected.text = coinsSum.ToString();
        }

        public void OnHomePressed()
        {
            Debug.Log("Return to main menu");

            SceneManager.LoadScene("Scene Main Menu");
            ResumeGame();
        }

        public void OnRestartPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            ResumeGame();
        }

        public void OnLeaderboardPressed()
        {
            Debug.Log("show leaderboard");
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}
