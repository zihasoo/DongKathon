using UnityEngine;

public class DefaultTable : Table
{
    protected ItemType currentItem = ItemType.None;

    public override ItemType takeItem()
    {
        if (currentItem == ItemType.None)
            return ItemType.None;

        Destroy(transform.GetChild(0).gameObject);
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
            Instantiate(obj, transform);
            return true;
        }
        else
        //음식이 있을 때 (음식 합치기) **************구현해야됨****************
        {
            print("음식 합쳐짐");
            
            return false; //음식이 합쳐지면 true 아니면 false
        }
    }
}
