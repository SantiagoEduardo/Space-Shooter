using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    //ID for PowerUps
    //0 = Triple Shot
    //1 = Speed
    //2 = Shields
    [SerializeField]    
    private int powerupID;

   /* 
   private AudioSource _audioSource;

[*] Aqui no debemos utilizar un clip de audio referenciado a un objeto ya que al destruirlo
se interrumpe el audio

[*]instanciar un objeto vacio cada que queremos el audio alentará el juego

[*]crear una corutina para esperar a que el audio termine de reproducirse pero inhabilitando el objeto destruido tampoco parece ser una buena solucion, asi que mejor vamos a obtener una referencia al audio que queremos sin depender del objeto:

*/


 

    void Update()
    {
        //move down at a speed of 3
        //When we leave the screen, destroy this object
        transform.Translate(Vector3.down * _speed * Time.deltaTime); 

        if(transform.position.y < -4.5f)
        {
          Destroy(this.gameObject);
        }
    }

    //OnTriggerCollision
    //Only ve colectable by the Player (HINT: Use Tags)
    //on collected, destroy
    private void OnTriggerEnter2D(Collider2D other) 
    {
      if (other.tag =="Player")
      {
        //comunicate with the player script
        //handle to the component I want
        //assign the handle to the component
        Player player = other.transform.GetComponent<Player>();

        // Para repdoducir nuestro audio sin tener la referencia en el objeto, solo en una variable
 //PlayClipAtPoint parece que emite el sonido desde un vector que indicamos y si está lejos de la cámara se escucha bajo el sonido, si se pone como ruta la cámara se elimina el efecto stereo, sde donde proviene el origend el sonido, izquierda o derecha
     //   AudioSource.PlayClipAtPoint(_clip, transform.position);

    //    AudioSource.PlayClipAtPoint(_clip, new Vector3(transform.position.x, transform.position.y, -10.0f));
 



        if(player != null)
        {
 
          //else if powerup is 1
          //play speed powerup
          //else if powerup is 2
          //shields powerup

          switch (powerupID)
          {
            case 0:
              player.TripleShotActive();
              break;
            case 1:
              player.SpeedBoostActive();
              break;
            case 2:
              player.ShieldActive();
              break;
            default:
              break;
          }
        }

        Destroy(this.gameObject);
      }
      
    }
}
