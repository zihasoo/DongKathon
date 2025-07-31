using System.Collections;
using UnityEngine;

public class SubmitTable : Table
{
    private static int score = 0;
    public override bool putDownItem(ItemType givenFood)
    {
        //**********점수 시스템과 연결하기*************
        if (((int)givenFood & isFinished) != 0)
        {
            score += 5;
            print("점수: " + score);
        }
        return true;
    }

    public override ItemType takeItem()
    {
        return ItemType.None;
    }
}
