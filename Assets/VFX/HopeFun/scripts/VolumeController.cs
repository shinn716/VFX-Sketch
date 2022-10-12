using System.Collections;
using System.Collections.Generic;
using Kino.PostProcessing;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using DG.Tweening;

public class VolumeController : MonoBehaviour
{
    public VolumeProfile mVolumeProfile;

    private Glitch mGlitch;
    private Bloom mBloom;
    private float value1 = 0;
    private float value2 = 0;
    private float value3 = 0;


    private void Start()
    {
        for (int i = 0; i < mVolumeProfile.components.Count; i++)
        {
            if (mVolumeProfile.components[i].name == "Glitch")
                mGlitch = (Glitch)mVolumeProfile.components[i];
            else if (mVolumeProfile.components[i].name == "Bloom")
                mBloom = (Bloom)mVolumeProfile.components[i];
        }
    }
    private void OnDisable()
    {
        ClampedFloatParameter intensity1 = mBloom.intensity;
        intensity1.value = .5f;
        ClampedFloatParameter intensity2 = mGlitch.block;
        intensity2.value = 0;
        ClampedFloatParameter intensity3 = mGlitch.drift;
        intensity3.value = 0;
    }

    [ContextMenu("SetBloom")]
    public void SetBloom()
    {
        DOTween.To(() => value3, x => value3 = x, .75f, 1).SetEase(Ease.OutSine)
        .OnUpdate(() =>
        {
            ClampedFloatParameter intensity = mBloom.intensity;
            intensity.value = value3;
        }).OnComplete(() =>
        {
            DOTween.To(() => value3, x => value3 = x, .3f, .75f).SetEase(Ease.OutSine)
            .OnUpdate(() =>
            {
                ClampedFloatParameter intensity = mBloom.intensity;
                intensity.value = value3;
            });
        });
    }

    [ContextMenu("SetGlitch")]
    public void SetGlitch()
    {
        DOTween.To(() => value1, x => value1 = x, .5f, 3).SetEase(Ease.OutExpo)
        .OnUpdate(() =>
        {
            ClampedFloatParameter intensity = mGlitch.block;
            intensity.value = value1;
        });

        DOTween.To(() => value2, x => value2 = x, .25f, 3).SetEase(Ease.OutExpo)
        .OnUpdate(() =>
        {
            ClampedFloatParameter intensity = mGlitch.drift;
            intensity.value = value2;
        });
    }
}
