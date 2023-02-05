using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        //if the R key was pressed
        //restart the current scene
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //Current Game Scene
            //Para asignar la escena 0 primero debemos ir a File -> Build Settings -> Add scene y se generará un indice para la escena en cuestión
        }
    }

    public void GameOver()
    {
        Debug.Log("GameManager::GameOver() Called");
        //en el comentario profesionalmente indicamos la clase::metodo
        _isGameOver = true;
    }

}
