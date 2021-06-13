using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HelpPanel : MonoBehaviour
{
    private bool state = false;
    public GameObject helpmenu;
    private bool ispause = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (state == true)
            {
                helpmenu.SetActive(false);
                state = false;
            }
            else
            {
                helpmenu.SetActive(true);
                state = true;
            }
        }
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void pause()
    {
        if(ispause == true)
        {
            Time.timeScale = 1f;
            ispause = false;
        }
        else
        {
            Time.timeScale = 0f;
            ispause = true;
        }
    }
    public void LoadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
