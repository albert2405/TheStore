using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] protected TMP_Text nameItem;
    [SerializeField] protected TMP_Text countItem;
    [SerializeField] protected TMP_Text costItem;

    void Start()
    {
        
    }

    public void SetData(Products products)
    {
        nameItem.text = products.name;
        countItem.text = products.quantity.ToString();
        costItem.text = products.cost.ToString();
    }
}
