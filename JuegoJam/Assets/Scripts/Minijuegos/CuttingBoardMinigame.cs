using UnityEngine;
using UnityEngine.UI;

public class CuttingBoardMinigame : MonoBehaviour
{
    [Header("Prefab del punto")]
    public GameObject pointButtonPrefab;

    [Header("Area donde aparecen los puntos")]
    public RectTransform spawnArea;

    [Header("Cantidad de puntos")]
    public int numberOfPoints = 10;

    [Header("Objeto inicia el juego")]
    public Button knifeButton;

    private bool gameStarted = false;

    void Start()
    {
        // El minijuego NO empieza automáticamente
        // Esperamos a que el jugador pulse el cuchillo
        knifeButton.onClick.AddListener(StartMinigame);
    }

    void StartMinigame()
    {
        if (gameStarted) return;

        gameStarted = true;

        // Ocultar el cuchillo cuando empieza
        knifeButton.gameObject.SetActive(false);

        SpawnAllPoints();
    }

    void SpawnAllPoints()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            GameObject btn = Instantiate(pointButtonPrefab, spawnArea);

            // Posición aleatoria dentro del panel
            float x = Random.Range(-spawnArea.rect.width / 2, spawnArea.rect.width / 2);
            float y = Random.Range(-spawnArea.rect.height / 2, spawnArea.rect.height / 2);

            RectTransform rt = btn.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(x, y);

            // Destruye el botón al hacer clic
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                Destroy(btn, 0.3f);

                // Si ya no quedan puntos éxito
                if (spawnArea.childCount == 1) // solo queda el panel
                    Success();
            });
        }
    }

    // ESTOS DOS MÉTODOS TIENEN QUE APARECER EN CADA MINIJUEGO:
    public void Success()
    {
        GameManager.Instance.StationCompleted(true);
        this.gameObject.SetActive(false);
    }

    public void Fail()
    {
        GameManager.Instance.StationCompleted(false);
        this.gameObject.SetActive(false);
    }
}
