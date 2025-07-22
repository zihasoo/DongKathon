using UnityEngine;

public class DefaultTable : Table
{
    private FoodType currentFood = FoodType.None;

    private void Start()
    {
        _tableType = 1;
    }

    public override FoodType interAct(FoodType givenFood)
    {
        if (currentFood == FoodType.None)
        //테이블에 음식이 없을 때
        {
            if (givenFood != FoodType.None)
            {
                print("음식 내려놓음");
                currentFood = givenFood;
                var foodObject = Resources.Load<GameObject>($"Prefabs/{Table.foodTypeNameMap[currentFood]}");
                Instantiate(foodObject, transform);
            }
            return FoodType.None;
        }
        else if (givenFood != FoodType.None)
        //테이블에도 음식이 있고 캐릭터에도 음식이 있을 때 (음식 합치기)
        {
            print("음식 합쳐짐");
            return FoodType.None;
        }
        else
        //테이블에 음식이 있지만 캐릭터에 음식이 없을 때 (음식 들기)
        {
            print("음식 들어짐");
            Destroy(transform.GetChild(0).gameObject);
            var ret = currentFood;
            currentFood = FoodType.None;
            return ret;
        }
    }
}
