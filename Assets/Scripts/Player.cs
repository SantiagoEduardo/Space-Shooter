using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  // Start is called before the first frame update
  //public or private reference
  //data type (int, float, bool, string)
  //every variable has a name
  //optional value assigned
[SerializeField] //serializar los datos para que podamos lkeerlos en el inspector
  private float _speed = 5f;
  private float _speedMultiplier = 2;
  [SerializeField]
  private GameObject _laserPrefab; 
  [SerializeField]
  private GameObject _tripleShotPrefab;
  [SerializeField]
  private float _fireRate = 0.5f;
  private float _canFire = -1f;
  [SerializeField]
  private int _lives = 3;
  private SpawnManager _spawnManager;
  private bool _isTripleShotActive = false;
  private bool _isSpeedBoostActive = false;
  private bool _isShieldActive = false;
  [SerializeField]
  private GameObject _shieldVisualizer;

  [SerializeField]
  private int _score;


  private UIManager _uiManager;

  
  void Start()
    {
    //take the current position = new position (0,0,0)
    transform.position = new Vector3(0, 0, 0);

    //comunicate with SpawnManager, but first we need to find the GameObject, then Get Component
    _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    if (_spawnManager == null)
    {
      Debug.LogError("The Spawn Manager is NULL.");
    }

    if (_uiManager == null)
    {
        Debug.Log("The UI Manager is NULL.");
    }
  } 

    // Update is called once per frame
  void Update()
    {
       CalculateMovement();
       
    if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
    {
      FireLaser();
    }

  }


void CalculateMovement()
{

    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");
    //   transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
    //   transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

    // transform.Translate(new Vector3(horizontalInput, verticalInput,0) * _speed * Time.deltaTime);
    Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
 
    transform.Translate(direction * _speed * Time.deltaTime);

   

    // if player position on the y is greater than 0
    // y position = 0
    //else if position on the y es less than -3.8f 
    //y pos = -3.8f

/*
    if ( transform.position.y >= 0) 
    {
      transform.position = new Vector3( transform.position.x, 0, 0);
    }
    else if (transform.position.y <= -3.8f)
    {
      transform.position = new Vector3(transform.position.x, -3.8f,0);
    }
*/
    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.8f,0),0);
  
  
    if ( transform.position.x >= 11.3f )
    {
      transform.position = new Vector3(-11.3f, transform.position.y,0);
    }
    else if ( transform.position.x <= -11.3f )
    {
      transform.position = new Vector3(11.3f, transform.position.y,0);
    }
}

void FireLaser()
{
         //if I hit the space key
       //spawn gameObject
          _canFire = Time.time + _fireRate;
          //Debug.Log("Space Key Pressed");

    if ( _isTripleShotActive == true )
    {
      //Instantiate for the triple shot
      Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
    }
    else
    {
        Instantiate(_laserPrefab,transform.position + new Vector3(0,1.05f,0), Quaternion.identity);
    }


}

public void Damage()
{
    ////if shields is active
    //do nothing...
    //deactivate shields
    //return; //return hace que se termine la ejecución del método void y no continue hacia abajo
    if (_isShieldActive == true)
    {
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);
        return;
    }

    _lives --;
    _uiManager.UpdateLives(_lives);
    //check if dead destroy us
    if (_lives <1)
    {
        _spawnManager.onPlayerDeath(); 
        Destroy(this.gameObject);
    }
}

public void TripleShotActive()
{
  //TripleShotActive becomes true
  //start the power down coroutine for triple shot
  _isTripleShotActive = true;
  StartCoroutine(TripleShotPowerDownRoutine());
}

//IEnumerator TripleShotPowerDownRoutine
//wait 5 seconds
//set the triple shot to false
IEnumerator TripleShotPowerDownRoutine()
{
  yield return new WaitForSeconds(5.0f);
  _isTripleShotActive = false;
}


    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

IEnumerator SpeedBoostPowerDownRoutine()
{
    yield return new WaitForSeconds(5.0f);
    _isSpeedBoostActive = false;
    _speed /= _speedMultiplier;
}

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    //Method to add 10 to the score
    //Communicate with the UI to update the score
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
