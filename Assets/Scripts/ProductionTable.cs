using UnityEngine;

public class ProductionTable : Table
{
    [SerializeField]
    private FoodType productionFoodType;

    private void Start()
    {
        _tableType = 2;
    }

    public override FoodType interAct(FoodType givenFood)
    {
        if (givenFood == FoodType.None)
        {
            print("음식 생성");
            return productionFoodType;
        }
        else
        {
            return FoodType.None;
        }
    }
}
