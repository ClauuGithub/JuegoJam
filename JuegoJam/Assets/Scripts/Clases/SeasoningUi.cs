using UnityEngine;
using UnityEngine.UI;

public class SeasoningUi : MonoBehaviour
{
    public Image icon;
    PrepTableMinigame minigame;

    public void Setup(Sprite sprite, PrepTableMinigame game)
    {
        icon.sprite = sprite;
        minigame = game;
    }

    public void OnClick()
    {
        // Aquí se mete la animación
        Debug.Log("Ingrediente añadido");

        minigame.OnSeasoningAdded();

        Destroy(gameObject); // desaparece el botón
    }
}
