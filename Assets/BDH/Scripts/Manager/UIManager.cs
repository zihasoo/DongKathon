using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public List<FoodOrderedUI> curOrdered = new List<FoodOrderedUI>();
    public List<FoodOrderedUI> completeOrdered = new List<FoodOrderedUI>();

    public GameObject foodOrderedObj;

    public Transform foodOrderOffPivot;
    public Transform foodOrderOnPivot;

    public Vector2 foodOrderedDefualtSize = new Vector2(100f, 100f);

    public Vector2 foodOrderedFirstPov;


    public TextMeshProUGUI scoreText;


    [Header("Game End UI")]
    // Game End UI;

    public GameObject endGameUI;

    public Image inGameTimerGauge;
    public TextMeshProUGUI inGameTimerText;

    public TextMeshProUGUI failedCountText;
    public TextMeshProUGUI finalCountText;
    public TextMeshProUGUI failedscoreText;
    public TextMeshProUGUI finalscoreText;

    public TextMeshProUGUI totalScoreText;

    public TextMeshProUGUI gameStageText;

    public Image[] scoreRatingImg;


    public float ratingGaugeTime;

    public void Initialized()
    {
        scoreText.text = "0";

        if (instance == null)
            instance = this;
    }

    void Start()
    {
        CreateOrdered();
    }

    void CreateOrdered()
    {
        for(int i = 0; i < GameManager.instance.maxOrderedCount; i++)
        {
            GameObject tmpOrdered = Instantiate(foodOrderedObj);
            FoodOrderedUI tmpfoodOdered = tmpOrdered.GetComponent<FoodOrderedUI>();
            
            tmpfoodOdered.foodLogo.sprite = null;

            for(int i1 = 0; i1 < tmpfoodOdered.meterialArr.Length; i1++)
            {
                tmpfoodOdered.meterialArr[i1].sprite = null;
                tmpfoodOdered.meterialArr[i1].transform.parent.gameObject.SetActive(false);
            }

            completeOrdered.Add(tmpfoodOdered);

            tmpOrdered.transform.SetParent(foodOrderOffPivot);
            tmpOrdered.transform.localPosition = Vector3.zero;
            tmpOrdered.SetActive(false);
        }
    }

    public void GetOrder(Item item)
    {
        if(GameManager.instance.curOrderedCount >= GameManager.instance.maxOrderedCount)
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
        #region ��ġ 
        //tmpRect.sizeDelta = new Vector2((float)item.uiSize, 100.0f);
        ////�ֹ��� �ִٸ�
        //if (curOrdered.Count > 0)
        //{
        //    int index = GameManager.instance.curOrderedCount -1;

        //    //���� �����ֹ� UI�� ����(��ġ ������)�� �����´�
        //    GameObject orderedObj = curOrdered[GameManager.instance.curOrderedCount-1].gameObject;
        //    RectTransform orderedRTr = orderedObj.GetComponent<RectTransform>();
        //    Vector3 prevPos = orderedRTr.localPosition;
        //    float sizeDeltaX = orderedRTr.sizeDelta.x;

        //    //���� �����ֹ� UI����(��ġ ������)�� ������� ���� �����ֹ� UI�� x�� ���Ѵ� y, z�� ������ ���̴�
        //    tmpRect.localPosition = new Vector3(prevPos.x + padding + sizeDeltaX, foodOrderedFirstPov.y, 0.0f);
        //}
        //else // �ֹ��� ������
        //{
        //    // ���� ��ġ��Ų��
        //    tmpRect.localPosition = new Vector3(foodOrderedFirstPov.x, foodOrderedFirstPov.y, 0.0f);

        //    //tmpRect.localPosition = new Vector3(foodOrderedFirstPov.x, foodOrderedFirstPov.y, 0.0f);
        //}

        #endregion

        //�޾ƾ��� �ֹ� ����Ʈ ���� �Ϸ��� ����Ʈ ����
        curOrdered.Add(tmpOrdered);
        completeOrdered.Remove(completeOrdered[0]);
        GameManager.instance.curOrderedCount++;


        //���ķΰ� �� ���ΰ� ����ó��
        if (tmpOrdered.foodLogo == null || tmpOrdered.meterialArr.Length <= 0)
        {
            Debug.Log("Image Data NULL");
            return;
        }

        //���ķΰ� �� ���ΰ� �̹��� �ֱ�
        tmpOrdered.foodLogo.sprite = item.foodLogo;
        for (int i = 0; i < item.foodMeterial.Length; i++)
        {
            tmpOrdered.meterialArr[i].sprite = item.foodMeterial[i];
            tmpOrdered.meterialArr[i].transform.parent.gameObject.SetActive(true);
        }

        Debug.Log(item.itemName);

        //tmpOderedObj.transform.localPosition =
        StartCoroutine(tmpOrdered.CookingTime(item));
    }


    public void CompleteOrder(Item item, bool isComplete = true)
    {
        if (curOrdered.Count <= 0 || item == null)
        {
            Debug.Log("�ֹ��� �����ϴ�");
            return;
        }

        int index = curOrdered.FindIndex(o => o.itemName == item.itemName);
        
        FoodOrderedUI tmpFoodOrderUI = curOrdered[index];
        GameObject tmpUiObj = tmpFoodOrderUI.gameObject;

        // ��Ȱ��ȭ �� off �̵�
        tmpUiObj.SetActive(false);
        tmpUiObj.transform.SetParent(foodOrderOffPivot);

        //�ʱ�ȭ
        tmpFoodOrderUI.itemName = "null";
        tmpFoodOrderUI.foodLogo.sprite = null;
        for(int i = 0; i < tmpFoodOrderUI.meterialArr.Length; i++)
        {
            tmpFoodOrderUI.meterialArr[i] = null;
        }
        tmpUiObj.GetComponent<RectTransform>().sizeDelta = new Vector2(foodOrderedDefualtSize.x, foodOrderedDefualtSize.y);

        curOrdered.Remove(tmpFoodOrderUI);
        completeOrdered.Add(tmpFoodOrderUI);

        if(isComplete)
        {
            AddScore(item.score);
            Debug.Log("Complete");
        }
        else
        {
            MinusScore(item.score / 2);
            Debug.Log("Fialed");
        }

        GameManager.instance.curOrderedCount--;
    }

    public void AddScore(int score)
    {
        GameManager.instance.inGameScore += score;
        GameManager.instance.completeCount++;
        scoreText.text = GameManager.instance.inGameScore.ToString();
        
    }

    public void MinusScore(int score)
    {
        GameManager.instance.failedScore += score;
        GameManager.instance.failedCount++;
    }
    
    
    public void StartGameEnd()
    {
        StartCoroutine("GameEnd");
    }

    IEnumerator GameEnd()
    {
        endGameUI.SetActive(true);
        scoreText.transform.parent.gameObject.SetActive(false);
        inGameTimerText.transform.parent.gameObject.SetActive(false);

        gameStageText.text = GameManager.instance.curStage.firstStage+"-"+GameManager.instance.curStage.secondStage;

        //���� ȸ�� �� ����
        finalCountText.text = "Complete Order x "+GameManager.instance.completeCount.ToString();
        finalscoreText.text = GameManager.instance.inGameScore.ToString();

        yield return new WaitForSeconds(0.1f);

        //���� ȸ�� �� ����
        finalCountText.text = "Filed Order x "+GameManager.instance.failedCount.ToString();
        failedscoreText.text = GameManager.instance.failedScore.ToString();

        yield return new WaitForSeconds(0.1f);

        int finalScore = GameManager.instance.inGameScore - GameManager.instance.failedScore;
        int ratingIndex = 0;

        Debug.Log(finalScore);
        totalScoreText.text = finalScore.ToString();

        // ���� ���
        for(int i = 0; i < 3; i++)
        {
            if (finalScore >= GameManager.instance.curStage.ratingScore[i])
            {
                Debug.Log("���� ����");
                ratingIndex++;
            }
        }
        
        StartCoroutine(RatingUIAnimation(ratingIndex));
    }

    IEnumerator RatingUIAnimation(int rating)
    {
        int index = 0;
        float time = ratingGaugeTime;
        while (true)
        {

            if(time <= 0)
            {
                Debug.Log("Rating Animation End"+index);
                scoreRatingImg[index].fillAmount = 1;
                time = ratingGaugeTime;
                index++;
            }
            else
            {
                if(time >= ratingGaugeTime)
                {
                    scoreRatingImg[index].gameObject.SetActive(true);
                }

                time -= 0.01f;

                scoreRatingImg[index].fillAmount = 1 - (time / ratingGaugeTime);
            }


            if (index >= rating)
            {
                Debug.Log("Your rating :" + index);
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }


}




