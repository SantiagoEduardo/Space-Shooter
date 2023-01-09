using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

  [SerializeField] //serializar los datos para que podamos leerlos en el inspector
  private float _speed = 8.0f;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
      transform.Translate(Vector3.up * _speed * Time.deltaTime);

      //if laser position is freater thab 8 on the y
      //destroy the object

      if (transform.position.y >7f)
      {
        //check if this object has a parent.
        //destroy the parent too!
        if(transform.parent != null)
        {
          Destroy(transform.parent.gameObject);
        }
        Destroy(this.gameObject);
       }


    }
}
