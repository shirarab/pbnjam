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

    
    [SerializeField] private Vector2 pointToGet_1;
    [SerializeField] private Vector2 pointToGet_2;
    [SerializeField] private Vector2 startPoint;

    private bool spawnFlag; //TODO?
    

    [SerializeField] private float speed;

    [SerializeField] GameObject toast_1;
    [SerializeField] GameObject toast_2;


    private void moveToSpawnPoint()
    {
        toast_1.transform.position = Vector2.MoveTowards(toast_1.transform.position, pointToGet_1, speed * Time.deltaTime);
        toast_2.transform.position = Vector2.MoveTowards(toast_1.transform.position, pointToGet_2, speed * Time.deltaTime);
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
