
using UnityEngine;
using UnityEngine.UI;

public class FoodOrderedUI : MonoBehaviour
{
    [SerializeField] GameObject[] foodOrderUIObj; 

    public Image foodLogo;

    public Image[] meterialArr;

    public Image meterialBackgroundImg;

    public string itemName;

    private void Start()
    {
        //if (foodLogo == null || meterialArr.Length <= 0)
        //{
        //    foodLogo = foodOrderUIObj[0].GetComponent<Image>();

        //    for (int i = 1; i < foodOrderUIObj.Length; i++)
        //    {
        //        meterialArr[i] = foodOrderUIObj[i].GetComponent<Image>();
        //    }
        //}
    }
}

public enum FoodUISize
{
    S= 100, M = 130, L = 170
}