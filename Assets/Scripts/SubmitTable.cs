using System.Collections;
using UnityEngine;

public class SubmitTable : Table
{
    public override bool putDownItem(ItemType givenFood)
    {
        //**********점수 시스템과 연결하기*************
        print("음식 제출됨");
        return true;
    }

    public override ItemType takeItem()
    {
        return ItemType.None;
    }
}
