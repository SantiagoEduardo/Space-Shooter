using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;   
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    private bool _stopSpawning = false;

    void Start()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }


    void Update()
    {
        
        //spawn objects every 5 seconds
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f), 7,0);
            GameObject newEnemy = Instantiate(_enemyPrefab,posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }

    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
      //every 3-7 seconds, spawn in a powerup
      while(_stopSpawning == false)
      {
        Vector3 posToSpawnP = new Vector3(Random.Range(-8f, 8f), 7, 0);
        int randomPowerUp = Random.Range(0,3);
        Instantiate(powerups[randomPowerUp], posToSpawnP, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(3,8));        
      }      

    }

    public void onPlayerDeath()
    {
      _stopSpawning = true;
    }
}

//No necesitamos de un Objetom Contenedor en los Power Up porque a diferencia de los enemigos, no
//lo estamos reciclando, estos se destruyen inmediatamente despues de desaparecer de la pantalla
//Quaternion.identity = sin rotacion