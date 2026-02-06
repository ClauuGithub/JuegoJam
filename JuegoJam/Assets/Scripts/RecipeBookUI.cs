using UnityEngine;

public class RecipeBookUI : MonoBehaviour
{
    // Panel del libro de recetas
    public GameObject recipeBookPanel;
    // Botón de salir al menú
    public GameObject returnButton;
    // Solo muestra los objetos de esa receta
    public HideObjectsManager objectsActivator;

    public void OpenRecipeBook()
    {
        recipeBookPanel.SetActive(true);
        returnButton.SetActive(false);
    }

    public void CloseRecipeBook()
    {
        recipeBookPanel.SetActive(false);
        returnButton.SetActive(true);
    }

    public void OnRecipeChosen(RecipeSO recipe)
    {
        recipeBookPanel.SetActive(false); // cerramos el libro
        objectsActivator.ActivateRecipe(recipe); // activa los objetos
        GameManager.Instance.StartCooking(recipe); // empezamos a cocinar
    }
}
