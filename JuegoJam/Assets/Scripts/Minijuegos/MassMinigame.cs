using UnityEngine;
using UnityEngine.InputSystem;

public class MassMinigame : MonoBehaviour
{
	public GameObject masa;
	public GameObject forma;

	public int terminado = 10;

	public int tecla = 0;
    void Update()
    {
        if (terminado > 0)
        {
            if (Keyboard.current.wKey.wasPressedThisFrame && tecla == 1)
            {
                FlipMasa();
                tecla = 0;
                terminado--;
            }

            if (Keyboard.current.sKey.wasPressedThisFrame && tecla == 0)
            {
                FlipMasa();
                tecla = 1;
                terminado--;
            }
        }
        else
        {
            forma.SetActive(true);
        }
    }

    void FlipMasa()
    {
        Vector3 scale = masa.transform.localScale;
        scale.y *= -1;
        masa.transform.localScale = scale;
    }

    void OnEnable()
    {
        terminado = 10;
        tecla = 0;
        forma.SetActive(false);

        // se resetea escala por si quedó volteada
        masa.transform.localScale = new Vector3(
            Mathf.Abs(masa.transform.localScale.x),
            Mathf.Abs(masa.transform.localScale.y),
            Mathf.Abs(masa.transform.localScale.z)
        );
    }

}
