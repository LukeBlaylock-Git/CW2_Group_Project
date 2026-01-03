using TMPro;
using UnityEngine;

public class Waves_UI : MonoBehaviour
{
    TextMeshProUGUI Text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveManager.Instance == null) return;
        Text.text = "Wave: " + (WaveManager.Instance.CurrentWave + 1);
    }
}

