using UnityEngine;
using UnityEngine.Events;

public class Client : MonoBehaviour, IClient
{
    public static event UnityAction<Products> OnEnteredZone;
    public static event UnityAction<Products> OnExitZone;

    [SerializeField] public Products products;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "buyStance")
        {
            OnEnteredZone?.Invoke(products);
            Debug.Log("Entre en $gameObject.name");
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "buyStance")
        {
            OnExitZone?.Invoke(products);
            Debug.Log("Salí de $gameObject.name");
        }
    }

    public void Buy()
    {


        throw new System.NotImplementedException();
    }

    public void Chat()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }
}
