using UnityEngine;

namespace ScoreScripts
{
    public class Score : MonoBehaviour
    {
        #region Constants
        private const string c_scoreFormat = "Score: {0}";
        #endregion
        
        #region Score fields
        [SerializeField] 
        ScoreComponents components;
        public ScoreComponents Components => components;
        
        [SerializeField] 
        ScoreStats stats;
        public ScoreStats Stats => stats;

        #endregion

        private int _score = 0;
        
        private void Start()
        {
            UpdateScoreText();
        }

        #region Points

        public void AddPoints(int points, int playerIndex)
        {
            _score += points;
            UpdateScoreText();
        }

        public void ResetPoints()
        {
            _score = 0;
            UpdateScoreText();
        }

        #endregion

        private void UpdateScoreText()
        {
            components.ScoreText.text = string.Format(c_scoreFormat, _score);
        }
    }
}
