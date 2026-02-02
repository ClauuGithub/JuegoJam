using UnityEngine;

public class RecipeBookUI : MonoBehaviour
{
    // Panel del libro de recetas
    public GameObject recipeBookPanel;

    public void OpenRecipeBook()
    {
        recipeBookPanel.SetActive(true);
    }

    public void OnRecipeChosen(RecipeSO recipe)
    {
        recipeBookPanel.SetActive(false); // cerramos el libro
        GameManager.Instance.StartCooking(recipe); // empezamos a cocinar
    }
}
