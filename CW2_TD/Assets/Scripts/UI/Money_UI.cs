using TMPro;
using UnityEngine;

public class Money_UI : MonoBehaviour
{
    TextMeshProUGUI Text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Text = GetComponent<TextMeshProUGUI>();

        if (Text == null)
        {
            Debug.LogError("Money_UI: No TMPGUI found on this object");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) return;
        if (Text == null) return;

        Text.text = "Money: " + GameManager.Instance.Money; //Updating the lives.
    }
}
