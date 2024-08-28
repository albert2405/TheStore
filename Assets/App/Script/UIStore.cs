using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStore : MonoBehaviour
{
    public static event UnityAction<Products> BuyFunction;
    public static event UnityAction<string, Transform, GameObject> InventoryFunction;

    [SerializeField] protected GameObject uiBuy;
    [SerializeField] protected GameObject uiInventory;
    [SerializeField] protected TMP_Text textChat;
    [SerializeField] protected Button buyButton;

    [Header("Inventory")]
    [SerializeField] protected Button inventoryButton;
    [SerializeField] protected GameObject inventoryItem;
    [SerializeField] protected Transform inventoryTransform;
    [SerializeField] protected Button closeInventory;

    protected Products products;

    void Start()
    {
        Client.OnEnteredZone += Show;
        Client.OnExitZone += Hide;

        buyButton.onClick.AddListener(BuyButton);
        inventoryButton.onClick.AddListener(InventoryButton);
        closeInventory.onClick.AddListener(CloseInventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Show(Products products)
    {
        uiBuy.SetActive(true);
        this.products = products;
        textChat.text = "Hola, deseo comprar "+ products.name;
    }

    protected void Hide(Products products)
    {
        uiBuy.SetActive(false);

    }

    protected void BuyButton()
    {
        BuyFunction?.Invoke(products);
    }

    protected void InventoryButton() 
    {
        uiInventory.SetActive(true);
        inventoryButton.gameObject.SetActive(false);
        InventoryFunction?.Invoke("new", inventoryTransform, inventoryItem);
    }

    protected void CloseInventory()
    {
        foreach (Transform child in inventoryTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
        inventoryButton.gameObject.SetActive(true);
        uiInventory.SetActive(false);
    }

}
