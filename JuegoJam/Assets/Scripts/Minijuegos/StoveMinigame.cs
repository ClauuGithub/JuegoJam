using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class StoveMinigame : MonoBehaviour
{
    [Header("Valor de decrecimiento")]
    public float dec;

    [Header("Valor de crecimiento")]
    public float inc;

    [Header("Bot�n que inicia el juego")]
    public Button PanButton;

    [Header("Huevo")]
    public GameObject egg;

    [Header("Solido 1")]
    public GameObject solid1;

    [Header("Solido 2")]
    public GameObject solid2;

    [Header("Tortilla")]
    public GameObject omelette;

    [Header("Flechita que indica como de hecho est� el plato")]
    public RectTransform Indicator;

    [Header("Barra por la que se mueve el indicador")]
    public RectTransform ProgressionBar;

    [Header("Espacio seguro")]
    public RectTransform SafeBar;

    private bool gameStarted = false;
    private float IndicatorPos;
    private float IndicatorVel;
    private float targetMin;
    private float targetMax;
    private bool resultTriggered = false;

    void Start()
    {
        PanButton.onClick.AddListener(StartMinigame);
    }

    void StartMinigame()
    {
        if (gameStarted) //Comprueba si ha empezado el juego o no
        {
            return;
        }

        resultTriggered = false;


        ProgressionBar.gameObject.SetActive(true);
        Indicator.gameObject.SetActive(true);

        //Se inicializan variables
        IndicatorPos = Indicator.anchoredPosition.x;
        IndicatorVel = 0.0f;

        PanButton.gameObject.SetActive(false); //Oculta el bot�n para que no estorbe

        //Activo los ingredientes
        egg.gameObject.SetActive(true);
        solid1.gameObject.SetActive(true);
        solid2.gameObject.SetActive(true);

        gameStarted = true;
        Debug.Log("Minijuego empezado. Pulsa Espacio para aumentar el fuego y sueltalo para disminuirlo");
    }

    void Update()
    {
        if (!gameStarted) //Comprueba si ha empezado el juego o no
        {
            return;
        }

        //Depende de si se pulsa o no el Espacio
        //se acelera o desacelera el indicador
        if (Keyboard.current.spaceKey.isPressed)
        {
            IndicatorVel += inc * Time.deltaTime;
        }
        else if (!Keyboard.current.spaceKey.isPressed && IndicatorVel != 0.0f)
        {

            IndicatorVel -= dec * Time.deltaTime;

            if (IndicatorVel < 0f)
            {
                IndicatorVel = 0f;
            }

        }

        // Se cambia la posici�n del indicador
        IndicatorPos += IndicatorVel * Time.deltaTime;

        // Limitar el indicador para que nunca se salga de la barra
        float minPos = -ProgressionBar.rect.width / 2 + Indicator.rect.width / 2;
        float maxPos = ProgressionBar.rect.width / 2 - Indicator.rect.width / 2;

        IndicatorPos = Mathf.Clamp(IndicatorPos, minPos, maxPos);
        Indicator.anchoredPosition = new Vector2(IndicatorPos, Indicator.anchoredPosition.y);


        IndicatorIsSafe(); // Se comprueba si se ha ganado o perdido
    }

    private void IndicatorIsSafe()
    {
        if (resultTriggered) return; // evita m�ltiples llamadas

        targetMin = SafeBar.anchoredPosition.x - SafeBar.rect.width / 2f;
        targetMax = SafeBar.anchoredPosition.x + SafeBar.rect.width / 2f;

        if (IndicatorPos >= targetMin && IndicatorPos <= targetMax && IndicatorVel <= 0.01f)
        {
            resultTriggered = true;
            Debug.Log("MINIJUEGO COMPLETADO");
            Success();
        }
        else if (IndicatorPos > targetMax)
        {
            resultTriggered = true;
            Debug.Log("MINIJUEGO FALLADO");
            Fail();
        }
    }



    public void Success()
    {
        StartCoroutine(HandleResult(true));
    }

    public void Fail()
    {
        StartCoroutine(HandleResult(false));
    }
    IEnumerator HandleResult(bool success)
    {
        gameStarted = false;

        // Apagar ingredientes visualmente
        egg.SetActive(false);
        solid1.SetActive(false);
        solid2.SetActive(false);

        if (success)
            omelette.SetActive(true);

        yield return null; // espera 1 frame para que Update no rompa

        GameManager.Instance.StationCompleted(success);

        // reseteamos TODO antes de desactivar
        ResetMinigame();
        this.gameObject.SetActive(false);
    }


    void OnEnable()
    {
        ResetMinigame();
    }

    void ResetMinigame()
    {
        gameStarted = false;
        resultTriggered = false;

        // Posici�n inicial m�s a la izquierda dentro de la barra
        IndicatorPos = -ProgressionBar.rect.width / 2 + Indicator.rect.width / 2;
        IndicatorVel = 0f;
        Indicator.anchoredPosition = new Vector2(IndicatorPos, Indicator.anchoredPosition.y);

        // Ingredientes visuales
        egg.SetActive(false);
        solid1.SetActive(false);
        solid2.SetActive(false);
        omelette.SetActive(false);

        // Barra y bot�n
        ProgressionBar.gameObject.SetActive(false);
        Indicator.gameObject.SetActive(false);
        PanButton.gameObject.SetActive(true);
    }


}
