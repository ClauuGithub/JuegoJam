using UnityEngine;
using System.Collections.Generic;

public enum CookingStation // TODAS las estaciones posibles
{
    CuttingBoard,
    PrepTable,
    Fryer,
    Oven,
    Stove,
    Plating
}

[CreateAssetMenu(fileName = "Recipe", menuName = "Cooking/Recipe")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public Sprite icon;
    public List<CookingStation> stationSteps;
}
