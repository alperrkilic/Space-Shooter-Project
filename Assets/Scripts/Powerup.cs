using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    // ID for Powerups: Triple Shot = 0, Speed = 1, Shields = 2
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <-4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // other stands for the collided object
    {
        if(other.tag == "Player") // if collided object is player, than we'll activate triple shot powerup
        {
            Player player = other.transform.GetComponent<Player>(); // accessing to the player component

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if(player!= null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }


}
