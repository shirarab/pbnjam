using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ScoreScripts
{
    public class Score : MonoBehaviour
    {
        #region Constants
        private const string c_scoreFormat = "{0}";
        #endregion
        
        #region Score fields
        [SerializeField] 
        private Text peanutButterText;
        [SerializeField]
        private Text jamText;
        [SerializeField]
        private int maxScore = 5;
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

        public void RemovePoints(int points, PlayerType playerType)
        {
            playerScores[playerType] = Mathf.Max(playerScores[playerType]-points, 0) ;
            UpdateScoreText();
        }

        public void ResetScore()
        {
            // reset the scores to 0
            playerScores[PlayerType.Jelly] = 0;
            playerScores[PlayerType.PeanutButter] = 0;
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
            peanutButterText.text = string.Format(c_scoreFormat, playerScores[PlayerType.PeanutButter].ToString().PadLeft(2, '0'));
            jamText.text = string.Format(c_scoreFormat, playerScores[PlayerType.Jelly].ToString().PadLeft(2, '0'));
        }
        
    }
}
