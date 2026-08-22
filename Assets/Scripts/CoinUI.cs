using UnityEngine;
using TMPro;
public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    void Update()
    {
        coinText.text = "Coins: " + GameManager.instance.coins;
        scoreText.text = "Score: " + GameManager.instance.score;
    }
}