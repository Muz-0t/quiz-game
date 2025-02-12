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
        InitQuizCard();
    }

    private void InitQuizCard()
    {
        var firstCardObejct = ObjectPool.Instance.GetObject();
        var secondCardObejct = ObjectPool.Instance.GetObject();
        var thirdCardObejct = ObjectPool.Instance.GetObject();
    }
}
