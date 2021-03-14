using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public bool GameStart, IsStop, IsPause, GodStop, GodMode, onCountDown;
    [SerializeField] GameObject characterPref;
    [SerializeField]
    GameObject playerCharacter;
    [SerializeField] float playerScore;
    [SerializeField] float playerCoin;

    [SerializeField] int playerHealth;

    [Header("Setting")]
    [SerializeField] int fullHealth;
    [SerializeField] float scoreAdd;
    [SerializeField] float extraScore;
    [SerializeField] float speedScore;

    private float distanceScore;
    void Start()
    {
        instance = this;
        InitData();
        InstantPlayer();
    }
    void InstantPlayer()
    {
        if (playerCharacter == null)
        {

            var instant = Instantiate(characterPref, this.transform);
            playerCharacter = instant;
        }
        var camInstance = CameraFollow.instance;
        if (camInstance)
            CameraFollow.instance.SetCamera(playerCharacter);
    }
    void InitData()
    {
        GameStart = false;
        IsStop = true;
        IsPause = false;
        playerHealth = fullHealth;
        ClearData();
    }
    void ClearData()
    {
        playerCoin = 0;
        playerScore = 0;
    }
    public void PauseGame()
    {
        if (IsStop || onCountDown) return;
        IsPause = !IsPause;

        if (IsPause)
        {
            Time.timeScale = 0;
            UIManager.instance.ShowUI(NameUIShow.PauseMenu);
        }
        else if (!IsPause)
            StartCoroutine("ResumeGame");

    }
    IEnumerator ResumeGame()
    {
        onCountDown = true;
        UIManager.instance.ShowUI(NameUIShow.GamePlay);
        GamePlayUI.instace.CountDownResumeText(3);
        yield return new WaitForSecondsRealtime(1);
        GamePlayUI.instace.CountDownResumeText(2);
        yield return new WaitForSecondsRealtime(1);
        GamePlayUI.instace.CountDownResumeText(1);
        yield return new WaitForSecondsRealtime(1);
        GamePlayUI.instace.CountDownResumeText(0);
        Time.timeScale = 1;
        onCountDown = false;
    }
    public void AddPlayerScore(float add)
    {
        distanceScore += add;
        playerScore += add * (extraScore * speedScore);

        if (GamePlayUI.instace)
            GamePlayUI.instace.UpdatePlayerScore(playerScore);
    }
    public void SetSpeedScore(float newSpeed)
    {
        speedScore = newSpeed / 10;
    }
    public void ChangeScoreAdd(float newValue)
    {
        scoreAdd = newValue;
    }
    public float GetPlayerScore()
    {
        return playerScore;
    }
    public float GetSpeedScore()
    {
        return speedScore;
    }
    public float GetPlayerDistance()
    {
        return distanceScore;
    }
    public void HitObstacle()
    {
        if (GodMode) return;
        Debug.Log("Hit Obstacle");
    }
    public void StartGame()
    {
        GameStart = true;
        ClearData();
        var camInstance = CameraMovement.instance;
        if (camInstance)
        {
            camInstance.StartGame();
            PlayerAnimation.instance.StartGame();

        }

    }
    public void AddItem(Item item)
    {

        if (item.Type == TypeItemEnum.Coin)
        {
            AddCoinPlayer(item.AddTo, item.IsExtra);
        }
        else if (item.Type == TypeItemEnum.Item)
        {
            if (item.TypeItem == TypeItemAddEnum.AddHealth)
            {
                AddHealth((int)item.AddTo);
            }
            else if (item.TypeItem == TypeItemAddEnum.FullHealth)
            {
                AddHealth(fullHealth);
            }
        }
    }
    public void AddScorePlayer(float scoreToAdd, bool isExtra)
    {
        /* if (isExtra)
             playerScore += scoreToAdd * 2;
         else
             playerScore += scoreToAdd;*/
    }
    public void AddCoinPlayer(float coinToAdd, bool isExtra)
    {
        if (isExtra)
            playerCoin += coinToAdd * 2;
        else
            playerCoin += coinToAdd;

        GamePlayUI.instace.UpdatePlayerCoin(playerCoin);
    }
    public void AddHealth(int healthAdd)
    {
        if (playerHealth == fullHealth) return;

        playerHealth = healthAdd;
    }
}
