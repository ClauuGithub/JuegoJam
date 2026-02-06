using UnityEngine;
using UnityEngine.UI;

public class StoveMinigame : MonoBehaviour
{
    [Header("Altura mínima requerida del indicador")] //Esto lo quito cuando vea el valor correcto
    public float IndicatorMinPos;

    [Header("Altura máxima requerida del indicador")] //Esto lo quito cuando vea el valor correcto
    public float IndicatorMaxPos;

    [Header("Valor de decrecimiento")] //Esto lo quito cuando vea el valor correcto
    public float dec;

    [Header("Valor de crecimiento")] //Esto lo quito cuando vea el valor correcto
    public float inc;

    [Header("Botón que inicia el juego")]
    public Button Pan;

    [Header("Flechita que indica como de hecho está el plato")]
    public GameObject Indicator;


    private float IndicatorPos;
    private float IndicatorVel;
    private float IndicatorInitialPos;
    private bool gameStarted = false;

    void Start()
    {
        Pan.onClick.AddListener(StartMinigame);
    }

    void StartMinigame()
    {
        if (gameStarted)
        {
            return;
        }



        IndicatorPos = Indicator.transform.position.y;
        IndicatorInitialPos = Indicator.transform.position.y;

        IndicatorVel = 0.0f;



        Pan.gameObject.SetActive(false); //Oculta el botón para que no estorbe
        Debug.Log("Minijuego empezado. Pulsa Espacio para subir el fuego y sueltalo para bajarlo");
    }

    void Update()
    {
        if (!gameStarted)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IndicatorVel += inc; // Esto aumanta la velocidad rapido al inicio pero lento al final.
                                 // Si se quiere que sea al revés se tendrá que multiplicar el valor y ajustar el valor de aumento
        }
        else if (!Input.GetKeyDown(KeyCode.Space) && IndicatorVel != 0.0f)
        {
            IndicatorVel = IndicatorVel * dec;

            if (IndicatorVel < 0.05f)
            {
                IndicatorVel = 0.0f;
            }
        }

        if ((IndicatorPos + IndicatorVel) > IndicatorMaxPos)
        {
            Fail();
        }
        else if (((IndicatorPos + IndicatorVel) <= IndicatorMaxPos) && ((IndicatorPos + IndicatorVel) >= IndicatorMinPos))
        {
            Success();
        }
        else
        {
            IndicatorPos += IndicatorVel;
            Indicator.transform.position = new Vector3(Indicator.transform.position.x, IndicatorPos, Indicator.transform.position.z);
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
