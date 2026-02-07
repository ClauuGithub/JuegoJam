using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] sentences;
    public float dialogueSpeed = 0.05f;

    private int index = 0;
    private bool isTyping = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        bool spacePressed =
            Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame;

        bool mousePressed =
            Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame;

        // SOLO acepta espacio/ratón cuando NO está escribiendo
        if ((spacePressed || mousePressed) && !isTyping)
        {
            index++;

            if (index < sentences.Length)
            {
                ShowSentence();
            }
            else
            {
                SceneManager.LoadScene("RestaurantScene");
            }
        }
    }

    void ShowSentence()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        StartCoroutine(TypeSentence(sentences[index]));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;

        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(dialogueSpeed);
        }

        isTyping = false;
    }

    void ResetDialogue()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;

        index = 0;
        isTyping = false;
        dialogueText.text = "";

        if (sentences != null && sentences.Length > 0)
            ShowSentence();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetDialogue();
    }
}

