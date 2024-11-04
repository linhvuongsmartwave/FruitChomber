using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int ruby;
    public TextMeshProUGUI Ruby;
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
        Ruby.text = ruby.ToString();
    }
}

