using UnityEngine;
using UnityEngine.UI;

public class ButtonSetting : MonoBehaviour
{
    public GameObject imgOn;  
    public GameObject imgOff;
    public string key;
    private bool isActive = true;

    private void Start()
    {
        LoadState();
    }

    public void Toggle()
    {
        isActive = !isActive;
        UpdateImage();
        Save();
        AudioManager.Instance.SetActive(isActive);
        AudioManager.Instance.AudioButtonClick();

    }

    private void UpdateImage()
    {
        imgOn.SetActive(isActive);
        imgOff.SetActive(!isActive);
    }

    private void Save()
    {
        int trangThaiLuu = isActive ? 1 : 0;
        PlayerPrefs.SetInt(key, trangThaiLuu);
        PlayerPrefs.Save();
    }

    private void LoadState()
    {
        int trangThaiDaLuu = PlayerPrefs.GetInt(key, 1);
        isActive = trangThaiDaLuu == 1;
        UpdateImage();
        AudioManager.Instance.SetActive(isActive);
    }
}
