using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image img;
    float time = 2.5f;
    float maxTime = 0f;
    public GameObject loading;
    public GameObject homeScene;
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
    void Start()
    {
        img.fillAmount = 0;
        maxTime = time;
        homeScene.gameObject.SetActive(false);
        Invoke(nameof(Hide),2.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            float full =1- time / maxTime;  // Tính giá trị của thanh trượt
            img.fillAmount = full;
        }
        else
        {
            img.fillAmount = 1f;
        }
    }
    void Hide()
    {
        loading.gameObject.SetActive(false);
        homeScene.gameObject.SetActive(true);

    }
}
