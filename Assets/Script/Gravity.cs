using NUnit.Framework;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using System.Collections.Generic; //for List

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.00674f; //Gravitational Constant 6.674

    public static List<Gravity> otherObjectsList;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //create a list for first time
        if(otherObjectsList == null )
        {
            otherObjectsList = new List<Gravity>();
        }

        //add object (with gravity script) to attract to the list
        otherObjectsList.Add(this);
    }

    private void FixedUpdate()
    {
        foreach(Gravity obj in otherObjectsList)
        {
            if(obj != this)
            {
                Attract(obj);
            }
        }
    }
    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - otherRb.position;

        float distance = direction.magnitude;

        //if 2 objs are same position, just return = do nothing to avoid collision
        if( distance == 0f) { return; }

        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);

        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravityForce);

    }
}
