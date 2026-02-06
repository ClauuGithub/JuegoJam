using UnityEngine;
using UnityEngine.UI;

public class PrepTableMinigame : MonoBehaviour
{
    public SeasoningStationUI seasoningUI; // referencia al generador de iconos
    public Image dishImage;                // imagen grande del plato en medio

    int remainingSeasonings;

    public void StartPrepStep(RecipeSO recipe)
    {
        // Mostrar plato en el centro
        dishImage.sprite = recipe.icon;

        // Mostrar ingredientes
        seasoningUI.ShowSeasonings(recipe, this);

        // Guardamos cuántos hay que añadir
        remainingSeasonings = recipe.seasoningIngredients.Count;
    }

    public void OnSeasoningAdded()
    {
        remainingSeasonings--;

        if (remainingSeasonings <= 0)
        {
            Success();
        }
    }

    public void Success()
    {
        GameManager.Instance.StationCompleted(true);
        gameObject.SetActive(false);
    }

    public void Fail()
    {
        GameManager.Instance.StationCompleted(false);
        gameObject.SetActive(false);
    }
}
