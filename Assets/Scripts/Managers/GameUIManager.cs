using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIManager : MonoBehaviour {

    private Canvas gameUI;
    private Text timeLeftInWaveText;
    private Text waveNumber;

    // Use this for initialization
    void Start ()
    {
        GameObject UIObject = GameObject.FindGameObjectWithTag("GameUI");
        if (UIObject == null)
        {
            Debug.LogError("Game UI game object could not be found.");
        }
        gameUI = UIObject.GetComponent<Canvas>();

        foreach (Text textComponent in UIObject.GetComponentsInChildren<Text>())
        {
            switch (textComponent.gameObject.name)
            {
                case "TimeLeftInWave":
                    timeLeftInWaveText = textComponent;
                    break;
                case "WaveNumber":
                    waveNumber = textComponent;
                    break;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void SetTimeLeftInWaveText(string text)
    {
        timeLeftInWaveText.text = text;
    }

    public void SetWaveNumber(string text)
    {
        waveNumber.text = text;
    }
}
