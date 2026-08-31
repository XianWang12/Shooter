using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string parameter;
    [SerializeField] private float multiplier;

    private Slider slider;

    private string PrefKey => $"Volume_{parameter}";

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        var value = PlayerPrefs.GetFloat(PrefKey, slider.value);
        slider.SetValueWithoutNotify(value);
        SetVolume(value);
    }

    public void SetVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat(parameter, Mathf.Log10(value) * multiplier);
        PlayerPrefs.SetFloat(PrefKey, value);
    }
}
