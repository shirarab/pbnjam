using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour
{
    private Vector3 target;
    public void SetTarget(Vector3 newTarget)
    {
        target = newTarget;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 5);
    }
}
