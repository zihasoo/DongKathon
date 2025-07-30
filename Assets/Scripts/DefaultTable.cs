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
        //���̺� ������ ���� ��
        {
            currentItem = givenFood;
            var obj = Resources.Load<GameObject>($"Prefabs/{Table.itemTypeNameMap[currentItem]}");
            Instantiate(obj, transform);
            return true;
        }
        else
        //������ ���� �� (���� ��ġ��) **************�����ؾߵ�****************
        {
            print("���� ������");
            
            return false; //������ �������� true �ƴϸ� false
        }
    }
}
