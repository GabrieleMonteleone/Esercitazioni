using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetReacher_Empty : MonoBehaviour
{
    //What do you need?
    // A target prefab
    // A variable to store the minimum distance
    // A variable to store the last created target

    public GameObject TargetPrefab;
    public float MinDistance = 0.2f;

    private GameObject _newtarget;
    void Start()
    {
        //Create the first Target
        CreateTarget();
    }

    void Update()
    {
        //Check if the target exists 
        if (_newtarget == null)
            return;

        float distance = Vector3.Distance(gameObject.transform.position, _newtarget.transform.position);
        //If it exists check if the distance between the target and the moving character is smaller than the a minimum distance
        //To calculate distance use the function Vector3.Distance();
        //Reference: https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
        if (distance <= MinDistance)
        {
            Destroy(_newtarget);
            CreateTarget();
        }
        //If the character has reached the target destroy the current one and create a new one in a random position
    }

    public void CreateTarget()
    {
        Vector3 random = new Vector3(Random.Range(-14,14),0, Random.Range(-14,14));
        _newtarget = Instantiate(TargetPrefab);
        _newtarget.transform.position = random;
    }
    
}
