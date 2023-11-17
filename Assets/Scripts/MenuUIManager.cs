using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public GameObject startPanel;

    public Text highScoreText;

    public Button startButton;

    public Button musicButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    public Button soundButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Start()
    {
        highScoreText.text = "High score: " + PlayerPrefs.GetInt("HighScore");

        SetMusicSprite();
        SetSoundSprite();

        InitializeButton();
    }

    private void InitializeButton()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() =>
            {
                startPanel.SetActive(false);
                GameManager.Instance.NewGame();
            });
        }

        if (musicButton != null)
        {
            musicButton.onClick.RemoveAllListeners();
            musicButton.onClick.AddListener(() =>
            {
                if (PlayerPrefs.GetFloat("MusicVolume") == 0)
                {
                    AudioManager.instance.OnMusic();
                    SetMusicSprite();
                }
                else
                {
                    AudioManager.instance.OffMusic();
                    SetMusicSprite();
                }
            });
        }

        if (soundButton != null)
        {
            soundButton.onClick.RemoveAllListeners();
            soundButton.onClick.AddListener(() =>
            {
                if (PlayerPrefs.GetFloat("SoundVolume") == 0)
                {
                    AudioManager.instance.OnSound();
                    SetSoundSprite();
                }
                else
                {
                    AudioManager.instance.OffSound();
                    SetSoundSprite();
                }
            });
        }
    }

    private void SetSoundSprite()
    {
        if (PlayerPrefs.GetFloat("SoundVolume") == 0)
        {
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
        }
    }

    private void SetMusicSprite()
    {
        if (PlayerPrefs.GetFloat("MusicVolume") == 0)
        {
            musicButton.GetComponent<Image>().sprite = musicOffSprite;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicOnSprite;
        }
    }
}
