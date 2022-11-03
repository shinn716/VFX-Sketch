using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonLightEFX : MonoBehaviour
{
    public Renderer render;

    float timer;
    float limit = .5f;

    // Start is called before the first frame update
    void Start()
    {
        //UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(render.material);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > limit)
        {
            limit = Random.Range(.5f, 1);
            timer = 0;
            UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(render.material);
        }
    }

    public static void UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(Material material)
    {
        const string kEmissiveColorLDR = "_EmissiveColorLDR";
        const string kEmissiveColor = "_EmissiveColor";
        const string kEmissiveIntensity = "_EmissiveIntensity";

        //print(material.GetFloat(kEmissiveIntensity));

        material.SetFloat(kEmissiveIntensity, Random.Range(5, 50));

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
