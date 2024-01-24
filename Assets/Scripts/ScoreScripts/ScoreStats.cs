using UnityEngine;

namespace ScoreScripts
{
    [System.Serializable]
    public class ScoreStats
    {
        // todo maybe player should hold score 
        [SerializeField]
        private PlayerType playerType;
        internal PlayerType PlayerType => playerType;
    }
}
