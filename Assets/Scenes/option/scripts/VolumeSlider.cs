using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private void Start()
    {
        var slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.SetValueWithoutNotify(MusicPlayer.SavedVolume);
        slider.onValueChanged.AddListener(v =>
        {
            if (MusicPlayer.Instance != null) MusicPlayer.Instance.SetVolume(v);
        });
    }
    //todo rajouter le changement de volume musique et effets sonnores dans OptionsMenumanager
}