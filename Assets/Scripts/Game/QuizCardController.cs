using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public struct QuizData
{
    public string question;
    public string description;
    public int type;
    public int answer;
    public string firstOption;
    public string secondOption;
    public string thirdOption;
}

public class QuizCardController : MonoBehaviour
{
    //Front Panel
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private GameObject frontPanel;
    [SerializeField] private GameObject threeOptionButtons;
    [SerializeField] private GameObject oxButtons;
    //[SerializeField] private Button addHeartButton;
    
    //정답, 오답 패널 프리팹
    [SerializeField] private GameObject correctBackPanel;
    [SerializeField] private GameObject incorrectBackPanel;
    [SerializeField] private MuzTimer timer;
    
    //Incorrect Back Panel
    [SerializeField] private TMP_Text heartCountText;
    
    [SerializeField] HeartPanelController heartPanel;
    
    
    private enum QuizCardPanelType{Front, CorrectBackPanel,IncorrectBackPanel}
    
    public delegate void QuizCardDelegate(int cardIndex);
    private event QuizCardDelegate onCompleted;

    private int _answer;
    private int _quizCardIndex;

    private Vector2 _correctBackPanelPosition;
    private Vector2 _incorrectBackPanelPosition;

    private void Awake()
    {
        _correctBackPanelPosition = correctBackPanel.GetComponent<RectTransform>().anchoredPosition;
        _incorrectBackPanelPosition = incorrectBackPanel.GetComponent<RectTransform>().anchoredPosition;
    }

    private void Start()
    {
        timer.OnTimeout = () =>
        {
            //TODO: 오답 연출
            SetQuizCardPanelActive(QuizCardPanelType.IncorrectBackPanel);
        };
    }

    public void SetVisible(bool visible)
    {
        if (visible)
        {
            timer.InitTimer();
            timer.StartTimer();
        }
        else
        {
            timer.InitTimer();
        }
    }

    public void SetQuiz(QuizData quizData, int quizCardIndex,QuizCardDelegate onCompleted)
    {
        // 1. 퀴즈
        // 2. 설명
        // 3. 타입 (0: OX퀴즈, 1: 보기 3개 객관식)
        // 4. 정답
        // 5. 보기 (1,2,3)
        
        //퀴즈 카드 인덱스 할당
        _quizCardIndex = quizCardIndex;
        //FrontPanel표시
        frontPanel.SetActive(true);
        correctBackPanel.SetActive(false);
        incorrectBackPanel.SetActive(false);
        
        // 퀴즈 데이터 표현
        questionText.text = quizData.question;
        _answer = quizData.answer;
        descriptionText.text = quizData.description;
        // descriptionText.text = quizData.description;
        

        //3지선다 게임
        if (quizData.type == 0)
        {
            threeOptionButtons.SetActive(true);
            oxButtons.SetActive(false);
            
            var firstButtonText = optionButtons[0].GetComponentInChildren<TMP_Text>();
            firstButtonText.text = quizData.firstOption;
            var secondButtonText = optionButtons[1].GetComponentInChildren<TMP_Text>();
            secondButtonText.text = quizData.secondOption;
            var thirdButtonText = optionButtons[2].GetComponentInChildren<TMP_Text>();
            thirdButtonText.text = quizData.thirdOption;
        }
        //OX게임
        else if (quizData.type == 1)
        {
            threeOptionButtons.SetActive(false);
            oxButtons.SetActive(true);
        }
        
        this.onCompleted = onCompleted;
        
        //Incorrect Back Panel
        heartCountText.text = GameManager.Instance.heartCount.ToString();
        
    }

    /// <summary>
    /// 퀴즈의 정답을 선택하기 위한 버튼
    /// </summary>
    /// <param name="buttonIndex"></param>
    public void OnClickOptionButton(int buttonIndex)
    {
        //Timer 일시 정지
        timer.PauseTimer();
        
        if (buttonIndex == _answer)
        {
            //TODO: 정답 연출
            Debug.Log("정답");
            
            SetQuizCardPanelActive(QuizCardPanelType.CorrectBackPanel);
        }
        else
        {
            //TODO: 오답 연출
            Debug.Log("오답");
            
            SetQuizCardPanelActive(QuizCardPanelType.IncorrectBackPanel);
        }
    }

    
    private void SetQuizCardPanelActive(QuizCardPanelType quizCardPanelType)
    {
        switch (quizCardPanelType)
        {
            case QuizCardPanelType.Front:
                frontPanel.SetActive(true);
                correctBackPanel.SetActive(false);
                incorrectBackPanel.SetActive(false);
                
                correctBackPanel.GetComponent<RectTransform>().anchoredPosition = _correctBackPanelPosition;
                incorrectBackPanel.GetComponent<RectTransform>().anchoredPosition = _incorrectBackPanelPosition;
                break;
            case QuizCardPanelType.CorrectBackPanel:
                frontPanel.SetActive(false);
                correctBackPanel.SetActive(true);
                incorrectBackPanel.SetActive(false);
                
                correctBackPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                incorrectBackPanel.GetComponent<RectTransform>().anchoredPosition = _incorrectBackPanelPosition;
                break;
            
            case QuizCardPanelType.IncorrectBackPanel:
                frontPanel.SetActive(false);
                correctBackPanel.SetActive(false);
                incorrectBackPanel.SetActive(true);
                
                correctBackPanel.GetComponent<RectTransform>().anchoredPosition = _correctBackPanelPosition;
                incorrectBackPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                break;
        }
    }
    
    public void OnClickExitButton()
    {
        
    }

    #region Correct Back Panel
    /// <summary>
    /// 다음 버튼 이벤트  
    /// </summary>
    public void OnClickNextQuizButton()
    {
        onCompleted?.Invoke(_quizCardIndex);
    }

    #endregion
    
    #region Incorrect Back Panel
    /// <summary>
    /// 다시도전 버튼 이벤트
    /// </summary>
    public void OnClickRetryQuizButton()
    {
        //여분의 하트가 있다면
        if (GameManager.Instance.heartCount > 0)
        {
            
                GameManager.Instance.heartCount--;
                heartPanel.RemoveHeart();
            DOVirtual.DelayedCall(1.5f, () =>
            {
                heartCountText.text = GameManager.Instance.heartCount.ToString();
                SetQuizCardPanelActive(QuizCardPanelType.Front);
            
                //타이머 초기화 및 다시시작
                timer.InitTimer();
                timer.StartTimer();
            });
        }
        //하트가 모자라서 재도전 불가
        else
        {
            heartPanel.EmptyHeart();
        }
        
    }

    public void OnClickAddHeartButton()
    {
        heartPanel.AddHeart(GameManager.Instance.heartCount);
    }
    
    #endregion
}