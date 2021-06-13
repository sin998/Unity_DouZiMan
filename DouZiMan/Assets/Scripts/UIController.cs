using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //暂停游戏btn
    public Button pauseGameBtn;
    //声音的Silder
    public Slider masterVolumeSlider;
    //是否暂停游戏
    private bool isPaused;
    public AudioMixer masterMixer;

    // Start is called before the first frame update
    void Start()
    {
        pauseGameBtn.onClick.AddListener(PauseGame);
        masterVolumeSlider.onValueChanged.AddListener(VolumeChange);
    }

    void VolumeChange(float volume)
    {
        masterMixer.SetFloat("MasterVolume", volume);
    }

    void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
