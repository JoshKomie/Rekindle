using System.Collections;
using System.Collections.Generic;

using SuperTiled2Unity;

using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    private List<GameObject> items;
    public Text inventoryText;
    public string AsString() {
        string s = "";
        foreach (var item in items) {
            s += item.GetComponent<Item>().name + "\n";
        }
        return s;
    }
    public void Start() {
        items = new List<GameObject>();
        inventoryText = GameObject.Find("InventoryText").GetComponent<Text>();
    }

    public void RemoveItem(string itemName)
    {
      GameObject obj = GetItemWithName(itemName);
      if (obj)
      {
        RemoveItem(obj);
      }
      inventoryText.text = AsString();
    }

    public void RemoveItem(GameObject item)
    {
      this.items.Remove(item);
      inventoryText.text = AsString();
    }

  public void AddItem(GameObject item) {
      items.Add(item);
      inventoryText.text = AsString();
  }

  private GameObject GetItemWithName(string name)
  {
    foreach (var item in items)
    {
      if (item.GetComponent<Item>().name.ToLower() == name.ToLower())
      {
        return item;
      }
    }

    return null;
  }

    public bool HasItem(string name) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i].GetComponent<Item>().name == name) {
                return true;
            }
        }
        return false;
    }
}
