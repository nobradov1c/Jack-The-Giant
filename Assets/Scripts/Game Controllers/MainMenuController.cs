using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button musicBtn;
    [SerializeField]
    private Sprite[] musicIcons;
    // Start is called before the first frame update
    void Start()
    {
        checkToPlayTheMusic();
    }
    void checkToPlayTheMusic()
    {
        if(GamePreferences.GetMusicState() == 1)
        {
            MusicController.instance.PlayMusic(true);
            musicBtn.image.sprite = musicIcons[0];
        } else
        {
            MusicController.instance.PlayMusic(false);
            musicBtn.image.sprite = musicIcons[1];
        }
    }

    public void StartGame()
    {
        GameManager.instance.gameStartedFromMainMenu = true;
        //        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
        SceneFader.instance.LoadLevel("Gameplay");
    }

    public void HighscoreMenu()
    {
        SceneManager.LoadScene("Highscore", LoadSceneMode.Single);

    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MusicButton()
    {
        if (GamePreferences.GetMusicState() == 1)
        {
            GamePreferences.SetMusicState(0);
            MusicController.instance.PlayMusic(false);
            musicBtn.image.sprite = musicIcons[1];
        }
        else
        {
            GamePreferences.SetMusicState(1);
            MusicController.instance.PlayMusic(true);
            musicBtn.image.sprite = musicIcons[0];
        }
    }
}
