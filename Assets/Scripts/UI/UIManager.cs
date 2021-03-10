using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public enum NameUIShow
{
    GamePlay,
    PauseMenu,
    MainMenu,
    ShoppingMenu
}
[System.Serializable]
public struct UIList
{
    public NameUIShow NameUI;
    public GameObject UI;
}
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] NameUIShow startUI;
    [SerializeField] UIList[] listUI;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Init()
    {
        foreach (var item in listUI)
        {
            if (startUI == item.NameUI)
                item.UI.SetActive(true);
            else
                item.UI.SetActive(false);

        }
    }
    public void ShowUI(NameUIShow show)
    {
        foreach (var item in listUI)
        {
            if (show == item.NameUI)
                item.UI.SetActive(true);
            else
                item.UI.SetActive(false);
        }
    }

}
