using UnityEngine;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    public Image customerImage;
    public Image recipeIcon;
    public Image patienceCircleExt;
    public Image patienceCircle;  // Imagen tipo Fill radial
    public Button takeOrderButton;

    public Order linkedOrder;

    public void Setup(Order order)
    {
        linkedOrder = order;

        customerImage.sprite = order.customer.sprite;
        recipeIcon.sprite = order.recipe.icon;

        // Estado visual inicial
        takeOrderButton.gameObject.SetActive(!order.taken);
        patienceCircleExt.gameObject.SetActive(order.taken);
        patienceCircle.gameObject.SetActive(order.taken);

        patienceCircle.fillAmount = 1f;

        takeOrderButton.onClick.RemoveAllListeners();
        takeOrderButton.onClick.AddListener(TakeOrder);
    }


    void Update()
    {
        if (linkedOrder == null || !linkedOrder.taken) return;

        float fill = Mathf.Clamp01(linkedOrder.timeRemaining / linkedOrder.customer.patienceTime);
        patienceCircle.fillAmount = fill;
    }


    void TakeOrder()
    {
        if (linkedOrder == null) return;

        linkedOrder.taken = true;
        linkedOrder.timeRemaining = linkedOrder.customer.patienceTime;

        takeOrderButton.gameObject.SetActive(false);
        patienceCircleExt.gameObject.SetActive(true);
        patienceCircle.gameObject.SetActive(true);

        patienceCircle.fillAmount = 1f;
    }

}
