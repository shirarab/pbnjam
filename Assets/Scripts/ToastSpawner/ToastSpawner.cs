using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastSpawner : MonoBehaviour
{

    #region  BOREDER POINTS
    [SerializeField] private float maxLeft;
    [SerializeField] private float maxRight;
    [SerializeField] private float maxTop;
    [SerializeField] private float maxDown; 
    [SerializeField] private float spawnRate = 5.0f;
    #endregion
    
    #region  SPAWN POINTS
    [SerializeField] private Vector3 spawnPoint;

    private Vector3 toasterPoint;
    #endregion

    [SerializeField] private float speed;

    #region GAME OBJECTS
    [SerializeField] GameObject toast;
    [SerializeField] MovementAnimation toastMovement;
    [SerializeField] private Transform toasterTransform;
    #endregion


    #region  ANIMATION

    [SerializeField] private Animator toastAnimator;
    [SerializeField] private float toastAnimatiomHelper;


    private void Start()
    {
        toasterPoint = toasterTransform.position;
    }

    private void TriggerAnimation(bool animationState)
    {
        toastAnimator.SetBool("toastAnimationState", animationState);
    }
    #endregion

    public IEnumerator SpawnToast()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            //————————-
            // check if pbToast and jamToast are enabled
            // if not, enable them and move them to the pointToGet_1 and pointToGet_2
            if (!toast.gameObject.activeSelf) 
            {
                spawnPoint = new Vector3(UnityEngine.Random.Range(maxLeft, maxRight), UnityEngine.Random.Range(maxDown, maxTop), 0f);
                Debug.Log("pbPoint: " + spawnPoint);
                toast.transform.position = toasterPoint;
                //ANIMATION
                TriggerAnimation(true);
                yield return new WaitForSeconds(toastAnimatiomHelper); 
                TriggerAnimation(false);
                toast.SetActive(true);
                toastMovement.SetTarget(spawnPoint);
                //TODO? setActive(true) -> Toast.transform.position = Vector2 moveTowards(…) <————————————————-THE OTHER OPTION FOR SPAWN
                //then: need to add startPosition for the position of the toaster 
            
            }
        }
    }
    
}