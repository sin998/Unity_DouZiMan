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
    //混音器
    public AudioMixer masterMixer;
    //得分UI
    public Text scoreText;
    //当前分数
    private static float score = 0.0f;

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

    public void addScore()
    {
        score += 100.0f;
        updateScore();
    }

    private void updateScore()
    {
        scoreText.text = "得分：" + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
