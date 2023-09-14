using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float _countdown = 4;
    private bool _timerOn = true;
    private GameManager _gameManager;

    [SerializeField] private TMP_Text _timerText;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        
        if (_timerOn)
        {
            _countdown -= Time.deltaTime;
            _timerText.text = Mathf.FloorToInt(_countdown).ToString("0");
            if (_countdown < 1 )
            {
                _timerOn = false;
                _gameManager._isGameStart = true;
                Destroy(_timerText);
                EventManager.CallTimerIsDone(true);
            }
            
        }
    }
}

