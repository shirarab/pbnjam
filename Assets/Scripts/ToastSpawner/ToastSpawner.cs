using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastSpawner : MonoBehaviour
{
    [SerializeField] private float maxLeft;
    [SerializeField] private float maxRight;
    [SerializeField] private float maxTop;
    [SerializeField] private float maxDown; 
    [SerializeField] private float spawnRate = 5.0f;

    
    [SerializeField] private Vector2 pbPoint;
    [SerializeField] private Vector2 jamPoint;
    [SerializeField] private Vector2 startPoint;

    private bool spawnFlag; //TODO?
    

    [SerializeField] private float speed;

    [SerializeField] GameObject pbToast;
    [SerializeField] GameObject jamToast;

    private void InitializePoints()
    {
        pbPoint = new Vector2(UnityEngine.Random.Range(maxLeft, maxRight), UnityEngine.Random.Range(maxDown, maxTop));
        jamPoint = new Vector2(UnityEngine.Random.Range(-maxLeft, -maxRight), UnityEngine.Random.Range(maxDown, maxTop));
    }


    private void MoveToSpawnPoint()
    {
        Debug.Log("Moving to spawn point");
        // check if pbToast and jamToast are enabled
        // if not, enable them and move them to the pointToGet_1 and pointToGet_2
        if (!pbToast.gameObject.activeSelf) 
        {
            Debug.Log("pbToast is not active");
            pbToast.SetActive(true);
            pbToast.transform.position = Vector2.MoveTowards(pbToast.transform.position, pbPoint, speed * Time.deltaTime);
        }
        if (!jamToast.gameObject.activeSelf)
        {
            Debug.Log("jamToast is not active");
            jamToast.SetActive(true);
            jamToast.transform.position = Vector2.MoveTowards(jamToast.transform.position, jamPoint, speed * Time.deltaTime);
        }
    }
    
    public IEnumerator SpawnToast()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            InitializePoints();
            MoveToSpawnPoint();
        }
    }
    
}