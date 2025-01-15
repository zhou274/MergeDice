using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace Ilumisoft.MergeDice
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        Text scoreText = null;

        [SerializeField]
        TextMeshProUGUI highscoreText = null;

        [SerializeField]
        TextMeshProUGUI messageText = null;

        void Start()
        {
            if (HasNewHighscore())
            {
                UpdateHighscore();
                DisplayHighscore(false);
                DisplayNewHighscoreMessage();
            }
            else
            {
                DisplayHighscore(true);
                DisplayNormalMessage();
            }

            DisplayScore();
        }

        bool HasNewHighscore()
        {
            return Score.Value > Highscore.Value;
        }

        void UpdateHighscore()
        {
            Highscore.Value = Score.Value;
        }

        void DisplayNewHighscoreMessage()
        {
            messageText.text = "新纪录！";
        }

        void DisplayNormalMessage()
        {
            messageText.text = "再试一次？";
        }

        void DisplayHighscore(bool show)
        {
            if (show)
            {
                highscoreText.text = $"最高分\n{Highscore.Value}";
            }
            else
            {
                highscoreText.text = string.Empty;
            }
        }

        void DisplayScore()
        {
            scoreText.text = Score.Value.ToString();
            PlayerPrefs.SetInt("Score", Score.Value);
        }
    }
}
