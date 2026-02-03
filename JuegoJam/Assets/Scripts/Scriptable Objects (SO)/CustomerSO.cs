using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Customer", menuName = "Cooking/Customer")]
public class CustomerSO : ScriptableObject
{
    public string customerName;
    public Sprite sprite;
    public List<RecipeSO> possibleRecipes;
    public float timeToOrder = 10f; // Tiempo que dura la orden del cliente hasta tomarla o que se vaya
    public float patienceTime = 40f; // Tiempo de espera para el plato que ha pedido
}
