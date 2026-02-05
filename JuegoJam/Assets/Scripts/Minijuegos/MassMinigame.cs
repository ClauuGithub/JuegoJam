using UnityEngine;
using UnityEngine.InputSystem;

public class MassMinigame : MonoBehaviour
{
	public GameObject masa;
	public GameObject forma;

	public int terminado = 10;

	public int tecla = 0;
    private void Update()
    {
        if(terminado != 0) 
		{
			if (Keyboard.current.wKey.isPressed && tecla ==1) 
			{
                Vector3 scale = masa.transform.localScale;
                scale.y *= -1;
                masa.transform.localScale = scale;
                tecla = 0;
				terminado--;
			}
            if (Keyboard.current.sKey.isPressed && tecla == 0)
            {
                Vector3 scale = masa.transform.localScale;
                scale.y *= -1;
				masa.transform.localScale = scale;
				tecla = 1;
				terminado--;
            }
        }
		else { forma.SetActive(true); }
    }
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
