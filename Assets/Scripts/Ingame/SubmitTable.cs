using System.Collections;
using UnityEngine;

public class SubmitTable : Table
{
    public override bool putDownItem(ItemType givenFood)
    {
        if (((int)givenFood & isFinished) != 0)
        {
            UIManager.instance.CompleteOrder(GameManager.instance.standardItem);
        }
        return true;
    }

    public override ItemType takeItem()
    {
        return ItemType.None;
    }
}
