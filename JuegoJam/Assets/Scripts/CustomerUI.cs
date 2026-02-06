using UnityEngine;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    public Image customerImage;
    public Image recipeIcon;
    public Image patienceCircle;  // Imagen tipo Fill radial
    public Button takeOrderButton;

    public Order linkedOrder;

    public void Setup(Order order)
    {
        linkedOrder = order;

        customerImage.sprite = order.customer.sprite;
        recipeIcon.sprite = order.recipe.icon;

        // Círculo lleno al inicio
        patienceCircle.fillAmount = 1f;

        // Configuramos botón
        takeOrderButton.onClick.RemoveAllListeners();
        takeOrderButton.onClick.AddListener(TakeOrder);

        takeOrderButton.gameObject.SetActive(!order.taken);
    }

    void Update()
    {
        if (linkedOrder == null) return;

        // Si la orden ya fue tomada, el tiempo restante empieza a decrecer
        if (linkedOrder.taken)
        {
            float fill = Mathf.Clamp01(linkedOrder.timeRemaining / linkedOrder.customer.patienceTime);
            patienceCircle.fillAmount = fill;
        }
    }

    void TakeOrder()
    {
        if (linkedOrder == null) return;

        linkedOrder.taken = true;
        linkedOrder.timeRemaining = linkedOrder.customer.patienceTime;

        takeOrderButton.gameObject.SetActive(false);
    }
}
