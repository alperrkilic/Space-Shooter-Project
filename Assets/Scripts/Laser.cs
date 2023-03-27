using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 20.0f; //speed variable of 20
    private bool _isEnemyLaser = false;

    void Update()
    {

        if(_isEnemyLaser == false)
        {
            moveUp();
        }
        else
        {
            moveDown();
        }

    }

    void moveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime); //Translate laser up

        if (transform.position.y < -8f) // after laser is not seen anymore, destroy the object.
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }


    void moveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime); //Translate laser up

        if (transform.position.y > 8f) // after laser is not seen anymore, destroy the object.
        {

            if (transform.parent != null) // Parent of the laser can be triple shot, with this control statement, we're deleting empty objects
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
        }

    }


}
