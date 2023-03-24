using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed ); 

        if(transform.position.y < -5f) // if bottom of screen, respawn at top with a new random x position
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7 , 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") // Enemy Collides with Player, reduce player health;
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject); // Destroy Enemy
            Destroy(this.gameObject); // Destroy Laser
        }

    }
}
