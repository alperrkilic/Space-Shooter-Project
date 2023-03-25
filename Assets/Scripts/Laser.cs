using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 20.0f; //speed variable of 8

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime); //Translate laser up

        if(transform.position.y > 8f) // after laser is not seen anymore, destroy the object.
        {

            if(transform.parent != null) // Parent of the laser can be triple shot, with this control statement, we're deleting empty objects
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }

    }
}
