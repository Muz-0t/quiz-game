using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public int heartCount;
    private void Start()
    {
        heartCount = UserInfomations.HeartCount;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void AllClearStage()
    {
        //TODO: 전체 스테이지 클리어 처리
        QuitGame();
    }
    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
    
    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
        UserInfomations.HeartCount = heartCount;
        
    }
}
