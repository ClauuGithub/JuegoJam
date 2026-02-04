using UnityEngine;
using UnityEngine.UI;

public class CuttingBoardMinigame : MonoBehaviour
{
    public GameObject pointButtonPrefab;
    public RectTransform spawnArea;
    public int numberOfPoints = 10;

    void Start()
    {
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
            });
        }
    }

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