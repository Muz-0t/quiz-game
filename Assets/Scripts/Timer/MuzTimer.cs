using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MuzTimer : MonoBehaviour
{
    [System.Serializable]
    public class FillSettings
    {
        public Color color;
    }

    public FillSettings fillSettings;

    [Serializable]
    public class BackGroundSettings
    {
        public Color color;
    }
    public BackGroundSettings backGroundSettings;
    
    [SerializeField] private Image fillImage;
    [SerializeField] private float totalTime;
    [SerializeField] private TMP_Text timeText;
    
    public float CurrentTime{get; private set;}
    private bool _isPaused;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (!_isPaused)
        {
            CurrentTime += Time.deltaTime;
            fillImage.fillAmount = Math.Clamp((CurrentTime / totalTime),0,1);
            var remainTime = Math.Max(0,totalTime-CurrentTime);
            timeText.text = remainTime.ToString("F1");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseTimer();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTimer();
        }
    }

    public void StartTimer()
    {
        fillImage.fillAmount = 0;
        _isPaused = false;
    }

    public void PauseTimer()
    {
        _isPaused = !_isPaused;
    }

    public void ResetTimer()
    {
        CurrentTime = 0;
        fillImage.fillAmount = 0;
    }

}
