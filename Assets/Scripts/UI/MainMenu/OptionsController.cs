using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Options menu layout controller
/// </summary>
public class OptionsController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Button saveButton;
    [SerializeField] private SoundSettingsEventChannelSO onSoundSettingsChange;

    private void OnEnable()
    {
        saveButton.onClick.AddListener(SaveSettings);
    }

    private void SaveSettings()
    {
        onSoundSettingsChange.RaiseEvent(musicSlider.value, soundSlider.value);
        Debug.Log(musicSlider.value);
    }
}