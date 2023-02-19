using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI bulletText,heathText,coinText;
    public GameObject dialogPanel, btnPlay,nextChapterPanel;
    public Image gameOverOrPauseImg;
    public Sprite sprite_pause, sprite_gameOver,Sprite_gameWon;
    public void SetItem(int text,bool isCoin)
    {
        if(isCoin)
            coinText.text = text + " x";
        else 
            bulletText.text = text + " x";
    }

    public void SetHeath(int heath)
    {
        heathText.text = "x " + heath;
    }

    public void ShowDialog(int state,bool isActive = true)
    {
        dialogPanel.SetActive(isActive);
        if(!isActive) return;
        if (state != 0)
        {
            if (state == 2)
            {
                ShowOrHidePlay(true,false);
                gameOverOrPauseImg.GetComponent<Image>().sprite = sprite_pause;
                return;
            }
            ShowOrHidePlay(false,false);
            gameOverOrPauseImg.GetComponent<Image>().sprite = sprite_gameOver;
            return;
        }
        ShowOrHidePlay(false,true);
        gameOverOrPauseImg.GetComponent<Image>().sprite = Sprite_gameWon;
    }

    public void ShowOrHidePlay(bool isPlay, bool isNextChap)
    {
        btnPlay.SetActive(isPlay);
        nextChapterPanel.SetActive(isNextChap);
    }

    
}