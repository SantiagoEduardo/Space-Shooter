using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // handle to text
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;



    void Start()
    {
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Es importante saber si la referenicia que buscamos de nuestro GameObject es nulo o no
        if (_gameManager == null)
        {
            Debug.Log("GameManager is NULL.");
        }
    }


    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }


    public void UpdateLives(int currentLives)
    {
        //display ing sprite
        //give it a new one based on the currentLives index
        _LivesImg.sprite = _liveSprites[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        //tiene sentido colocar la lectura de la tecla R aqui? no, para eso se diseña un GAME MANAGER, que es el responsable de gestionar el estado del juego:
        //Se ha acabado la partida? el jugador sigue vivo?, Cuantos enemigos hay en el juego?
        //Al Crear el Script Game Manager Unity le coloca un icono de Egranaje, es una configuragion por default

        StartCoroutine(GameOverFLickerRoutine());
    }

    IEnumerator GameOverFLickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
