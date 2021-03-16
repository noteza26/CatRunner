using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
    void CheckInput()
    {
        if (PlayerManager.instance.GameStart) return;
        if (Input.touchCount == 1 || Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
    void StartGame()
    {
        PlayerManager.instance.StartGame();
        UIManager.instance.ChangeUI(NameUIShow.GamePlay);
        GamePlayUX.instance.Tween();
        Debug.Log("START GAME");
    }
}
