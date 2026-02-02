using UnityEngine;

[System.Serializable]
public class Order
{
    public CustomerSO customer;
    public RecipeSO recipe;
    public float timeRemaining; // tiempo hasta que se vaya
    public bool taken; // si ya se le ha tomado el pedido
}
