using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLaunchCounter : MonoBehaviour, IDataPersistence
{
    private int LaunchCount = 0;

    public TextMeshProUGUI LaunchCountText;
    
    void Awake()
    {
        LaunchCountText = this.GetComponent<TextMeshProUGUI>();
    }

    public void LoadData(GameData data)
    {
        this.LaunchCount = data.LaunchCount;
    }

    public void SaveData(ref GameData data)
    {
        data.LaunchCount = this.LaunchCount;
    }

    void Start()
    {
        this.LaunchCount++;
        UpdateText();
    }

    void UpdateText()
    {
        LaunchCountText.text = "Launches: " + this.LaunchCount;
    }
}
