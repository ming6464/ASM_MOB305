using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool isGameOverOrWin;
    public int coin,bullet,heath;

    public override void Start()
    {
        UIManager.Ins.SetItem(coin,true);
        UIManager.Ins.SetItem(bullet,false);
        UIManager.Ins.SetHeath(heath);
    }

    public void IncreaseItem(bool isCoin,bool isCreaseBullet = false)
    {
        if (isCoin)
        {
            coin++;
            UIManager.Ins.SetItem(coin,isCoin);
            if(coin == 10)
                ShowDialog(0);
        }
        else
        {
            if (isCreaseBullet)
                bullet++;
            else bullet--;
            UIManager.Ins.SetItem(bullet,isCoin);
        }
    }

    public void DecreaseHeath()
    {
        heath--;
        UIManager.Ins.SetHeath(heath);
        if (heath <= 0) ShowDialog(1);
    }

    public void ShowDialog(int state)
    {
        UIManager.Ins.ShowDialog(state);
        if (state != 2)
        {
            setActiveEnemies(false);
            isGameOverOrWin = true;
        }
        else 
            Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        UIManager.Ins.ShowDialog(1,false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        UIManager.Ins.ShowDialog(2);
    }
    

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
    void setActiveEnemies(bool isActive)
    {
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            e.SetActiveAnim(isActive);
        }
    }
}