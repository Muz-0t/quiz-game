using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class LogoController : MonoBehaviour
{
    //Image의 위치정보가 필요하기때문에 RectTansform으로 할당
    [SerializeField] private RectTransform topLogoRectTransform;
    [SerializeField] private RectTransform middleLogoRectTransform;
    [SerializeField] private RectTransform bottomLogoRectTransform;

    private void Start()
    {
        Show();
    }
    
    public void Show()
    {
        //3장의 로고 이미지의 위치를 변경해서 로고가 입체적으로 보이게 하는 애니메이션 실행
        topLogoRectTransform.DOAnchorPosY(15f, 2f);
        bottomLogoRectTransform.DOAnchorPosY(-15f, 2f);
    }
}
