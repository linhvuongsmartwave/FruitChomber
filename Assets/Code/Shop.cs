using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int ruby;
    public TextMeshProUGUI txtRuby;
    public static Shop Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Load();
    }

    public void BuyRuby(int ruby)
    {
        this.ruby += ruby;
        Save();
        UpdateGold();
    }

    public void Load()
    {
        ruby = PlayerPrefs.GetInt("ruby", ruby);
        UpdateGold();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("ruby", ruby);
        PlayerPrefs.Save();
    }

    public void UpdateGold()
    {
        txtRuby.text = ruby.ToString();
    }
}

