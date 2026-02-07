using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public TMP_Text pista;

    public GameObject oven;
    public GameObject cut;
    public GameObject grate;
    public GameObject masa;
    public GameObject prep;
    public GameObject mix;
    public GameObject stove;
    public GameObject pot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (oven.activeSelf) { pista.text = "Hit space when the square hits the green part"; }
        else if (cut.activeSelf) { pista.text = "Select the ingredient and click the knife to cut them"; }
        else if (grate.activeSelf) { pista.text = "Select the ingredient and click the grater to grate them"; }
        else if (masa.activeSelf) { pista.text = "Spam S and W and then hit the buttons in the sequence"; }
        else if (prep.activeSelf) { pista.text = "Select the ingredients"; }
        else if (mix.activeSelf) { pista.text = "Select the ingredients and spam A and D to mix them"; }
        else if (stove.activeSelf) { pista.text = "Click the pan and using Space stop the square in the green zone"; }
        else if (pot.activeSelf) { pista.text = "Click the pot and using Space stop the square in the green zone"; }
        else { pista.text = " "; }
    }
}
