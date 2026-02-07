using UnityEngine;
using System.Collections.Generic;

public enum CookingStation // TODAS las estaciones posibles
{
    CuttingBoard,
    MassTable,
    Fryer,
    Oven,
    Stove,
    Plating,
    Spices,
    Shaping,
    CheeseGrater,
    Mixing
}

public enum Seasoning // TODAS las especias posibles
{
    Salt,
    Pepper,
    OliveOil,
    Nutmeg,
    Vinegar
}

[CreateAssetMenu(fileName = "Recipe", menuName = "Cooking/Recipe")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public Sprite icon;
    public List<CookingStation> stationSteps;
    public List<Seasoning> seasoningIngredients;
}
