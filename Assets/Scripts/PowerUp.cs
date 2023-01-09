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
