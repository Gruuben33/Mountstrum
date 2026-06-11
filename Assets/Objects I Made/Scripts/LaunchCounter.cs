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
        LaunchCount++;
    }

    public void LoadData(GameData data)
    {
        this.LaunchCount = data.launchCount;
    }

    public void SaveData(ref GameData data)
    {
        data.launchCount = this.LaunchCount;
    }

    void Start()
    {
        UpdateText();
    }

    void UpdateText()
    {
        LaunchCountText.text = "Launches: " + LaunchCount;
    }
}
