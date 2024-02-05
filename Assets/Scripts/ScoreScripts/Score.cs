using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace ScoreScripts
{
    public class Score : MonoBehaviour
    {
        #region Constants
        private const string c_scoreFormat = "{0}";
        #endregion
        
        #region Score fields
        [SerializeField]
        private int maxScore = 5;
        private Dictionary<PlayerType, int> playerScores;
        
        [SerializeField]
        private Animator pbAnimator;
        
        [SerializeField]
        private Animator jamAnimator;

        private int numberOfBreads;
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
            float pbScorePercentage = (float)playerScores[PlayerType.PeanutButter] / numberOfBreads;
            float jamScorePercentage = (float)playerScores[PlayerType.Jelly] / numberOfBreads;

            int pbScoreValue = Mathf.RoundToInt(pbScorePercentage * maxScore);
            int jamScoreValue = Mathf.RoundToInt(jamScorePercentage * maxScore);
            
            pbAnimator.SetInteger(Constants.PB_SCORE, pbScoreValue);
            jamAnimator.SetInteger(Constants.JAM_SCORE, jamScoreValue);
        }

        public void SetNumberOfBreads(int num)
        {
            numberOfBreads = num;
        }
    }
}
