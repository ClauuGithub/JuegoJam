using System.Collections.Generic;
using UnityEngine;

// GESTIÓN DE LOS CLIENTES EN EL MOSTRADOR
public class OrderManager : MonoBehaviour
{
    public List<Order> activeOrders = new List<Order>();
    public int maxOrders = 3; // número máximo de clientes

    public void SpawnCustomer(CustomerSO customer)
    {
        if (activeOrders.Count >= maxOrders) return;

        // Se elige una receta al azar de las que se puede elegir
        RecipeSO recipe = customer.possibleRecipes[
            Random.Range(0, customer.possibleRecipes.Count)
        ];

        // Creamos una orden
        Order order = new Order
        {
            customer = customer,
            recipe = recipe,
            timeRemaining = customer.timeToOrder,
            taken = false
        };

        // Y lo añadimos a la lista de órdenes
        activeOrders.Add(order);
        UIManager.Instance.UpdateOrdersUI(activeOrders);
    }

    public void TakeOrder(Order order)
    {
        order.taken = true;
        order.timeRemaining = order.customer.patienceTime; // El tiempo que le quede será el del cliente (para tomar en cuenta los clientes especiales)
    }

    public void DeliverDish(RecipeSO cookedRecipe)
    {
        // Buscamos si en los pedidos activos está la receta que acabamos de preparar
        Order order = activeOrders.Find(o => o.recipe == cookedRecipe && o.taken);

        if (order != null)
        {
            activeOrders.Remove(order);
            Debug.Log("Cliente servido correctamente"); // PRUEBAS
        }
        else
        {
            Debug.Log("Ese plato no era para nadie"); // PRUEBAS
        }

        // Se actualizan las órdenes activas en la UI
        UIManager.Instance.UpdateOrdersUI(activeOrders);
    }


    void Update()
    {
        for (int i = activeOrders.Count - 1; i >= 0; i--) // Para todas las órdenes activas
        {
            activeOrders[i].timeRemaining -= Time.deltaTime; // Disminuimos el tiempo en cada frame

            if (activeOrders[i].timeRemaining <= 0) // Si llegamos a 0
            {
                Debug.Log("Cliente se fue enfadado");
                activeOrders.RemoveAt(i); // El cliente se va y retiramos su orden de las órdenes activas tanto en script...
                UIManager.Instance.UpdateOrdersUI(activeOrders); // ... como en UI
            }
        }
    }
}
