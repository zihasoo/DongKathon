
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FoodOrderedUI : MonoBehaviour
{
    [SerializeField] GameObject[] foodOrderUIObj; 

    public Image foodLogo;

    public Image[] meterialArr;

    public Image meterialBackgroundImg;

    public string itemName;

    public Image cookingTimeGauge;

    public float curCookingTime = 0.0f;

    private IEnumerator CookingTimeRoutine;

    public void StartCookingRoutine(Item item)
    {
        CookingTimeRoutine = CookingTime(item);
        StartCoroutine(CookingTimeRoutine);
    }

    public void StopCookingRoutine()
    {
        StopCoroutine(CookingTimeRoutine);
    }

    private IEnumerator CookingTime(Item item)
    {
        curCookingTime = item.cookingTime;

        while(true)
        {
            if(curCookingTime <= 0f)
            {
                Debug.Log("Cooking Time Over");
                UIManager.instance.CompleteOrder(item, false);
                break;
            }

            curCookingTime -= 0.01f;
            cookingTimeGauge.fillAmount = (curCookingTime / item.cookingTime);

            yield return new WaitForSeconds(0.01f);
        }
    }
}
