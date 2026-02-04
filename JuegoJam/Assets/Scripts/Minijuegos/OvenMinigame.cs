using UnityEngine;

public class OvenMinigame : MonoBehaviour
{
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
