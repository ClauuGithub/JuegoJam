using UnityEngine;

public class HideObjectsManager : MonoBehaviour
{
    [Header("Script Objects de las recetas")]
    public RecipeSO saladRecipe;
    public RecipeSO tortillaRecipe;
    public RecipeSO pretzelRecipe;
    public RecipeSO fondueRecipe;

    [Header("Objetos Ensalada")]
    public GameObject[] saladObjects;

    [Header("Objetos Tortilla")]
    public GameObject[] tortillaObjects;

    [Header("Objetos Pretzel")]
    public GameObject[] pretzelObjects;

    [Header("Objetos Fondue")]
    public GameObject[] fondueObjects;

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
        else if (recipe == pretzelRecipe)
        {
            Enable(pretzelObjects);
        }
        else if (recipe == fondueRecipe)
        {
            Enable(fondueObjects);
        }
        else
        {
            Debug.LogWarning("Receta sin objetos configurados");
        }
    }

    void HideAll()
    {
        foreach (GameObject obj in allObjects)
        {
            obj.SetActive(false);
        }
            
    }

    void Enable(GameObject[] objs)
    {
        foreach (GameObject obj in objs)
        {
            obj.SetActive(true);
        }
            
    }
}
