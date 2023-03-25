using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6f;

    private Player _player;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("The Player is NULL");
        }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("The Animator is NULL");
        }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") // Enemy Collides with Player, reduce player health;
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.GetComponent<Collider2D>());
            _speed = 0;
            Destroy(this.gameObject,2.8f); // Destroy the Enemy after it's collided with laser 2.8 seconds while animation performs
        }

        if(other.tag == "Laser") // Enemy Collides with Laser
        {
            Destroy(other.gameObject); // Destroy Laser

            if(_player != null) // if an enemy is destroyed, increase player score
            {
                _player.AddScore(10);
            }

            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.GetComponent<Collider2D>());
            _speed = 0;
            Destroy(this.gameObject,2.8f); // Destroy Enemy after 2.8 seconds while animation performs
        }

    }
}
