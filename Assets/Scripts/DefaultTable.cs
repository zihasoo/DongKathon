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
        //���̺� ������ ���� ��
        {
            currentItem = givenFood;
            var obj = Resources.Load<GameObject>($"Prefabs/{Table.itemTypeNameMap[currentItem]}");
            Instantiate(obj, pool);
            return true;
        }
        else
        //������ ���� �� (���� ��ġ��) 
        {
            if (((int)givenFood & isCutOff) != 0 && ((int)currentItem & isCutOff) != 0 && currentItem != givenFood)
                //���̺� �ִ� ���İ� �÷��̾ ������������ ������ ��� �߸� �����̰� ���� ������ �ƴ� ��
            {
                Destroy(pool.GetChild(0).gameObject); //���� �ִ� ���� ����
                currentItem = ItemType.FinishedDish; //������(�ϼ���) �������� �ٲٱ�
                var obj = Resources.Load<GameObject>($"Prefabs/{Table.itemTypeNameMap[currentItem]}");
                Instantiate(obj, pool);
                return true;
            }
            return false;
        }
    }
}
