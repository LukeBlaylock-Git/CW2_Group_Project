using TMPro;
using UnityEngine;

public class Lives_UI : MonoBehaviour
{
    TextMeshProUGUI Text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Text = GetComponent<TextMeshProUGUI>();

        if(Text == null)
        {
            Debug.LogError("Lives_UI: No TMPGUI found on this object");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) return;
        if (Text == null) return;

        Text.text = "Lives " + GameManager.Instance.Lives; //Updating the lives.
    }
}
