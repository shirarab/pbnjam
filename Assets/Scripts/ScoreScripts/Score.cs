using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreScripts
{
    public class Score : MonoBehaviour
    {
        #region Constants
        private const string c_scoreFormat = "Score: {0}";
        #endregion
        
        #region Score fields
        [SerializeField] 
        private Text peanutButterText;
        [SerializeField]
        private Text jamText;
        private Dictionary<PlayerType, int> playerScores;
        #endregion
        
        private void Start()
        {
            // initialize the dictionary with the player types and 0 scores
            playerScores = new Dictionary<PlayerType, int>
            {
                {PlayerType.Jelly, 0},
                {PlayerType.PeanutButter, 0}
            };
        }

        #region Points

        public void AddPoints(int points, PlayerType playerType)
        {
            playerScores[playerType] += points;
            UpdateScoreText();
        }

        public void ResetPoints()
        {
            // reset the scores to 0
            foreach (var playerType in playerScores.Keys)
            {
                playerScores[playerType] = 0;
            }
            UpdateScoreText();
        }

        #endregion

        private void UpdateScoreText()
        {
            if (peanutButterText == null || jamText == null)
            {
                Debug.LogError("Score text is null");
                return;
            }
            peanutButterText.text = string.Format(c_scoreFormat, playerScores[PlayerType.PeanutButter]);
            jamText.text = string.Format(c_scoreFormat, playerScores[PlayerType.Jelly]);
        }
    }
}
