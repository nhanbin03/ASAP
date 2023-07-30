using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TargetController : MonoBehaviour
{
    public GameObject WarningSign;
    public GameObject player;
    public TextMeshProUGUI LevelDisplayer;

    public double[][] PatternManager = {
        new double[] {2, 2},
        new double[] {0.5, 1, 0.5, 1, 2, 1},
        new double[] {0.5, 0.5, 0.5, 0.5, 2, 0.5, 0.5, 0.5},
        new double[] {2, 0.5, 1, 1, 1.2, 0.5, 1.2, 0.5},
        new double[] {2, 0.5, 2, 0.5, 2, 0.5, 0.2, 0.5},
        new double[] {1.5, 0.5},
        new double[] {1.5, 1, 0.5, 0.5, 0.5, 1},
        new double[] {0.5, 1, 0.2, 0.5, 1.5, 0.2, 0.2, 0.2}
    };

    private double[] _patternArray; 
    private double _curTime = 0;
    private int _patternState = 0;

    public int Difficulty = 0;

    private SpriteRenderer _sprite;
    private bool _isActive;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }


    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        _isActive = state == GameState.Gameplay;
        _curTime = 0;
        _patternState = 0;
        if (state == GameState.Victory) {
            Difficulty = Math.Min(Difficulty + 1, PatternManager.Length - 1);
        }
        if (state == GameState.Lose) {
            Difficulty = 0;
        }
    }

    void Start() {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        LevelDisplayer.text = "Level: " + (Difficulty + 1).ToString();
        if (_isActive) {
            _patternArray = PatternManager[Difficulty];
            _curTime += Time.deltaTime;
            while (_curTime > _patternArray[_patternState]) {
                int nextState = (_patternState + 1) % _patternArray.Length;
                _curTime -= _patternArray[_patternState];

                _patternState = nextState;
            }
            if (_patternState % 2 == 0)
            {
                Vector3 newScale = transform.localScale;
                newScale.x = Math.Abs(newScale.x);
                transform.localScale = newScale;
                WarningSign.SetActive(false);
                if (_curTime > _patternArray[_patternState] * 0.8) {
                    WarningSign.SetActive(true);

                }
            }
            else
            {
                WarningSign.SetActive(false);
                Vector3 newScale = transform.localScale;
                newScale.x = -Math.Abs(newScale.x);
                transform.localScale = newScale;
            }
            if (player.GetComponent<Controller>().IsChanneling && _patternState % 2 == 1) {
                GameManager.Instance.UpdateGameState(GameState.Lose);
            }
        }
    }
}
