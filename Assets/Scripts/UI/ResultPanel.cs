using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _resultText = null;

        private void Awake()
        {
            GameManager.gameEnded += GameResult;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            GameManager.gameEnded -= GameResult;
        }

        private void GameResult(GameManager.EndGame result)
        {
            switch (result) {
                case GameManager.EndGame.Defeat:
                    _resultText.text = "YOU LOSE :(";
                    break;
                case GameManager.EndGame.Victory:
                    _resultText.text = "YOU WIN!";
                    break;
            }
            
            gameObject.SetActive(true);
        }
    }
}
