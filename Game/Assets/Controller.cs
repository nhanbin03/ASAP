using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Sprite NormalArm;
    public Sprite ArmedArm;

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
                    _curTime = 0;
                }
                UpdateChanneling();
            } else {
                IsChanneling = false;
                _curTime = 0;
            }
            if (IsChanneling) {
                _sprite.sprite = ArmedArm;
            } else {
                _sprite.sprite = NormalArm;
                _sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void UpdateChanneling() {
        if (_curTime < CHANNEL_TIME * 5 / 6) {
            double totalTime = CHANNEL_TIME * 5 / 6;
            _sprite.transform.rotation = Quaternion.Euler(0, 0, 90 + (180 - 90) * (float) (_curTime / totalTime));
        } else {
            double totalTime = CHANNEL_TIME / 6;
            _sprite.transform.rotation = Quaternion.Euler(0, 0, 90 + (0 - 90) * (float)((_curTime - CHANNEL_TIME * 5 / 6) / totalTime));
        }
    }
}
