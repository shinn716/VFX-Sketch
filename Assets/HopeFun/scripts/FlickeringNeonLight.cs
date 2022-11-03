using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringNeonLight : MonoBehaviour
{
    // Light instance //    
    //private Light neonLight;
    // The original full intensity of the light //
    private float fullIntensity;
    // Last timestamp of the light state change //
    private float lastStateChangedTime;
    // How much current state is going to last //
    private float currentStateDuration;
    // A random integer generator // 
    private System.Random random = new System.Random();
    // Current state of the lamp (enum we wrote before) // 
    private NeonState? state;


    [SerializeField] Vector2Int timeRange = new Vector2Int(0, 3);


    public Renderer render;

    // Start is called before the first frame update //
    void Start()
    {
        //print(render.material.GetFloat("_EmissiveIntensity"));

        //neonLight = GetComponent<Light>();
        fullIntensity = render.material.GetFloat("_EmissiveIntensity");



        lastStateChangedTime = Time.time;
        ChangeNeon(NeonState.Full_Light_On);
    }
    // Update is called once per frame //
    void Update()
    {
        if (State == null)
        {
            // Last state is over: switching to a new one //
            State = (NeonState)random.Next(0, 4);
        }

        //if (neonLight.enabled)
        //{
        //    // Light is enabled //
        //    if (State == null)
        //    {
        //        // Last state is over: switching to a new one //
        //        State = (NeonState)random.Next(0, 4);
        //    }
        //}
        //else
        //{
        //    // Light is disabled //
        //    State = NeonState.Off;
        //}
    }



    private void ChangeNeon(NeonState newState)
    {
        switch (newState)
        {
            case NeonState.Full_Light_On:
                // Full light on: back to the original full intensity //
                //neonLight.intensity = fullIntensity;
                UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(render.material, fullIntensity);
                break;
            case NeonState.Flickering:
                StartCoroutine(FlickeringCoroutine());
                break;
            case NeonState.Half_Light_On:
                UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(render.material, fullIntensity / 2f);
                //neonLight.intensity = fullIntensity / 2f;
                break;
            case NeonState.Off:
                UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(render.material, 0);
                //neonLight.intensity = 0f;
                break;
        }

        lastStateChangedTime = Time.time;
        currentStateDuration = random.Next(timeRange.x, timeRange.y);
    }
    private IEnumerator FlickeringCoroutine()
    {
        while (State == NeonState.Flickering)
        {
            UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(render.material, 0);
            //neonLight.intensity = 0f;
            yield return new WaitForSeconds(0.08f);
            UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(render.material, fullIntensity);
            //neonLight.intensity = fullIntensity;
            yield return new WaitForSeconds(0.25f);
        }
    }




    public NeonState? State
    {
        get
        {
            float differenceFromLastTime = Time.time - lastStateChangedTime;
            if (differenceFromLastTime > currentStateDuration)
                return null;
            else
                return state;
        }
        set
        {
            if (state == value)
                return;
            state = value;
            lastStateChangedTime = Time.time;
            ChangeNeon(value.Value);
        }
    }


    public enum NeonState
    {
        ///<summary>
        ///Light is at 100% power, stationary
        ///</summary>
        Full_Light_On,
        ///<summary>
        ///Light is flickering
        ///</summary>
        Flickering,
        ///<summary>
        ///Light is at 50% power, stationary
        ///</summary>
        Half_Light_On,
        ///<summary>
        ///Light is completely off
        ///</summary>
        Off
    }




    public static void UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(Material material, float _value)
    {
        const string kEmissiveColorLDR = "_EmissiveColorLDR";
        const string kEmissiveColor = "_EmissiveColor";
        const string kEmissiveIntensity = "_EmissiveIntensity";

        //print(material.GetFloat(kEmissiveIntensity));
        //material.SetFloat(kEmissiveIntensity, Random.Range(5, 50));

        material.SetFloat(kEmissiveIntensity, _value);

        if (material.HasProperty(kEmissiveColorLDR) && material.HasProperty(kEmissiveIntensity) && material.HasProperty(kEmissiveColor))
        {
            // Important: The color picker for kEmissiveColorLDR is LDR and in sRGB color space but Unity don't perform any color space conversion in the color
            // picker BUT only when sending the color data to the shader... So as we are doing our own calculation here in C#, we must do the conversion ourselves.
            Color emissiveColorLDR = material.GetColor(kEmissiveColorLDR);
            Color emissiveColorLDRLinear = new Color(Mathf.GammaToLinearSpace(emissiveColorLDR.r), Mathf.GammaToLinearSpace(emissiveColorLDR.g), Mathf.GammaToLinearSpace(emissiveColorLDR.b));
            material.SetColor(kEmissiveColor, emissiveColorLDRLinear * material.GetFloat(kEmissiveIntensity));
        }
    }


}
