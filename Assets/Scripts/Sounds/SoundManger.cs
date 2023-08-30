using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger Instance { get; private set; }

    [SerializeField] AudioClipRefsSO audioClipRefsSO;
    private float volume = 1f;
    private const string PLAYER_PREFS_SFX_VOLUME = "SFXVolume";

    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, .5f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecepieSuccess += DeliveryManager_OnRecepieSuccess;
        DeliveryManager.Instance.OnRecepieFailed += DeliveryManager_OnRecepieFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedUpKitchenObject += Player_OnPickedUpKitchenObject;
        BaseCounter.OnKitchenObjectPlacedOnCounter += BaseCounter_OnKitchenObjectPlacedOnCounter;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }

    // Play Trash Sound
    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    // Play Object Placed on counter sound
    private void BaseCounter_OnKitchenObjectPlacedOnCounter(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    // Play object picked up sound
    private void Player_OnPickedUpKitchenObject(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    // Play chopping sound
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    // Play Incorrect recipie sound
    private void DeliveryManager_OnRecepieFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverFailed, DeliveryCounter.Instance.transform.position);
    }

    // Play Correct Recipie sound
    private void DeliveryManager_OnRecepieSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverSuccess, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 postion, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], postion, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 postion, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, postion, volumeMultiplier * volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRefsSO.footsteps, position, volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }


}
