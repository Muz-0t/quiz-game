using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PopupPanelController))]
public class StagePopupPanelController : MonoBehaviour
{
    [SerializeField] private GameObject stageCellPrefab;
    [SerializeField] private Transform contentTransform;
    private void Start()
    {
        GetComponent<PopupPanelController>().SetTitleText("STAGE");

        var lastStageIndex = 90;
        var maxStageCount = 100;
        
        //Stage Cell 만들기
        for (int i = 0; i < maxStageCount; i++)
        {
            GameObject stageCellObject = Instantiate(stageCellPrefab, contentTransform);
            StageCellButton stageCellButton = stageCellObject.GetComponent<StageCellButton>();
            //stageCellButton.SetStageCell(i, StageCellButton.StageCellType.Normal);

            if (i < lastStageIndex)
            {
                stageCellButton.SetStageCell(i, StageCellButton.StageCellType.Clear);
            }
            else if (i == lastStageIndex)
            {
                stageCellButton.SetStageCell(i, StageCellButton.StageCellType.Normal);
            }
            else 
            {
                stageCellButton.SetStageCell(i, StageCellButton.StageCellType.Lock);
            }
        }
        
        StageYPos(lastStageIndex);
        
    }

    private void StageYPos(int lastStageIndex)
    {
        int cellSpacing = 340;
        
        int yPos = cellSpacing * (lastStageIndex/3);
        
        contentTransform.position = new Vector2(0, yPos);
        
        Debug.Log(yPos);
    }
}
