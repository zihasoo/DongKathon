
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
        
    }
}

public enum FoodUISize
{
    S= 100, M = 130, L = 170
}