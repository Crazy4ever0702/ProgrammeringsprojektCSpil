using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Tooltip("Holder spillerens items som tekst IDs")]
    public List<string> items = new List<string>();

    [Header("UI")]
    [Tooltip("Parent til inventory slots (fx et Horizontal Layout Group)")]
    public Transform uiParent;
    [Tooltip("Prefab for et inventory-slot (UI Button/Text)")]
    public GameObject slotPrefab;

    public void AddItem(string itemId)
    {
        items.Add(itemId);
        RefreshUI();
    }

    public bool HasItem(string itemId)
    {
        return items.Contains(itemId);
    }

    public void RefreshUI()
    {
        foreach (Transform child in uiParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (string id in items)
        {
            var slot = GameObject.Instantiate(slotPrefab, uiParent);
            Text txt = slot.GetComponentInChildren<Text>();
            if (txt != null)
                txt.text = id;
        }
    }
}
