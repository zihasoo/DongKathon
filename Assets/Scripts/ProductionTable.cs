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
        print("���� ����");
        return productionFoodType;
    }
}
