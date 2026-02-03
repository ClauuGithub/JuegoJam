using UnityEngine;

public class CuttingBoardMinigame : MonoBehaviour
{
    // ESTOS DOS MÉTODOS TIENEN QUE APARECER EN CADA MINIJUEGO:
    public void Success()
    {
        GameManager.Instance.StationCompleted(true);
        this.gameObject.SetActive(false); // opcional: esconder el minijuego
    }

    public void Fail()
    {
        GameManager.Instance.StationCompleted(false);
        this.gameObject.SetActive(false);
    }
}