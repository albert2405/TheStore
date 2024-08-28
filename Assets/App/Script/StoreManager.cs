using UnityEngine;
using System;

public class StoreManager : MonoBehaviour
{
    [SerializeField] protected InventoryManager inventoryManager;

    [SerializeField] public Products products;

    void Start()
    {
        Client.OnEnteredZone += ReceiveClient;
        Client.OnExitZone += RetireClient;
        UIStore.BuyFunction += SoldItem;
        UIStore.InventoryFunction += GetItemInventory;

        inventoryManager.LoadProducts();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            inventoryManager.AddOrUpdateProduct(products, products.state);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            inventoryManager.SubtractProduct(products);
        }
    }

    private void ReceiveClient(Products products)
    {
        Debug.Log("Pedido recibido: " + products.name);
        // Aquí puedes añadir la lógica que necesitas para procesar la variable A
    }

    protected void RetireClient(Products products)
    {
        Debug.Log("Pedido cancelado: " + products.name);
    }

    protected void SoldItem(Products products)
    {
        inventoryManager.SubtractProduct(products);
    }

    protected void GetItemInventory(string stateType, Transform spawn, GameObject inventoryItem)
    {
        inventoryManager.GetProductByState(stateType, spawn, inventoryItem);
    }
}
