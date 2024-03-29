﻿using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using Utils;

namespace BreadScripts
{ 
    public class BreadGrid : MonoBehaviour
    {
        [SerializeField] private Bread breadPrefab;
        [SerializeField] private int numberOfColumns = 5;
        [SerializeField] private float margin = 0.2f;
        [SerializeField] private Vector2 breadSize = new(1f, 1f);
        [SerializeField] private Vector3 breadScale = new(1.7f, 1.7f);
        [SerializeField] private RectTransform scoreBar;
        private const int NumberOfRows = 7;
        
        private HashSet<Bread> allBreads = new();
        
        internal void GenerateGrid()
        {
            var cameraMain = Camera.main;
            breadPrefab.transform.localScale = breadScale;
            
            float scoreBarHeight = (!scoreBar.IsUnityNull()) ? scoreBar.rect.height / Screen.height * cameraMain.orthographicSize * 2f : 0f;
            
            float startX = -(numberOfColumns - 1) * (breadSize.x + margin) / 2f; // same as: -(columns * (breadSize.x + margin)) / 2f + (breadSize.x + margin) / 2f;
            float startY = CalculateStartingYPosition(scoreBarHeight, cameraMain) + margin;

            for (int col = 0; col < numberOfColumns; col++)
            {
                float x = startX + col * (breadSize.x + margin);

                for (int row = 0; row < NumberOfRows; row++)
                {
                    float y = startY + row * (breadSize.y + margin);
                    Bread currBread = Instantiate(breadPrefab, new Vector3(x, y, 0f), Quaternion.identity);
                    allBreads.Add(currBread);
                }
            }
        }

        private int CalculateNumberOfRows(float scoreBarHeight, Camera cameraMain)
        {
            // float screenHeight = !cameraMain.IsUnityNull() ? cameraMain.orthographicSize * 2f : SCREEN_HEIGHT;
            // screenHeight -= scoreBarHeight;
            // return Mathf.FloorToInt(screenHeight / (breadSize.y + margin));
            return 7;
        }
        
        private float CalculateStartingYPosition(float scoreBarHeight, Camera cameraMain)
        {
            float centerY = cameraMain.transform.position.y;
            float halfHeight = cameraMain.orthographicSize;
            
            centerY += (scoreBarHeight / 2f);
            halfHeight -= (scoreBarHeight / 2f);
            
            return centerY - halfHeight + (breadScale.y + margin) / 2f;
            
        }
        
        public void ResetGrid()
        {
            foreach (Bread bread in allBreads)
            {
                bread.ResetBread();
            }
        }

        public int ResetBreadType(BreadType breadType)
        {
            int resetCount = 0;
            
            foreach (var bread in allBreads)
            {
                if (bread.CurrentBreadType.Equals(breadType))
                {
                    bread.ResetBread();
                    resetCount += 1;
                }
            }

            return resetCount;
        }

        public Dictionary<BreadType, int> GetBreadCountsByType()
        {
            var breadCounts = new Dictionary<BreadType, int>();

            foreach (var breadType in EnumUtils.GetValues<BreadType>())
            {
                breadCounts.Add(breadType, 0);
            }

            foreach (var bread in allBreads)
            {
                breadCounts[bread.CurrentBreadType] += 1;
            }

            return breadCounts;
        }

        public int GetNumberOfBreads()
        {
            return allBreads.Count;
        }
    }
}
