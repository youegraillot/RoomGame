using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class SettingsController : MonoBehaviour
{



    enum UICompName
    {
        GraphicsQual,
        Resolution,
        FullScreen,
        AA,
        ShadowQuality,
        TextureQuality,
        Language,
        Halo,
        Reticle,
        Volume
    }
    //1 : true , 2 : false
    public Toggle fullScreen;
    public Toggle halo;
    public Toggle reticle;

    public Slider volume;//0-100

    public Dropdown graphicsQuality;//value 0 : Ultra 1 : normale 2: basse
    public Dropdown resolution;
    public Dropdown antiAliasing;
    public Dropdown shadowQuality;
    public Dropdown textureQuality;
    public Dropdown language;

    SettingsData m_settings=new SettingsData();
    #region UIEvents
    void Start()
    {
        graphicsQuality.onValueChanged.AddListener(delegate { ValueChange(graphicsQuality.value, UICompName.GraphicsQual); });
        resolution.onValueChanged.AddListener(delegate { ValueChange(resolution.value, UICompName.Resolution); });
        antiAliasing.onValueChanged.AddListener(delegate { ValueChange(antiAliasing.value, UICompName.AA); });
        shadowQuality.onValueChanged.AddListener(delegate { ValueChange(shadowQuality.value, UICompName.ShadowQuality); });
        textureQuality.onValueChanged.AddListener(delegate { ValueChange(textureQuality.value, UICompName.TextureQuality); });
        language.onValueChanged.AddListener(delegate { ValueChange(language.value, UICompName.Language); });
        halo.onValueChanged.AddListener(delegate { ValueChange(Convert.ToInt32(halo.isOn), UICompName.Halo); });
        fullScreen.onValueChanged.AddListener(delegate { ValueChange(Convert.ToInt32(fullScreen.isOn), UICompName.FullScreen); });
        reticle.onValueChanged.AddListener(delegate { ValueChange(Convert.ToInt32(reticle.isOn), UICompName.Reticle); });
        volume.onValueChanged.AddListener(delegate { ValueChange(Convert.ToInt32(volume.value), UICompName.Volume); });
    }

    void ValueChange(int value, UICompName name)
    {
        //Debug.Log(value+" dsf" + name);
        switch (name)
        {
            case UICompName.GraphicsQual:
                break;
            case UICompName.Resolution:
                break;
            case UICompName.FullScreen:
                break;
            case UICompName.AA:
                break;
            case UICompName.ShadowQuality:
                break;
            case UICompName.TextureQuality:
                break;
            case UICompName.Language:
                break;
            case UICompName.Halo:
                break;
            case UICompName.Reticle:
                break;
            case UICompName.Volume:
                break;
            default:
                break;
        }
    }


    public void ApplyChange()
    {
        Settings.Instance.ApplySettings(m_settings);
    }
    void OnEnable()
    {
        ResetSettings();
    }
    public void ResetSettings()
    {
        m_settings = Settings.Instance.ResetSettings();
        antiAliasing.value = m_settings.m_aa;
        graphicsQuality.value = m_settings.m_qGraphic;
        resolution.value = m_settings.m_resolution;
        language.value = m_settings.m_qLanguage;
        volume.value = m_settings.m_volume;
        shadowQuality.value = m_settings.m_qShadow;
        textureQuality.value = m_settings.m_qTexture;

        fullScreen.isOn = m_settings.m_fullscreen;
        halo.isOn = m_settings.m_halo;
        reticle.isOn = m_settings.m_reticle;
    }

    public void Cancel()
    {

    }
    #endregion
}
