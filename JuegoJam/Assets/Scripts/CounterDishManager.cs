using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CounterDishManager : MonoBehaviour
{
    public static CounterDishManager Instance;

    public Transform dishesContainer;   // Donde aparecen los platos
    public GameObject dishUIPrefab;     // Prefab del plato

    List<RecipeSO> readyDishes = new List<RecipeSO>();

    // PRUEBAAAAAS
    [Header("DEBUG")]
    public List<RecipeSO> debugRecipes;

    public void DebugAddDish(int index)
    {
        if (index < 0 || index >= debugRecipes.Count) return;

        AddDish(debugRecipes[index]);
        Debug.Log("DEBUG: Plato añadido al mostrador: " + debugRecipes[index].recipeName);
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            AddDish(debugRecipes[0]);

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            AddDish(debugRecipes[1]);
    }

    /////

    void Awake() => Instance = this;

    public void AddDish(RecipeSO recipe)
    {
        readyDishes.Add(recipe);
        UpdateDishUI();
    }

    void UpdateDishUI()
    {
        foreach (Transform child in dishesContainer)
            Destroy(child.gameObject);

        foreach (RecipeSO recipe in readyDishes)
        {
            GameObject go = Instantiate(dishUIPrefab, dishesContainer);
            go.GetComponent<Image>().sprite = recipe.icon;

            go.GetComponent<Button>().onClick.RemoveAllListeners();
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                TryServeDish(recipe);
            });
        }
    }

    void TryServeDish(RecipeSO recipe)
    {
        bool success = FindFirstObjectByType<OrderManager>().TryDeliverDish(recipe);

        if (!success) // Si el plato no se encuentra entre las distintas órdenes:
            Debug.Log("Nadie quería este plato");

        readyDishes.Remove(recipe);
        UpdateDishUI();
    }
}
