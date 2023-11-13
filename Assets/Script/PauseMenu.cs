using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Toggle m_MenuToggle;
    private Toggle m_MapToggle;
    private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    private bool m_Paused;

    public GameObject minimap;


    void Awake()
    {
        m_MenuToggle = GetComponent<Toggle>();
        m_MapToggle = minimap.GetComponentInChildren<Toggle>();
    }


    private void MenuOn()
    {
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        m_VolumeRef = AudioListener.volume;
        AudioListener.volume = 0f;

        m_Paused = true;
        minimap.gameObject.SetActive(false);
    }


    public void MenuOff()
    {
        Time.timeScale = m_TimeScaleRef;
        AudioListener.volume = m_VolumeRef;
        m_Paused = false;
        minimap.gameObject.SetActive(true);
        if (m_MapToggle.isOn)
        {
            m_MapToggle.isOn = false;
        }
    }


    public void OnMenuStatusChange()
    {
        if (m_MenuToggle.isOn && !m_Paused)
        {
            MenuOn();
        }
        else if (!m_MenuToggle.isOn && m_Paused)
        {
            MenuOff();
        }
    }
}