using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SettingsData
{
    public bool m_halo = true;
    public bool m_reticle = true;
    public bool m_fullscreen = true;
    public int m_volume = 100;
    public int m_resolution = 0;
    public int m_aa = 3;
    public int m_qGraphic = 0;
    public int m_qShadow = 0;
    public int m_qTexture = 0;
    public int m_qLanguage = 0;
}


public class Settings : MonoBehaviour
{
    SettingsData m_settingsData;

    Vector2[] m_resArray = new Vector2[3];

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        m_settingsData = new SettingsData();
        m_resArray[0] = new Vector2(1280, 720);
        m_resArray[1] = new Vector2(1366, 768);
        m_resArray[2] = new Vector2(1920, 1080);
    }

    string m_filename = Application.persistentDataPath + "/Player.settings";
    private static Settings _instance;
    public static Settings Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(Settings))as Settings;
                if(!_instance)
                {
                    GameObject go = new GameObject("Settings");
                    _instance = go.AddComponent <Settings>();
                }
            }
            return _instance;
        }
    }

    public SettingsData resetSettings()
    {
        m_settingsData = null;
        m_settingsData = new SettingsData();
        return m_settingsData;
    }
    void loadSettings()
    {
        if(File.Exists(m_filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_filename, FileMode.Open);
            SettingsData sData =(SettingsData) bf.Deserialize(file);
            file.Close();
            applySettings(sData,false);
        }
    }

    public void applySettings(SettingsData settings, bool save=true)
    {
        if(m_settingsData.m_aa != settings.m_aa)
        {
            QualitySettings.antiAliasing = settings.m_aa;
        }
        if(m_settingsData.m_fullscreen != settings.m_fullscreen)
        {
            Screen.fullScreen = settings.m_fullscreen;
        }
        if(m_settingsData.m_halo != settings.m_halo)
        {
            //change halo
        }
        if(m_settingsData.m_qGraphic != settings.m_qGraphic)
        {
            //preset qualitée graphique
        }
        if(m_settingsData.m_qLanguage != settings.m_qLanguage)
        {
            //change language
        }
        if(m_settingsData.m_qShadow != settings.m_qShadow)
        {
            switch(settings.m_qShadow)
            {
                case 0:
                    break;
                default:
                    break;
            }
        }
        if(m_settingsData.m_qTexture != settings.m_qTexture)
        {
            switch(settings.m_qTexture)
            {
                case 0:
                    QualitySettings.masterTextureLimit = 0;
                    break;
                case 1:
                    QualitySettings.masterTextureLimit = 1;
                    break;
                case 2:
                    QualitySettings.masterTextureLimit = 2;
                    break;
                default:
                    QualitySettings.masterTextureLimit = 0;
                    break;
            }
        }
        if(m_settingsData.m_reticle != settings.m_reticle)
        {
            //change reticle
        }
        if(m_settingsData.m_volume!= settings.m_volume)
        {
            //change volume
        }
        if(m_settingsData.m_resolution != settings.m_resolution )
        {
            Screen.SetResolution((int)m_resArray[settings.m_resolution].x,
                (int)m_resArray[settings.m_resolution].y, settings.m_fullscreen);
        }
        m_settingsData = settings;
        if (save)
            saveSettings();
    }

    void saveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(m_filename);
        bf.Serialize(file, m_settingsData);
        file.Close();
    }
}