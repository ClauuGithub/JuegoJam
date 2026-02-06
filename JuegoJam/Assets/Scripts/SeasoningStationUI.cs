using UnityEngine;
using UnityEngine.UI;

public class SeasoningStationUI : MonoBehaviour
{
    public Transform seasoningContainer;
    public GameObject seasoningIconPrefab;        // Prefab del icono
    public SeasoningIconDatabase iconDatabase;    // Base de datos de iconos

    public void ShowSeasonings(RecipeSO recipe, PrepTableMinigame game)
    {
        ClearContainer();

        foreach (Seasoning seasoning in recipe.seasoningIngredients)
        {
            // Instanciamos el prefab
            GameObject iconGO = Instantiate(seasoningIconPrefab, seasoningContainer);
            SeasoningUi ui = iconGO.GetComponent<SeasoningUi>();

            // Asignamos sprite y minijuego
            Sprite iconSprite = iconDatabase.GetIcon(seasoning);
            ui.Setup(iconSprite, game);

            // Conectamos automáticamente el botón al OnClick del SeasoningUi
            Button btn = iconGO.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(ui.OnClick);
            }
        }
    }

    void ClearContainer()
    {
        foreach (Transform child in seasoningContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
