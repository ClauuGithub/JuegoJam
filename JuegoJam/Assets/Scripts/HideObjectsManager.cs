using UnityEngine;

public class HideObjectsManager : MonoBehaviour
{
    [Header("Referencias a recetas")]
    public RecipeSO saladRecipe;
    public RecipeSO tortillaRecipe;

    [Header("Objetos Ensalada")]
    public GameObject[] saladObjects;

    [Header("Objetos Tortilla")]
    public GameObject[] tortillaObjects;

    [Header("Todos los objetos")]
    public GameObject[] allObjects;

    public void ActivateRecipe(RecipeSO recipe)
    {
        HideAll();

        if (recipe == saladRecipe)
        {
            Enable(saladObjects);
        }
        else if (recipe == tortillaRecipe)
        {
            Enable(tortillaObjects);
        }
        else
        {
            Debug.LogWarning("Receta sin objetos configurados");
        }
    }

    void HideAll()
    {
        foreach (GameObject obj in allObjects)
            obj.SetActive(false);
    }

    void Enable(GameObject[] objs)
    {
        foreach (GameObject obj in objs)
            obj.SetActive(true);
    }
}
