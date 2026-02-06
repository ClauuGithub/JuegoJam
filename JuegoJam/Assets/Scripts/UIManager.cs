using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    // MOSTRADOR
    public GameObject counterPanel;
    public Transform customersContainer; // Contenedor para los prefabs de los clientes
    public GameObject customerUIPrefab; // Prefab del cliente

    // LIBRO DE RECETAS
    public GameObject recipeBookPanel;

    // MINIJUEGOS
    public GameObject cuttingGame;
    public GameObject gratingGame;
    public GameObject massGame;
    public GameObject fryerGame;
    public GameObject ovenGame;
    public GameObject stoveGame;
    public GameObject platingGame;

    void Awake() => Instance = this;

    public void ShowMinigame(CookingStation station)
    {
        HideAll();

        switch (station)
        {
            case CookingStation.CuttingBoard: cuttingGame.SetActive(true); break;
            case CookingStation.CheeseGrater: gratingGame.SetActive(true); break;
            case CookingStation.MassTable: massGame.SetActive(true); break;
            case CookingStation.Fryer: fryerGame.SetActive(true); break;
            case CookingStation.Oven: ovenGame.SetActive(true); break;
            case CookingStation.Stove: stoveGame.SetActive(true); break;
            case CookingStation.Plating: platingGame.SetActive(true); break;
        }
    }

    public void ReturnToCounter()
    {
        HideAll();
        counterPanel.SetActive(true);
    }

    void HideAll()
    {
        counterPanel.SetActive(false);
        recipeBookPanel.SetActive(false);
        cuttingGame.SetActive(false);
        gratingGame.SetActive(false);
        massGame.SetActive(false);
        fryerGame.SetActive(false);
        ovenGame.SetActive(false);
        stoveGame.SetActive(false);
        platingGame.SetActive(false);
    }

    public void UpdateOrdersUI(List<Order> orders)
    {
        // Primero borramos cualquier UI existente de clientes
        foreach (Transform child in customersContainer)
        {
            Destroy(child.gameObject);
        }

        // Creamos un UI por cada pedido
        foreach (var order in orders)
        {
            GameObject go = Instantiate(customerUIPrefab, customersContainer);

            // Configuramos el sprite del cliente
            go.transform.Find("CustomerImage").GetComponent<Image>().sprite = order.customer.sprite;

            // Configuramos el icono de receta
            go.transform.Find("RecipeIcon").GetComponent<Image>().sprite = order.recipe.icon;

            // Configuramos la barra de paciencia
            Slider patienceSlider = go.transform.Find("PatienceSlider").GetComponent<Slider>();
            patienceSlider.maxValue = order.timeRemaining; // paciencia inicial
            patienceSlider.value = order.timeRemaining;

            // Configuramos el botón de tomar pedido
            Button takeButton = go.transform.Find("TakeOrderButton").GetComponent<Button>();
            takeButton.onClick.RemoveAllListeners(); // Quitamos cualquier listener anterior
            takeButton.onClick.AddListener(() =>
            {
                FindFirstObjectByType<OrderManager>().TakeOrder(order);
            });

        }
    }

    //MENUS

    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ConfigScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("RestaurantScene");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Juego cerrado");
    }



}
