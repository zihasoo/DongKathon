using UnityEngine;

public class DefaultTable : Table
{
    [SerializeField]
    protected Transform pool;
    protected ItemType currentItem = ItemType.None;

    public override ItemType takeItem()
    {
        if (currentItem == ItemType.None)
            return ItemType.None;

        Destroy(pool.GetChild(0).gameObject);
        var ret = currentItem;
        currentItem = ItemType.None;
        return ret;
    }

    public override bool putDownItem(ItemType givenFood)
    {
        if (currentItem == ItemType.None)
        //테이블에 음식이 없을 때
        {
            currentItem = givenFood;
            var obj = Resources.Load<GameObject>($"Prefabs/{Table.itemTypeNameMap[currentItem]}");
            Instantiate(obj, pool);
            return true;
        }
        else
        //음식이 있을 때 (음식 합치기) 
        {
            if (((int)givenFood & isCutOff) != 0 && ((int)currentItem & isCutOff) != 0 && currentItem != givenFood)
                //테이블에 있는 음식과 플레이어가 내려놓으려는 음식이 모두 잘린 음식이고 같은 음식이 아닐 때
            {
                Destroy(pool.GetChild(0).gameObject); //원래 있던 음식 제거
                currentItem = ItemType.FinishedDish; //합쳐진(완성된) 음식으로 바꾸기
                var obj = Resources.Load<GameObject>($"Prefabs/{Table.itemTypeNameMap[currentItem]}");
                Instantiate(obj, pool);
                return true;
            }
            return false;
        }
    }
}
