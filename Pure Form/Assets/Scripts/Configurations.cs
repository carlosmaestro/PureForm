using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Configurations : MonoBehaviour {

    public Slider volumeMasterSlider;
    public Text valueVolumeMasterText;
    public GameObject camera;
    AudioListener audioCamera;
    public float valueVolumeMaster =0.5f;
	// Use this for initialization
	void Start () {
        //camera = GameObject.FindGameObjectWithTag("MainCamera");
        audioCamera = camera.GetComponent<AudioListener>();
        if (PlayerPrefs.HasKey("VolumeMaster"))
        {
            valueVolumeMaster = PlayerPrefs.GetFloat("VolumeMaster");

            
        }
        if (volumeMasterSlider != null)
            volumeMasterSlider.value = valueVolumeMaster;
        if (valueVolumeMaster != null)
        {
            
            if (valueVolumeMaster <= 1 && valueVolumeMaster >= 0)
            {
                AudioListener.volume = valueVolumeMaster;
            }
            else
            {
                AudioListener.volume = 0.5f;
            }
        }
        else
        {
            AudioListener.volume = 0.5f;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetVolumeMaster()
    {
        valueVolumeMaster = volumeMasterSlider.value;
        AudioListener.volume = valueVolumeMaster;
        PlayerPrefs.SetFloat("VolumeMaster", valueVolumeMaster);
        valueVolumeMasterText.text = "" + valueVolumeMaster * 10;
        PlayerPrefs.Save();
    }
}
