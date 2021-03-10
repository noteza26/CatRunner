using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public bool IsStop, IsPause, GodStop;

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
    }
    void InitData()
    {
        IsStop = true;
        IsPause = false;
        playerCoin = 0;
        playerScore = 0;
        playerHealth = fullHealth;
    }
    public void PauseGame()
    {
        if (IsStop) return;
        IsPause = !IsPause;
    }

    private void Update()
    {
        if (IsPause)
        {
            Time.timeScale = 0;
            UIManager.instance.ShowUI(NameUIShow.PauseMenu);
        }
        else
        {
            Time.timeScale = 1;
            UIManager.instance.ShowUI(NameUIShow.GamePlay);

        }
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
    public float GetPlayerDistance()
    {
        return distanceScore;
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
