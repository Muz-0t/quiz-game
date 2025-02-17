using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HeartPanelController : MonoBehaviour
{
    private int _heartCount;
    [SerializeField]private TMP_Text heartCountText;
    [SerializeField] private GameObject heartRemoveImageObject;
    
    //하트 추가 연출
    //하트 감소 연출
    //하트 부족 연출

    private void Start()
    {
        heartRemoveImageObject.SetActive(false);
        
        InitHeartCount(10);
        
    }

    /// <summary>
    /// 하트 패널에 하트 수 초기화
    /// </summary>
    /// <param name="heartCount">하트 수</param>
    public void InitHeartCount(int heartCount)
    {
        _heartCount = heartCount;
        heartCountText.text = _heartCount.ToString();
    }

    private void ChangeTextAnimation(bool isAdd)
    {
        float duration = 0.2f;
        float yPos = 40f;
        
        heartCountText.rectTransform.DOAnchorPosY(-yPos, duration);
        heartCountText.DOFade(0f, duration).OnComplete(() =>
        {
            if (isAdd)
            {
                var currentHeartText = heartCountText.text;
                heartCountText.text = (int.Parse(currentHeartText) + 1).ToString();
            }
            else
            {
                var currentHeartText = heartCountText.text;
                heartCountText.text = (int.Parse(currentHeartText) - 1).ToString();
            }
            
            //Heart Panel의 Width를 글자 수에 따라 변경하는 코드
            var textLength = heartCountText.text.Length;
            GetComponent<RectTransform>().sizeDelta = new Vector2(100 + textLength * 30f, 100f);
            
            heartCountText.rectTransform.DOAnchorPosY(yPos, 0);
            heartCountText.rectTransform.DOAnchorPosY(0, duration);
            heartCountText.DOFade(1, duration).OnComplete(() =>
            {
                
            });
        });
    }

    public void AddHeart(int heartCount)
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < 3; i++)
        {
            sequence.AppendCallback(() =>
            {
                ChangeTextAnimation(true);
                //TODO: 효과음 재생
            });
            sequence.AppendInterval(0.5f);
        }
    }

    public void EmptyHeart()
    {
        GetComponent<RectTransform>().DOPunchPosition(new Vector3(20f,0f,0f), 1f,7);
    }

    public void RemoveHeart()
    {
        //하트 사라지는 연출
        heartRemoveImageObject.SetActive(true);
        heartRemoveImageObject.transform.localScale = Vector3.zero;
        heartRemoveImageObject.GetComponent<Image>().color = Color.white;
        
        heartRemoveImageObject.transform.DOScale(3f, 1f);
        heartRemoveImageObject.GetComponent<Image>().DOFade(0f, 1f);
        
        //하트 텍스트가 감소하는 연출
        DOVirtual.DelayedCall(1.5f, () =>
        {
            heartCountText.rectTransform.DOAnchorPosY(-40f, 0.5f);
            heartCountText.DOFade(0f, 0.5f).OnComplete(() =>
            {
                //하트 개수 감소
                _heartCount--;
                heartCountText.text = _heartCount.ToString();
                
                var textLength = heartCountText.text.Length;
                GetComponent<RectTransform>().sizeDelta = new Vector2(100 + textLength * 30f, 100f);

                heartCountText.rectTransform.DOAnchorPosY(40f, 0f);
                heartCountText.rectTransform.DOAnchorPosY(0, 0.5f);
                heartCountText.DOFade(1f, 0.5f);
            });
        });
        
        
        
    }
}
