using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MixingMinigame : MonoBehaviour
{
    [Header("Cantidad máxima de pulsaciones")]
    public int nMaxPress = 20;

    [Header("Botón que inicia el juego")]
    //public Button InitButton;

    [Header("Sprites del bowl")]
    public Sprite bowlEmpty;
    public Sprite bowlMix;
    public Sprite bowlIzq;
    public Sprite bowlDer;

    [Header("Objeto bowl")]
    public GameObject bowlMpt;

    [Header("Ingredientes")]
    public int totalIngredientsNeeded = 3;

    private int ingredientsAdded = 0;
    private bool ingredientsIns = false;

    private bool dWasntPressed;
    private bool aWasntPressed;
    private int nPress;
    private string nextKey;
    private bool gameStarted = false;

    private SpriteRenderer bowlRenderer;

    /*void Start()
    {
        InitButton.onClick.AddListener(StartMinigame);
    }*/

    void OnEnable()
    {
        // Reset completo al entrar
        gameStarted = false;
        ingredientsIns = false;
        ingredientsAdded = 0;

        dWasntPressed = true;
        aWasntPressed = true;
        nPress = 0;
        nextKey = "";

       // InitButton.gameObject.SetActive(true);

        bowlRenderer = bowlMpt.GetComponent<SpriteRenderer>();
        bowlRenderer.sprite = bowlEmpty;
    }

    // Llamar a este método cuando se añade un ingrediente al bowl
    public void AddIngredient()
    {
        ingredientsAdded++;

        if (ingredientsAdded >= totalIngredientsNeeded)
        {
            ingredientsIns = true;
            Debug.Log("Todos los ingredientes añadidos, ya puedes mezclar");
            StartMinigame();
        }
    }

    void StartMinigame()
    {
        if (gameStarted) return;

        if (!ingredientsIns)
        {
            Debug.Log("Faltan ingredientes para empezar");
            return;
        }

        gameStarted = true;

        dWasntPressed = true;
        aWasntPressed = true;
        nPress = 0;
        nextKey = "";

        bowlRenderer.sprite = bowlMix;
       // InitButton.gameObject.SetActive(false);

        Debug.Log("Minijuego empezado. Pulsa A o D");
    }

    void Update()
    {
        if (!gameStarted) return;

        if (nextKey == "")
        {
            if (Keyboard.current.aKey.isPressed)
            {
                nextKey = "D";
                nPress++;
                aWasntPressed = false;
                bowlRenderer.sprite = bowlDer;
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                nextKey = "A";
                nPress++;
                dWasntPressed = false;
                bowlRenderer.sprite = bowlIzq;
            }
        }
        else if (nextKey == "A" && Keyboard.current.aKey.isPressed && aWasntPressed)
        {
            nextKey = "D";
            nPress++;
            aWasntPressed = false;
            bowlRenderer.sprite = bowlIzq;
        }
        else if (nextKey == "D" && Keyboard.current.dKey.isPressed && dWasntPressed)
        {
            nextKey = "A";
            nPress++;
            dWasntPressed = false;
            bowlRenderer.sprite = bowlDer;
        }

        KeyPressedCheck();

        if (nPress >= nMaxPress)
        {
            Success();
        }
    }

    private void KeyPressedCheck()
    {
        if (!Keyboard.current.aKey.isPressed)
            aWasntPressed = true;

        if (!Keyboard.current.dKey.isPressed)
            dWasntPressed = true;
    }

    public void Success()
    {
        gameStarted = false;
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
