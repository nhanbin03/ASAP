using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetController : MonoBehaviour
{
    public GameObject player;

    public double[][] PatternManager = {
        new double[] {2, 2},
        new double[] {0.5, 1, 0.5, 1, 2, 1},
        new double[] {0.5, 0.5, 0.5, 0.5, 2, 0.5, 0.5, 0.5},
        new double[] {2, 0.5, 1, 1, 1.2, 0.5, 1.2, 0.5},
        new double[] {2, 0.5, 2, 0.5, 2, 0.5, 0.2, 0.5},
        new double[] {0.5, 1, 0.2, 0.5, 1.5, 0.2, 0.2, 0.2},
        new double[] {1.5, 0.5},
        new double[] {1.5, 1, 0.5, 0.5, 0.5, 1}
    };

    private double[] _patternArray; 
    private double _curTime = 0;
    private int _patternState = 0;


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
        Debug.Log(state.ToString());
        _isActive = state == GameState.Gameplay;
    }

    void Start() {
        _patternArray = PatternManager[PatternManager.Length - 1];
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_isActive) {
            _curTime += Time.deltaTime;
            while (_curTime > _patternArray[_patternState]) {
                int nextState = (_patternState + 1) % _patternArray.Length;
                _curTime -= _patternArray[_patternState];

                _patternState = nextState;
            }
            if (_patternState % 2 == 0)
            {
                _sprite.color = Color.blue;
            }
            else
            {
                _sprite.color = Color.red;
            }
            if (player.GetComponent<Controller>().IsChanneling && _patternState % 2 == 1) {
                Debug.Log("Detected!");
            }
        }
    }
}
