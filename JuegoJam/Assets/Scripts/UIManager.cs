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
        massGame.SetActive(false);
        fryerGame.SetActive(false);
        ovenGame.SetActive(false);
        stoveGame.SetActive(false);
        platingGame.SetActive(false);
    }

    public void UpdateOrdersUI(List<Order> orders)
    {
        // Marcamos qué UIs están en uso
        List<Order> ordersToDisplay = new List<Order>(orders);

        foreach (Transform child in customersContainer)
        {
            CustomerUI ui = child.GetComponent<CustomerUI>();
            if (ui == null) continue;

            // Si la orden sigue activa, actualizamos el UI
            if (ordersToDisplay.Contains(ui.linkedOrder))
            {
                ui.Setup(ui.linkedOrder);
                ordersToDisplay.Remove(ui.linkedOrder); // ya está en pantalla
            }
            else
            {
                // Orden ya no existe, eliminamos UI
                Destroy(child.gameObject);
            }
        }

        // Creamos UI solo para órdenes que no tenían
        foreach (var order in ordersToDisplay)
        {
            GameObject go = Instantiate(customerUIPrefab, customersContainer);
            CustomerUI ui = go.GetComponent<CustomerUI>();
            ui.Setup(order);
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
