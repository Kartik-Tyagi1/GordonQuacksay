using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliverFailed;
    public AudioClip[] deliverSuccess;
    public AudioClip[] footsteps;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] trash;
    public AudioClip[] warning;
    public AudioClip stoveSizzle;
}
