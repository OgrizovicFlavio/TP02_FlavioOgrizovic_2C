using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviourSingleton<CoinManager>
{
    [SerializeField] private TextMeshProUGUI coinCounterText;

    private int currentCoins = 0;

    protected override void OnAwaken()
    {
        ResetCoins();
    }

    public void AddCoin(int amount = 1)
    {
        currentCoins += amount;
        UpdateUI();
    }

    public void ResetCoins()
    {
        currentCoins = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinCounterText != null)
        {
            coinCounterText.text = $"x{currentCoins}";
        }
    }
}
