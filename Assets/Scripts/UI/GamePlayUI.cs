using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI instace;
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI playerCoinText;
    // Start is called before the first frame update
    void Start()
    {
        if (instace == null)
            instace = this;
        else
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePlayerScore(float newScore)
    {
        playerScoreText.text = newScore.ToString("00000000");
    }
    public void UpdatePlayerCoin(float newScore)
    {
        playerCoinText.text = newScore.ToString("0000");
    }
}

