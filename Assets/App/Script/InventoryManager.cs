using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    protected string path;
    protected RootObject inventory;

    void Awake()
    {

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        string folderPath = Path.Combine(documentsPath, "The Store");
        Directory.CreateDirectory(folderPath);

        path = Path.Combine(folderPath, "Resources/products.json");

        string directory = Path.GetDirectoryName(path);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        LoadProducts();
    }

    public void LoadProducts()
    {
        if (!File.Exists(path) || new FileInfo(path).Length == 0)
        {
            Debug.LogWarning("Archivo inventory.json no existe o est� vac�o. Creando archivo nuevo con datos por defecto.");
            inventory = new RootObject();
            SaveInventory();
        }
        else {
            Debug.Log("Archivo inventory.json existe. Cargando.");
            LoadInventory();
        }
    }

    public void AddOrUpdateProduct(Products product, string stateType)
    {
        Products productCache = new Products()
        {
            id = product.id,
            name = product.name,
            category = product.category,
            cost = product.cost,
            quantity = product.quantity,
            state = stateType
        };

        List<Products> stateList = GetStateList(stateType);

        Products existingProduct = stateList.Find(p=>p.name == productCache.name);

        if (existingProduct != null) {
            existingProduct.quantity += productCache.quantity;
            Debug.Log($"Cantidad de {productCache.name} actualizada a {existingProduct.quantity} en estado {stateType}.");
        }
        else
        {
            stateList.Add(productCache);
            Debug.Log($"Producto {productCache.name} agregado con cantidad {product.quantity} al estado {stateType}.");
        }

        SaveInventory();

    }

    public bool SubtractProduct(Products products)
    {
        List<Products> stateList = GetStateList(products.state);
        Products existingProduct = stateList.Find(p => p.name == products.name);

        if (existingProduct!=null && existingProduct.quantity>= products.quantity)
        {
            existingProduct.quantity -= products.quantity;
            Debug.Log($"Cantidad de {products.name} actualizada a {existingProduct.quantity} en estado {products.state}.");

            if (existingProduct.quantity == 0)
            {
                stateList.Remove(existingProduct);
                Debug.Log($"Producto {products.name} removido del estado {products.state} por falta de stock.");
            }

            SaveInventory();
            return true;
        }
        else
        {
            Debug.LogWarning($"No hay suficiente stock de {products.name} en estado {products.state}.");
            return false;
        }
    }

    public void GetProductByState(string stateType, Transform spawn, GameObject inventoryItem)
    {
        List<Products> stateList = GetStateList(stateType);
        Debug.Log($"Productos en estado {stateType}:");
        foreach (Products product in stateList)
        {
            GameObject instantiatedItem = Instantiate(inventoryItem, spawn);
            instantiatedItem.GetComponent<InventoryItem>().SetData(product);
            Debug.Log($"{product.name}: {product.quantity} unidades.");
        }
    }

    protected List<Products> GetStateList(string stateType)
    {
        return stateType switch
        {
            "new" => inventory.state.@new,
            "used" => inventory.state.used,
            "junk" => inventory.state.junk,
            _ => throw new System.ArgumentException("Estado inv�lido", nameof(stateType)),
        };
    }

    protected void SaveInventory()
    {
        string json = JsonUtility.ToJson(inventory, true);
        File.WriteAllText(path, json);
        Debug.Log("Inventario guardado exitosamente en inventory.json");
    }

    protected void LoadInventory()
    {
        string json = File.ReadAllText(path);
        inventory = JsonUtility.FromJson<RootObject>(json);
        Debug.Log("Inventario cargado exitosamente desde inventory.json");
    }
}
