using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// GESTIÓN DE LOS CLIENTES EN EL MOSTRADOR
public class OrderManager : MonoBehaviour
{
    public List<Order> activeOrders = new List<Order>();
    public float spawnInterval = 8f; // cada cuánto tiempo aparece uno
    public int maxOrders = 3; // número máximo de clientes

    float spawnTimer;

    [Header("Clientes normales")]
    public List<CustomerSO> possibleCustomers; // tipos de clientes que pueden aparecer

    [Header("Clientes especiales")]
    public List<CustomerSO> specialCustomers;
    public int dishesPerSpecialCustomer = 5; // Cada cuántos platos aparece uno especial

    int dishesServedCount = 0;
    bool specialCustomerPending = false;

    List<CustomerSO> remainingSpecialCustomers = new List<CustomerSO>();

    void Start()
    {
        remainingSpecialCustomers = new List<CustomerSO>(specialCustomers);
    }
    public void SpawnCustomer(CustomerSO customer)
    {
        Debug.Log("Cliente spawneado"); // PRUEBAS
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
            dishesServedCount++;

            if (dishesServedCount % dishesPerSpecialCustomer == 0 &&
                remainingSpecialCustomers.Count > 0)
            {
                specialCustomerPending = true;
            }

            Debug.Log("Cliente servido correctamente");
        }
    }

    void TrySpawnCustomer()
    {
        if (activeOrders.Count >= maxOrders)
            return;

        CustomerSO customerToSpawn = null;

        // Si ahora toca spawnear un cliente especial
        if (specialCustomerPending && remainingSpecialCustomers.Count > 0)
        {
            int index = Random.Range(0, remainingSpecialCustomers.Count);
            customerToSpawn = remainingSpecialCustomers[index];

            remainingSpecialCustomers.RemoveAt(index); // NO puede volver a salir
            specialCustomerPending = false;

            Debug.Log("¡Aparece cliente ESPECIAL!");
        }
        else
        {
            if (possibleCustomers.Count == 0) return;

            customerToSpawn = possibleCustomers[
                Random.Range(0, possibleCustomers.Count)
            ];
        }

        SpawnCustomer(customerToSpawn);
    }

    // PRUEBAS
    public void DebugServeDish()
    {
        dishesServedCount++;

        if (dishesServedCount % dishesPerSpecialCustomer == 0 &&
            remainingSpecialCustomers.Count > 0)
        {
            specialCustomerPending = true;
            Debug.Log("DEBUG: Próximo cliente será ESPECIAL");
        }
        else
        {
            Debug.Log("DEBUG: Plato servido. Total = " + dishesServedCount);
        }
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

        // Tiempo y gestión de spawn de clientes
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            TrySpawnCustomer();
            spawnTimer = spawnInterval;
        }
    }
}
