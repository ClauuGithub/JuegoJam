using UnityEngine;
using UnityEngine.Audio;

public class MusicSlider : MonoBehaviour
{
    public AudioMixer mixer;

    public void SliderControl(float slider)
    {
        mixer.SetFloat("GlobalVolume", Mathf.Log10(slider) * 20);
    }
}

