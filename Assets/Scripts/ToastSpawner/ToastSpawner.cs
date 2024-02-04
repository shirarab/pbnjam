using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastSpawner : MonoBehaviour
{
    [SerializeField] private float maxLeftPb;
    [SerializeField] private float maxRightPb;
    [SerializeField] private float maxLeftJam;
    [SerializeField] private float maxRightJam;
    [SerializeField] private float maxTop;
    [SerializeField] private float maxDown; 
    [SerializeField] private float spawnRate = 5.0f;

    
    [SerializeField] private Vector2 pointToGet_1;
    [SerializeField] private Vector2 pointToGet_2;
    [SerializeField] private Vector2 startPoint;

    private bool spawnFlag; //TODO?
    

    [SerializeField] private float speed;

    [SerializeField] GameObject toast_1;
    [SerializeField] GameObject toast_2;

    private void Start()
    {
        // get random postion for pointToGet_1 and pointToGet_2
        // based on the maxLeftPb, maxRightPb, maxLeftJam, maxRightJam, maxTop, maxDown
        InitializePoints();
    }

    private void InitializePoints()
    {
        pointToGet_1 = new Vector2(UnityEngine.Random.Range(maxLeftPb, maxRightPb), UnityEngine.Random.Range(maxDown, maxTop));
        pointToGet_2 = new Vector2(UnityEngine.Random.Range(maxLeftJam, maxRightJam), UnityEngine.Random.Range(maxDown, maxTop));
    }

    private void MoveToSpawnPoint()
    {
        // check if toast_1 and toast_2 are enabled
        // if not, enable them and move them to the pointToGet_1 and pointToGet_2
        if (!toast_1.activeSelf)
        {
            toast_1.SetActive(true);
            toast_1.transform.position = Vector2.MoveTowards(toast_1.transform.position, pointToGet_1, speed * Time.deltaTime);
        }
        if (!toast_2.activeSelf)
        {
            toast_2.SetActive(true);
            toast_2.transform.position = Vector2.MoveTowards(toast_1.transform.position, pointToGet_2, speed * Time.deltaTime);
        }
        
    }
    
    private IEnumerator SpawnToast()
    {
        while (true)
        {
            InitializePoints();
            MoveToSpawnPoint();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    
}