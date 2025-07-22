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
        //���̺� ������ ���� ��
        {
            if (givenFood != FoodType.None)
            {
                print("���� ��������");
                currentFood = givenFood;
                var foodObject = Resources.Load<GameObject>($"Prefabs/{Table.foodTypeNameMap[currentFood]}");
                Instantiate(foodObject, transform);
            }
            return FoodType.None;
        }
        else if (givenFood != FoodType.None)
        //���̺��� ������ �ְ� ĳ���Ϳ��� ������ ���� �� (���� ��ġ��)
        {
            print("���� ������");
            return FoodType.None;
        }
        else
        //���̺� ������ ������ ĳ���Ϳ� ������ ���� �� (���� ���)
        {
            print("���� �����");
            Destroy(transform.GetChild(0).gameObject);
            var ret = currentFood;
            currentFood = FoodType.None;
            return ret;
        }
    }
}
