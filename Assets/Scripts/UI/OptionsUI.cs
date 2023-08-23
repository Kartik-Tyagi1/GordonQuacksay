using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button exitOptionsButton;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;

        sfxButton.onClick.AddListener(() =>
        {
            SoundManger.Instance.ChangeVolume();
            UpdateSFXVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateMusicVisual();
        });

        exitOptionsButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        UpdateSFXVisual();
        UpdateMusicVisual();
        Hide();

        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;
    }

    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateSFXVisual()
    {
        sfxText.text = $"SFX VOLUME: {Mathf.Round(SoundManger.Instance.GetVolume() * 10f)}";
    }

    private void UpdateMusicVisual()
    {
        musicText.text = $"MUSIC VOLUME: {Mathf.Round(MusicManager.Instance.GetVolume() * 10f)}";
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
