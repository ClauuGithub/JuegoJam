using UnityEngine;
using UnityEngine.UI;

public class MixingMinigame : MonoBehaviour
{
	[Header("Cantidad máxima de pulsaciones")]
	public int nMaxPress = 20;

	[Header("Botón que inicia el juego")]
	public Button Bowl;

	private int nPress;
	private string nextKey;
	private bool gameStarted = false;

	void Start()
	{
		Bowl.onClick.AddListener(StartMinigame);
	}

	// 1. Esto se ejecuta SOLO UNA VEZ al pulsar el bowl
	void StartMinigame()
	{
		if (gameStarted) return; // Si ya empezó, no hagas nada

		nPress = 0;
		nextKey = ""; // Empezamos vacío para que valga cualquiera (A o D)
		gameStarted = true;

		Bowl.gameObject.SetActive(false); // Opcional: ocultar botón para que no estorbe
		Debug.Log("Minijuego empezado. Pulsa A o D");
	}
	   
	// 2. Esto se ejecuta TODO EL RATO (cada frame)
	void Update()
	{
		if (!gameStarted) return; // Si no le has dado al bowl, no leas el teclado

		
		if (nextKey == "") // Primera pulsación (acepta cualquiera)
		{
			if (Input.GetKeyDown(KeyCode.A)) 
			{ 
				nextKey = "D"; nPress++; 
			}
			else if (Input.GetKeyDown(KeyCode.D)) 
			{ 
				nextKey = "A"; nPress++; 
			}
		}
		else if (nextKey == "A" && Input.GetKeyDown(KeyCode.A))
		{
			nextKey = "D";
			nPress++;
		}
		else if (nextKey == "D" && Input.GetKeyDown(KeyCode.D))
		{
			nextKey = "A";
			nPress++;
		}

		// Comprobación de victoria
		if (nPress >= nMaxPress)
		{
			Success();
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
