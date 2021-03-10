using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public bool IsStop, GodStop;

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
        playerCoin = 0;
        playerScore = 0;
        playerHealth = fullHealth;
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
