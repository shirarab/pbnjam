using System;
using UnityEngine;

namespace BreadScripts
{ 
    public class BreadGrid : MonoBehaviour
    {
        [SerializeField] private Bread breadPrefab;
        [SerializeField] private int columns = 5;
        [SerializeField] private float margin = 0.1f;

        private float _screenHeight;
        private int _rows;
        private const int SCREEN_REF_HEIGHT = 1080;
        
        private void Start()
        {
            _screenHeight = Camera.main != null? Camera.main.orthographicSize * 2f : SCREEN_REF_HEIGHT;
        }
        
        internal void GenerateGrid()
        {
            var localScale = breadPrefab.transform.localScale;
            var x = localScale.x;
            var y = localScale.y;
            
            float totalWidth = columns * (x + margin) - margin;
            float startX = -totalWidth / 2f;

            int rows = Mathf.FloorToInt(_screenHeight / (y + margin)) + 1; 

            float totalHeight = rows * (y + margin) - margin;
            float startY = totalHeight / 2f; 

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    float xPos = startX + i * (x + margin);
                    float yPos = startY - j * (y + margin);
                    Instantiate(breadPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity, transform);
                }
            }
        }

    }

}