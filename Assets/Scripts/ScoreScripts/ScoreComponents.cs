using UnityEngine;
using UnityEngine.UI;

namespace ScoreScripts
{
    [System.Serializable]
    public class ScoreComponents
    {
        [SerializeField] 
        private Text scoreText;
        public Text ScoreText => scoreText;
    }
}