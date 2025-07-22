using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float selectedColorAlpha;
    private const float unSelectedColorAlpha = 1.0f;
    public Transform triggerTransform;

    private bool touch = false;
    private Collider currentSelected = null;
    private HashSet<Collider> collidedObjects = new HashSet<Collider>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnInteraction()
    {

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
            unSelect(currentSelected);
            select(closest);
            currentSelected = closest;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Food")) return;

        collidedObjects.Add(other);
        if (!touch)
        {
            select(other);
            currentSelected = other;
            touch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentSelected == other)
        {
            unSelect(currentSelected);
            touch = false;
        }
        collidedObjects.Remove(other);
    }

    private void select(Collider c) => setAlpha(c, selectedColorAlpha);

    private void unSelect(Collider c) => setAlpha(c, unSelectedColorAlpha);

    private void setAlpha(Collider target, float alpha)
    {
        Color otherColor = target.GetComponent<MeshRenderer>().material.color;
        target.GetComponent<MeshRenderer>().material.color = new Color(otherColor.r, otherColor.g, otherColor.b, alpha);
    }
}
