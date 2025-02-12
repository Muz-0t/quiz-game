using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private GameObject quizCardPrefab;     //퀴즈 카드의 프리팹
    [SerializeField] private Transform quizCardParent;      //퀴즈 카드가 표시 될 UI Parent

    private void Start()
    {
        ShowQuizCard();
    }

    private void ShowQuizCard()
    {
        var quizCardObject = Instantiate(quizCardPrefab, quizCardParent);
    }
    public void OnClickGameOverButton()
    {
        GameManager.Instance.QuitGame();
    }
}
