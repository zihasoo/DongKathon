using System.Collections;
using UnityEngine;

public class SubmitTable : Table
{
    public override bool putDownItem(ItemType givenFood)
    {
        //**********���� �ý��۰� �����ϱ�*************
        print("���� �����");
        return true;
    }

    public override ItemType takeItem()
    {
        return ItemType.None;
    }
}
