using UnityEngine;
using UnityEngine.InputSystem;

public class ShapingMinigame : MonoBehaviour
{
    public GameObject masa1;
    public GameObject masa2;

    public GameObject tec1;
    public GameObject tec2;
    public GameObject tec3;
    public GameObject tec4;

    public int tec = 1;

    public void Start()
    {
       masa1.SetActive(false);
        masa2.SetActive(false);
    }

    public void Update()
    {
        if(tec != 5) 
        {
            if (Keyboard.current.sKey.isPressed && tec == 1)
            {
                tec1.SetActive(false);
                tec++;
            }
            if (Keyboard.current.aKey.isPressed && tec == 2)
            {
                tec2.SetActive(false);
                tec++;
            }
            if (Keyboard.current.dKey.isPressed && tec == 3)
            {
                tec3.SetActive(false);
                tec++;
            }
            if (Keyboard.current.wKey.isPressed && tec == 4)
            {
                tec4.SetActive(false);
                tec++;
                Success();
            }
        }
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
