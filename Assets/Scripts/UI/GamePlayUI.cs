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

    [SerializeField] TextMeshProUGUI countdownResumeText;
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
        countdownResumeText.gameObject.SetActive(false);

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
    public void CountDownResumeText(int num)
    {
        if (num != 0)
        {
            countdownResumeText.gameObject.SetActive(true);
            countdownResumeText.text = num.ToString("0");

            var anim = countdownResumeText.gameObject.GetComponent<Animator>();
            if (anim)
                anim.Play("FadeInCountDownAnim");
        }
        else
        {
            countdownResumeText.gameObject.SetActive(false);
            countdownResumeText.text = "";
        }

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

