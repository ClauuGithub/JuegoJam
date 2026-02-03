using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    RecipeSO currentRecipe;
    int currentStepIndex;

    void Awake()
    {
        Instance = this;
    }

    public void StartCooking(RecipeSO recipe)
    {
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
        // Comprobamos si alguien quería ese pedido
        UIManager.Instance.ReturnToCounter();
        FindFirstObjectByType<OrderManager>().DeliverDish(currentRecipe);
    }
}
