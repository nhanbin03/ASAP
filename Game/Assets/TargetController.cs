using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameObject player;

    private float[] _patternArray = {1, 1};
    private float _curTime = 0;
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
