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

    
    [SerializeField] private Vector3 pbPoint;
    [SerializeField] private Vector3 jamPoint;
    [SerializeField] private float speed;

    [SerializeField] GameObject pbToast;
    [SerializeField] GameObject jamToast;

    #region  ANIMATION————————————————-

    private Animator toastAnimator;
    [SerializeField]private float toastAnimatiomHelper;


    public void TriggerAnimation(bool animationState)
    {
        toastAnimator.SetBool("toastAnimationState", animationState);
    }


    #endregion

    private void InitializePoints()
    {
        pbPoint = new Vector3(UnityEngine.Random.Range(maxLeft, maxRight), UnityEngine.Random.Range(maxDown, maxTop), 0f);
        jamPoint = new Vector3(UnityEngine.Random.Range(-maxLeft, -maxRight), UnityEngine.Random.Range(maxDown, maxTop), 0f);
    }


    private void MoveToSpawnPoint()
    {
        // check if pbToast and jamToast are enabled
        // if not, enable them and move them to the pointToGet_1 and pointToGet_2
        if (!pbToast.gameObject.activeSelf) 
        {
            pbToast.GetComponent<Transform>().position = pbPoint;
            pbToast.SetActive(true);
            
        }
        if (!jamToast.gameObject.activeSelf)
        {
            jamToast.GetComponent<Transform>().position = jamPoint;
            jamToast.SetActive(true);
            
        }
    }
    
    public IEnumerator SpawnToast()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            TriggerAnimation(true);
            yield return new WaitForSeconds(toastAnimatiomHelper); 
            TriggerAnimation(false);
            InitializePoints();
            MoveToSpawnPoint();
        }
    }
    
}