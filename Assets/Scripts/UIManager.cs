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


    void Start()
    {
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
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
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverFLickerRoutine());
        }
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
