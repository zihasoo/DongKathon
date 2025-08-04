using System.Collections;
using UnityEngine;

public class SubmitTable : Table
{
    private static int score = 0;
    public override bool putDownItem(ItemType givenFood)
    {
        //**********���� �ý��۰� �����ϱ�*************
        if (((int)givenFood & isFinished) != 0)
        {
            score += 5;
            print("����: " + score);
        }
        return true;
    }

    public override ItemType takeItem()
    {
        return ItemType.None;
    }
}
