using UnityEngine;
using System;

public class StoreManager : MonoBehaviour
{
    [SerializeField] protected InventoryManager inventoryManager;

    [SerializeField] protected string nameProduct;
    [SerializeField] protected int quantitySold;

    [SerializeField] public Products products;


    void Start()
    {
        inventoryManager.LoadProducts();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            inventoryManager.AddOrUpdateProduct(products, products.state);
        }
    }
}
