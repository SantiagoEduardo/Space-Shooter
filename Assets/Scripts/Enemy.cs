using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();    
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if bottom of screen
        //respawn atat top with a new random x position
        if (transform.position.y < -5f)
        {
          float randomX = Random.Range(-8f, 8f); 
          transform.position = new Vector3(randomX,7,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      //if other is Player damage the player and destroy us
      if (other.tag == "Player")
      {
        Player player = other.transform.GetComponent<Player>();

        if (player != null)
        {
          player.Damage();  //other.transform.GetComponent<Player>().Damage();
        }
        
        Destroy(this.gameObject);
      }

      //if other is laser destroy laser and destroy us
      if (other.tag == "Laser")
      {
        Destroy(other.gameObject);
        //Add 10 to score
        //Se agrega otra variable de player ya que la asignaciónde player anterior está dentro de 
        //un if al que no tenemos acceso desde acá
        //Usar el compinente GET con frecuencia es costoso y debemos tratar de usarlo lo menos posible
        //Cada que disparemos al enemigo estará haciendo referencia al componente player, eso no es buena práctica:

        //Player player = GameObject.Find("Player").GetComponent<Player>();
        if (_player != null)
        {
            _player.AddScore(10);
        }

        Destroy(this.gameObject);
      }
      
      
    }
}
