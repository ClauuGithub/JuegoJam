using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StoveMinigame : MonoBehaviour
{
    [Header("Valor de decrecimiento")]
    public float dec;

    [Header("Valor de crecimiento")]
    public float inc;

    [Header("Botón que inicia el juego")]
    public Button PanButton;

    [Header("Flechita que indica como de hecho está el plato")]
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

		ProgressionBar.gameObject.SetActive(true);
		Indicator.gameObject.SetActive(true);

		//Se inicializan variables
		IndicatorPos = Indicator.anchoredPosition.x;
        IndicatorVel = 0.0f;

        PanButton.gameObject.SetActive(false); //Oculta el botón para que no estorbe
		gameStarted=true;
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

        // Se cambia la posición del indicador
		//Debug.Log("Velocidad del indicador: " + IndicatorVel);
		IndicatorPos += IndicatorVel * Time.deltaTime;
		Indicator.anchoredPosition = new Vector2(IndicatorPos, Indicator.anchoredPosition.y);

		IndicatorIsSafe(); // Se comprueba si se ha ganado o perdido
    }
    
	private void IndicatorIsSafe()
	{
		//SE PUEDE PONER MÁS BONITO
		targetMin = SafeBar.anchoredPosition.x - SafeBar.rect.width / 2f;
		targetMax = SafeBar.anchoredPosition.x + SafeBar.rect.width / 2f;

		if (IndicatorPos >= targetMin && IndicatorPos <= targetMax && IndicatorVel == 0.0f) //Acierta si el indicador está quieto dentro de la zona segura
        {
            Debug.Log("MINIJUEGO COMPLETADO");
            Success();
        } 
        else if (IndicatorPos > targetMax) //Falla si el indicador sobrepasa la zona segura
        {
			Debug.Log("MINIJUEGO FALLADO");
			Fail();
        }
	}


	public void Success()
    {
        gameStarted = false; // Importante apagar el interruptor
        GameManager.Instance.StationCompleted(true);
        this.gameObject.SetActive(false);
    }

    public void Fail()
    {
        gameStarted = false;
        GameManager.Instance.StationCompleted(false);
        this.gameObject.SetActive(false);
    }
}
