using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    RecipeSO currentRecipe;
    int currentStepIndex;

    public PrepTableMinigame prepGame; // arrastrar desde Inspector


    void Awake()
    {
        Instance = this;
    }

    public void StartCooking(RecipeSO recipe)
    {
        // Revisamos si el mostrador está lleno
        if (CounterDishManager.Instance.CurrentDishCount() >= CounterDishManager.Instance.maxDishesOnCounter)
        {
            Debug.Log("No puedes cocinar, el mostrador está lleno");
            return;
        }

        currentRecipe = recipe;
        currentStepIndex = 0;
        GoToNextStation();
    }

    void GoToNextStation()
    {
        if (currentStepIndex >= currentRecipe.stationSteps.Count) // Si ya hemos llegado a la última estación
        {
            FinishDish();
            return;
        }

        // Siguiente estación (0 si es la primera):
        CookingStation station = currentRecipe.stationSteps[currentStepIndex];
        UIManager.Instance.ShowMinigame(station);

        if (station == CookingStation.Spices || station == CookingStation.Mixing)
        {
            prepGame.gameObject.SetActive(true);
            prepGame.StartPrepStep(currentRecipe);
        }
    }

    public void StationCompleted(bool success)
    {
        // Aumentamos el contador de estación solo si se ha superado la estación, en caso contrario habrá que repetirla
        if (success)
            currentStepIndex++;

        GoToNextStation();
    }

    void FinishDish()
    {
        UIManager.Instance.ReturnToCounter();
        CounterDishManager.Instance.AddDish(currentRecipe);
    }
}
