﻿using System;
using UnityEngine;

namespace BreadScripts
{ 
    public class BreadGrid : MonoBehaviour
    {
        [SerializeField] private Bread breadPrefab;
        [SerializeField] private int numberOfColumns = 5;
        [SerializeField] private float margin = 0.2f;
        [SerializeField] private Vector2 breadSize = new(1f, 1f);
        [SerializeField] private Vector3 breadScale = new(1.5f, 1.5f);
        [SerializeField] private RectTransform scoreBar;

        private const int SCREEN_HEIGHT = 10;
        
        internal void GenerateGrid()
        {
            breadPrefab.transform.localScale = breadScale;
            
            float scoreBarHeight = (scoreBar != null) ? scoreBar.rect.height / Screen.height * Camera.main.orthographicSize * 2f : 0f;
            
            float startX = -(numberOfColumns - 1) * (breadSize.x + margin) / 2f; // same as: -(columns * (breadSize.x + margin)) / 2f + (breadSize.x + margin) / 2f;
            float startY = CalculateStartingYPosition(scoreBarHeight) + margin;
            
            int numberOfRows = CalculateNumberOfRows(scoreBarHeight);

            for (int col = 0; col < numberOfColumns; col++)
            {
                float x = startX + col * (breadSize.x + margin);

                for (int row = 0; row < numberOfRows; row++)
                {
                    float y = startY + row * (breadSize.y + margin);
                    Instantiate(breadPrefab, new Vector3(x, y, 0f), Quaternion.identity);
                }
            }
        }

        private int CalculateNumberOfRows(float scoreBarHeight)
        {
            float screenHeight = Camera.main != null ? Camera.main.orthographicSize * 2f : SCREEN_HEIGHT;
            screenHeight -= scoreBarHeight;
            return Mathf.FloorToInt(screenHeight / (breadSize.y + margin));
        }
        
        private float CalculateStartingYPosition(float scoreBarHeight)
        {
            float centerY = Camera.main.transform.position.y;
            float halfHeight = Camera.main.orthographicSize;

            centerY -= (scoreBarHeight / 2f);
            halfHeight -= (scoreBarHeight / 2f);

            return centerY - halfHeight + (breadSize.y + margin) / 2f;
        }
    }
}
