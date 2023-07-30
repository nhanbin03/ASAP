using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    public GameObject HomeScreen;
    public GameObject VictoryScreen;
    public GameObject LoseScreen;


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
        HomeScreen.SetActive(state == GameState.Home);
        VictoryScreen.SetActive(state == GameState.Victory);
        LoseScreen.SetActive(state == GameState.Lose);
    }

    public void SetHomeState() {
        GameManager.Instance.UpdateGameState(GameState.Home);
    }
    public void SetGameplayState()
    {
        GameManager.Instance.UpdateGameState(GameState.Gameplay);
    }
    public void SetVictoryState()
    {
        GameManager.Instance.UpdateGameState(GameState.Victory);
    }
    public void SetLoseState()
    {
        GameManager.Instance.UpdateGameState(GameState.Lose);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
