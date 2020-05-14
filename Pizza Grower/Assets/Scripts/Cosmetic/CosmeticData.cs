using UnityEngine;
using UnityEngine.UI;

public class CosmeticData : MonoBehaviour
{
    CosmeticHandler _handler => CosmeticHandler.instance;
    public int pizzaIndex;

    public void HandleClick()
    {
        Cosmetic cosmetic = _handler.cosmeticDefinition[pizzaIndex];
        if(_handler.unlocked[pizzaIndex])
        {
            _handler.SwitchSkin(pizzaIndex);
        } else
        {
            _handler.buyMenu.name = $"Buy menu{pizzaIndex}";
            _handler.buyMenu.SetActive(true);
            _handler.buyMenu.GetComponentInChildren<Text>().text = $"Unlock this skin?\n\nDo you wish to purchase\nthis skin for {cosmetic.cost} coins?";
        }
    }
}
