using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject counterPanel;
    public GameObject recipeBookPanel;

    public GameObject cuttingGame;
    public GameObject prepGame;
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
            case CookingStation.PrepTable: prepGame.SetActive(true); break;
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
        prepGame.SetActive(false);
        fryerGame.SetActive(false);
        ovenGame.SetActive(false);
        stoveGame.SetActive(false);
        platingGame.SetActive(false);
    }
}
