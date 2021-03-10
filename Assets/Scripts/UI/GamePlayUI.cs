using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI instace;
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI playerCoinText;

    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    // Start is called before the first frame update
    void Start()
    {
        if (instace == null)
            instace = this;
        else
            Destroy(this.gameObject);

        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(PauseGame);
    }
    void PauseGame()
    {
        if (PlayerManager.instance.IsStop) return;

        PlayerManager.instance.PauseGame();
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

