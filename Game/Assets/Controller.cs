using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool IsChanneling = false;
    private SpriteRenderer _sprite;
    private bool _isActive;


    public const double CHANNEL_TIME = 1.2f;
    private double _curTime = 0;

    void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state) {
        _isActive = state == GameState.Gameplay;
    }

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_isActive) {
            if (Input.GetKey(KeyCode.Space)) {
                IsChanneling = true;
                _curTime += Time.deltaTime;
                if (_curTime > CHANNEL_TIME) {
                    Debug.Log("Win!");
                }
            } else {
                IsChanneling = false;
                _curTime = 0;
            }
            if (IsChanneling) {
                _sprite.color = Color.blue;
            } else {
                _sprite.color = Color.red;
            }
        }
    }
}
