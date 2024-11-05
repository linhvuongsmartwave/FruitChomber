using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image buttonImg;
    [SerializeField] private Sprite currentButton;
    [SerializeField] private Sprite lockedButton;
    [SerializeField] private Sprite playedButton;
    [SerializeField] private TextMeshProUGUI txtNumberLevel;
    public int numLevel;
    public int nextLevel;
    private Button button;
    private bool canClick = true;
    //public SceneFader sceneFader;
    public static LevelButton Instance;

    private void Awake()
    {
        Assert.IsNotNull(currentButton);
        Assert.IsNotNull(lockedButton);
        Assert.IsNotNull(buttonImg);
        Assert.IsNotNull(txtNumberLevel);
        Instance = this;
        txtNumberLevel.text = (numLevel+1).ToString();
        int nb =PlayerPrefs.GetInt("CompletedLevel");
        nextLevel = nb;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        if (numLevel == nextLevel) buttonImg.sprite = currentButton;

        else if (numLevel < nextLevel) buttonImg.sprite = playedButton;

        else
        {
            buttonImg.sprite = lockedButton;
            txtNumberLevel.gameObject.SetActive(false);
            canClick = false;
        }
    }

    public void OnButtonClick()
    {
        if (canClick)
        {
            Debug.Log("OnButtonClick");
            PlayerPrefs.SetInt("SelectedLevel", numLevel);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GamePlay");
            //sceneFader.FadeTo("GamePlay");
        }
    }
}
