using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform triggerTransform;

    private bool touch = false;
    private FoodType carryingFoodType = FoodType.None;
    private Collider currentSelected = null;
    private HashSet<Collider> collidedObjects = new HashSet<Collider>();

    void Start()
    {
        OnInteraction();
    }

    void Update()
    {
    }

    void OnInteraction()
    {
        if (touch) {
            touch = false;
            collidedObjects.Clear();
            var table = currentSelected.gameObject.GetComponent<Table>();
            table.unSelect();

            var food = table.interAct(carryingFoodType);

            if (food == FoodType.None)
            //음식을 내려놨을 때
            {
                if (carryingFoodType != FoodType.None)
                {
                    Destroy(transform.GetChild(1).gameObject);
                }
            }
            else
            //음식을 들었을 때
            {
                var foodObj = Resources.Load<GameObject>($"Prefabs/{Table.foodTypeNameMap[food]}");
                var obj = Instantiate(foodObj, transform);
                obj.transform.localPosition = new Vector3(0, 0.5f, 0.8f);
            }
            carryingFoodType = food;
            //currentSelected.transform.SetParent(triggerTransform);
            //currentSelected.transform.localPosition = Vector3.zero;
            //Destroy(currentSelected.gameObject);
        }
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
            touch = false;
        }
        collidedObjects.Remove(other);
    }
}
