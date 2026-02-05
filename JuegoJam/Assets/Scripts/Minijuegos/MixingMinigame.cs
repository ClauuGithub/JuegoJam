using UnityEngine;
using UnityEngine.UI;

public class MixingMinigame : MonoBehaviour
{
	[Header("Cantidad máxima de pulsaciones")]
	public int nMaxPress = 20;

	[Header("Botón que inicia el juego")]
	public Button InitBowl;

	[Header("Sprite bowl mezclar izq.")]
	public GameObject bowlIzq;

	[Header("Sprite bowl mezclar der.")]
	public GameObject bowlDer;

	[Header("Sprite bowl vacio")]
	public GameObject bowlMpt;

	[Header("Sprite bowl mezclado")]
	public GameObject bowlMixed;

	[Header("Sprite masa")]
	public GameObject bowlDough;

	[Header("Sprite liquido")]
	public GameObject bowlLiquid;

	[Header("Sprite solido 1")]
	public GameObject bowlSolid1;

	[Header("Sprite solido 2")]
	public GameObject bowlSolid2;


	private int nPress;
	private string nextKey;
	private bool gameStarted = false;

	void Start()
	{
		InitBowl.onClick.AddListener(StartMinigame);
	}

	// 1. Esto se ejecuta SOLO UNA VEZ al pulsar el bowl
	void StartMinigame()
	{
		if (gameStarted) return; // Si ya empezó, no hagas nada

		nPress = 0;
		nextKey = ""; // Empezamos vacío para que valga cualquiera (A o D)
		gameStarted = true;

		InitBowl.gameObject.SetActive(false); // Opcional: ocultar botón para que no estorbe
		bowlMpt.gameObject.SetActive(true);
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
				//Cambiar a sprite izq.
				setComplete(bowlIzq);
				nextKey = "D"; nPress++; 
			}
			else if (Input.GetKeyDown(KeyCode.D)) 
			{
				//Cambiar a sprite der.
				setComplete(bowlDer);
				nextKey = "A"; nPress++; 
			}
		}
		else if (nextKey == "A" && Input.GetKeyDown(KeyCode.A))
		{
			//Cambiar a sprite izq.
			setComplete(bowlIzq);
			nextKey = "D";
			nPress++;
		}
		else if (nextKey == "D" && Input.GetKeyDown(KeyCode.D))
		{
			//Cambiar a sprite der.
			setComplete(bowlDer);
			nextKey = "A";
			nPress++;
		}

		// Comprobación de victoria
		if (nPress >= nMaxPress)
		{
			Success();
		}
	}

	private void setComplete(GameObject newComplete)
	{
		setFalseAll();
		newComplete.gameObject.SetActive(true);
	}

	private void setPartial(GameObject newPartial)
	{
		setFalseAll();
		bowlMpt.gameObject.SetActive(true);
		newPartial.gameObject.SetActive(true);
	}

	private void setFalseAll()
	{
		bowlIzq.gameObject.SetActive(false);
		bowlDer.gameObject.SetActive(false);
		bowlMpt.gameObject.SetActive(false);
		bowlMixed.gameObject.SetActive(false);
		bowlDough.gameObject.SetActive(false);
		bowlLiquid.gameObject.SetActive(false);
		bowlSolid1.gameObject.SetActive(false);
		bowlSolid2.gameObject.SetActive(false);
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
