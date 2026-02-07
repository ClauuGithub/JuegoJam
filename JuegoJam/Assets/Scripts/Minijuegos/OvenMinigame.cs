using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OvenMinigame : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform bar;
    public RectTransform targetBar;
    public RectTransform arrow;

    [Header("Gameplay Settings")]
    public float arrowSpeed = 300f;
    public float timeLimit = 3f;

    private float timer;
    private bool movingRight = true;
    private bool gameActive = true;

    public GameObject barras;

    public int emp = 0;

    public Button comen;
    void Start()
    {
        
        timer = timeLimit;
        RandomizeTargetBar();
        comen.onClick.AddListener(Comenzar);
    }

    void Update()
    {
        if (emp == 1)
        {
            barras.SetActive(true);
            if (!gameActive) return;

            MoveArrow();
            HandleInput();
            UpdateTimer();
        }
    }
    void Comenzar() { emp = 1; }

    void MoveArrow()
    {
        float barWidth = bar.rect.width;
        float arrowX = arrow.anchoredPosition.x;

        if (movingRight)
            arrowX += arrowSpeed * Time.deltaTime;
        else
            arrowX -= arrowSpeed * Time.deltaTime;

        // Bounce off edges
        if (arrowX > barWidth / 2f)
        {
            arrowX = barWidth / 2f;
            movingRight = false;
        }
        else if (arrowX < -barWidth / 2f)
        {
            arrowX = -barWidth / 2f;
            movingRight = true;
        }

        arrow.anchoredPosition = new Vector2(arrowX, arrow.anchoredPosition.y);
    }

    void HandleInput()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            if (IsArrowOverTarget())
                Success();
            else
                Fail();
        }
    }

    bool IsArrowOverTarget()
    {
        float arrowX = arrow.anchoredPosition.x;

        float targetMin = targetBar.anchoredPosition.x - targetBar.rect.width / 2f;
        float targetMax = targetBar.anchoredPosition.x + targetBar.rect.width / 2f;

        return arrowX >= targetMin && arrowX <= targetMax;
    }

    void RandomizeTargetBar()
    {
        float barWidth = bar.rect.width;
        float targetWidth = targetBar.rect.width;

        float minX = -barWidth / 2f + targetWidth / 2f;
        float maxX = barWidth / 2f - targetWidth / 2f;

        float randomX = Random.Range(minX, maxX);
        targetBar.anchoredPosition = new Vector2(randomX, targetBar.anchoredPosition.y);
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Fail();
        }
    }

    public void Success()
	{
        Debug.Log("Éxito");
        GameManager.Instance.StationCompleted(true);
		this.gameObject.SetActive(false); // opcional: esconder el minijuego
	}

	public void Fail()
	{
        Debug.Log("Fallo");
        ResetMinigame();
	}

    void ResetMinigame()
    {
        timer = timeLimit;
        movingRight = true;
        gameActive = true;

        // Colocar flecha al inicio
        arrow.anchoredPosition = new Vector2(-bar.rect.width / 2f, arrow.anchoredPosition.y);

        // Nueva posición de la zona verde
        RandomizeTargetBar();
    }
}
