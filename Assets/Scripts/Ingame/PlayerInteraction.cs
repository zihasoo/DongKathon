using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInteraction : MonoBehaviour
{
    public Transform triggerTransform;

    private bool touch = false;
    private ItemType carryingFoodType = ItemType.None;
    private Collider currentSelected = null;
    private HashSet<Collider> collidedObjects = new HashSet<Collider>();

    void OnLeftClick()
    {
        if (!touch) return;

        var table = currentSelected.gameObject.GetComponent<Table>();
        if (carryingFoodType == ItemType.None)
        //음식 들기
        {
            var takenFood = table.takeItem();
            if (takenFood != ItemType.None)
            {
                var foodObj = Resources.Load<GameObject>($"Prefabs/Ingame/{Table.itemTypeNameMap[takenFood]}");
                var obj = Instantiate(foodObj, transform);
                obj.transform.localPosition = new Vector3(0, 0.5f, 0.8f);
                carryingFoodType = takenFood;
            }
        }
        else
        //음식 내려놓기
        {
            var result = table.putDownItem(carryingFoodType);
            if (result)
            {
                Destroy(transform.GetChild(1).gameObject);
                carryingFoodType = ItemType.None;
            }
        }
    }

    void OnRightClick(InputValue input)
    {
        if (!touch) return;
        var table = currentSelected.GetComponent<CuttingTable>();
        if (table == null) return;
        if(input.isPressed)
            table.startCutting();
        else
            table.stopCutting();
    }

    private void OnTriggerStay(Collider other)
    {
        if (collidedObjects.Count <= 1) return;

        Collider closest = null;
        float minDistance = 9999999f;

        foreach (var col in collidedObjects)
        {
            float dist = Vector3.Distance(col.transform.position, triggerTransform.position);

            if (dist < minDistance)
            {
                minDistance = dist;
                closest = col;
            }
        }
        if (closest != currentSelected)
        {
            currentSelected.GetComponent<Table>().unSelect();
            currentSelected.GetComponent<CuttingTable>()?.stopCutting();

            closest.GetComponent<Table>().select();
            currentSelected = closest;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Selectable")) return;

        collidedObjects.Add(other);
        if (!touch)
        {
            other.GetComponent<Table>().select();
            currentSelected = other;
            touch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentSelected == other)
        {
            currentSelected.GetComponent<Table>().unSelect();
            currentSelected.GetComponent<CuttingTable>()?.stopCutting();
            touch = false;
        }
        collidedObjects.Remove(other);
    }
}
