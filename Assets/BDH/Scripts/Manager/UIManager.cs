using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public List<FoodOrderedUI> curOrdered = new List<FoodOrderedUI>();
    public List<FoodOrderedUI> completeOrdered = new List<FoodOrderedUI>();

    public float padding = 0f;

    public GameObject foodOrderedObj;

    public Transform foodOrderOffPivot;
    public Transform foodOrderOnPivot;


    public Vector2 foodOrderedFirstPov;



    void Start()
    {
        CreateOrdered();
    }

    void CreateOrdered()
    {
        for(int i = 0; i < GameManager.instance.MaxOrderedCount; i++)
        {
            GameObject tmpOrdered = Instantiate(foodOrderedObj);
            FoodOrderedUI tmpfoodOdered = tmpOrdered.GetComponent<FoodOrderedUI>();
            
            tmpfoodOdered.foodLogo.sprite = null;

            for(int i1 = 0; i1 < tmpfoodOdered.meterialArr.Length; i1++)
            {
                tmpfoodOdered.meterialArr[i1].sprite = null;
            }

            completeOrdered.Add(tmpfoodOdered);

            tmpOrdered.transform.SetParent(foodOrderOffPivot);
            tmpOrdered.transform.localPosition = Vector3.zero;
            tmpOrdered.SetActive(false);
        }
    }

    public void GetOrder(Item item)
    {
        if(GameManager.instance.CurOrderedCount >= GameManager.instance.MaxOrderedCount)
        {
            Debug.Log("Ordered Pull");
            return;
        }

        if(item == null)
        {
            Debug.Log("NULL DATA");
            return;
        }

        FoodOrderedUI tmpOrdered = completeOrdered[0];
        GameObject tmpOderedObj = tmpOrdered.gameObject;
        RectTransform tmpRect = tmpOderedObj.GetComponent<RectTransform>();

        tmpOrdered.itemName = item.itemName;
        tmpOderedObj.transform.SetParent(foodOrderOnPivot);
        tmpOderedObj.SetActive(true);
        tmpRect.localScale = Vector3.one;
        tmpRect.sizeDelta = new Vector2((float)item.uiSize, 100.0f);
        

        //주문이 있다면
        if (curOrdered.Count > 0)
        {
            int index = GameManager.instance.CurOrderedCount -1;

            //전에 음식주문 UI에 정보(위치 사이즈)를 가져온다
            GameObject orderedObj = curOrdered[GameManager.instance.CurOrderedCount-1].gameObject;
            RectTransform orderedRTr = orderedObj.GetComponent<RectTransform>();
            Vector3 prevPos = orderedRTr.localPosition;
            float sizeDeltaX = orderedRTr.sizeDelta.x;

            //전에 음식주문 UI정보(위치 사이즈)를 기반으로 다음 음식주문 UI에 x을 정한다 y, z는 동일한 값이다
            tmpRect.localPosition = new Vector3(prevPos.x + padding + sizeDeltaX, foodOrderedFirstPov.y, 0.0f);
        }
        else // 주문이 없으면
        {
            // 최조 위치시킨다
            tmpRect.localPosition = new Vector3(foodOrderedFirstPov.x, foodOrderedFirstPov.y, 0.0f);
            
            //tmpRect.localPosition = new Vector3(foodOrderedFirstPov.x, foodOrderedFirstPov.y, 0.0f);
        }

        //받아야할 주문 리스트 저장 완료한 리스트 제거
        curOrdered.Add(tmpOrdered);
        completeOrdered.Remove(completeOrdered[0]);
        GameManager.instance.CurOrderedCount++;


        //음식로고 및 재료로고 예외처리
        if (tmpOrdered.foodLogo == null || tmpOrdered.meterialArr.Length <= 0)
        {
            Debug.Log("Image Data NULL");
            return;
        }

        //음식로고 및 재료로고 이미지 넣기
        tmpOrdered.foodLogo.sprite = item.foodLogo;
        for (int i = 0; i < tmpOrdered.meterialArr.Length; i++)
        {
            tmpOrdered.meterialArr[i].sprite = item.foodMeterial[i];
        }

        Debug.Log(item.itemName);

        //tmpOderedObj.transform.localPosition =
    }


    public void CompleteOrder(Item item)
    {
        if (curOrdered.Count <= 0 || item == null)
        {
            Debug.Log("주문이 없습니다");
            return;
        }

        int index = curOrdered.FindIndex(o => o.itemName == item.itemName);

        Debug.Log(index);
        
        FoodOrderedUI tmpFoodOrderUI = curOrdered[index];

        GameObject tmpUiObj = tmpFoodOrderUI.gameObject;
        RectTransform tmpRectTr = tmpUiObj.GetComponent<RectTransform>();
        Vector3 pos = tmpRectTr.localPosition;

        tmpUiObj.SetActive(false);
        tmpUiObj.transform.SetParent(foodOrderOffPivot);
        tmpRectTr.localPosition = Vector3.zero;

        tmpRectTr.sizeDelta = new Vector2(100f, 100f);

        curOrdered.Remove(tmpFoodOrderUI);
        completeOrdered.Add(tmpFoodOrderUI);

        GameManager.instance.CurOrderedCount--;
    }
}




