using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MixingMinigame : MonoBehaviour
{
	[Header("Cantidad máxima de pulsaciones")]
	public int nMaxPress = 20;

	[Header("Botón que inicia el juego")]
	public Button InitButton;

	[Header("Sprite bowl mexclado")]
	public Sprite bowlMix;

	[Header("Sprite bowl mezclar izq.")]
	public Sprite bowlIzq;

	[Header("Sprite bowl mezclar der.")]
	public Sprite bowlDer;

	[Header("Objeto bowl")]
	public GameObject bowlMpt;

	private bool ingredientsIns;
	private bool dWasntPressed;
	private bool aWasntPressed;
	private int nPress;
	private string nextKey;
	private bool gameStarted = false;
	private SpriteRenderer bowlRenderer;


	void Start()
	{
		InitButton.onClick.AddListener(StartMinigame);
	}

	// 1. Esto se ejecuta SOLO UNA VEZ al pulsar el bowl
	void StartMinigame()
	{
		if (gameStarted) return; // Si ya empezó, no hagas nada

		ingredientsIns = true; //Esto lo tengo que poner a false cuando implemente poner ingredientes
		dWasntPressed = true;
		aWasntPressed = true;
		nPress = 0;
		nextKey = ""; // Empezamos vacío para que valga cualquiera (A o D)
		gameStarted = true;

		bowlRenderer = bowlMpt.GetComponent<SpriteRenderer>();
		bowlRenderer.sprite = bowlMix;
		InitButton.gameObject.SetActive(false); // Opcional: ocultar botón para que no estorbe
												//bowlMpt.gameObject.SetActive(true);
		Debug.Log("Minijuego empezado. Pulsa A o D");
	}

	// 2. Esto se ejecuta TODO EL RATO (cada frame)
	void Update()
	{
		if (!gameStarted) return; // Si no le has dado al bowl, no leas el teclado


		if (nextKey == "" && ingredientsIns) // Primera pulsación (acepta cualquiera)
		{
			if (Keyboard.current.aKey.isPressed)
			{
				//Cambiar a sprite izq.
				nextKey = "D";
				nPress++;
				aWasntPressed = false;
				bowlRenderer.sprite = bowlDer;


			}
			else if (Keyboard.current.dKey.isPressed)
			{
				//Cambiar a sprite der.
				nextKey = "A";
				nPress++;
				dWasntPressed = false;
				bowlRenderer.sprite = bowlIzq;


			}
		}
		else if (nextKey == "A" && Keyboard.current.aKey.isPressed && aWasntPressed)
		{
			//Cambiar a sprite izq.
			nextKey = "D";
			nPress++;
			aWasntPressed = false;
			bowlRenderer.sprite = bowlIzq;


		}
		else if (nextKey == "D" && Keyboard.current.dKey.isPressed && dWasntPressed)
		{
			//Cambiar a sprite der.
			nextKey = "A";
			nPress++;
			dWasntPressed = false;
			bowlRenderer.sprite = bowlDer;


		}

		KeyPressedCheck();


		Debug.Log(nPress);
		// Comprobación de victoria
		if (nPress >= nMaxPress)
		{
			Success();
		}
	}

	private void KeyPressedCheck()
	{
		if (!Keyboard.current.aKey.isPressed)
		{
			aWasntPressed = true;

		}

		if (!Keyboard.current.dKey.isPressed)
		{
			dWasntPressed = true;
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
