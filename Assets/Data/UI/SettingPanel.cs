using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace EarthDenfender
{
    public class SettingPanel : MonoBehaviour
    {
        private static SettingPanel instance;
        public static SettingPanel Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<SettingPanel>();
                return instance;
            }
        }
        [SerializeField] private Scrollbar musicBar;
        [SerializeField] private Scrollbar sfxBar;
        [SerializeField] private Toggle tgMusic;
        [SerializeField] private Toggle tgSFX;
        public Action<float> onMusicVolumeChanged;
        public Action<float> onSFXVolumeChanged;

        private float musicVolume;
        private float sfxVolume;
        private float lastMusicVolume;
        private float lastSFXVolume;
        private void OnEnable()
        {
            musicBar.value = 1f;
            sfxBar.value = 1f;
        }
        public void OnMusicBarValueChanged()
        {
            musicVolume = musicBar.value;
            if (onMusicVolumeChanged != null)
                onMusicVolumeChanged(musicVolume);
            lastMusicVolume = musicVolume;
            AudioManager.Instance.DisPlayMusic(musicVolume);

        }
        public void OnSFXBarValueChanged()
        {
            sfxVolume = sfxBar.value;
            if (onSFXVolumeChanged != null)
                onSFXVolumeChanged(sfxVolume);
            lastSFXVolume = sfxVolume;
            AudioManager.Instance.DisPlayMusic(sfxVolume);
        }

        public void OnToggleMusicChanged()
        {
            if (!tgMusic.isOn)
            {
                PlayerPrefs.SetInt("MusicVolumeKey", 0);
                musicBar.gameObject.SetActive(false);
            }
            else
            {
                PlayerPrefs.SetInt("MusicVolumeKey", 1);
                musicBar.gameObject.SetActive(true);
            }
            AudioManager.Instance.DisPlayMusic(lastMusicVolume);
        }
        public void OnToggleSFXChanged()
        {
            if (!tgSFX.isOn)
            {
                PlayerPrefs.SetInt("SFXVolumeKey", 0);
                sfxBar.gameObject.SetActive(false);
            }
            else
            {
                PlayerPrefs.SetInt("SFXVolumeKey", 1);
                sfxBar.gameObject.SetActive(true);
            }
            AudioManager.Instance.DisplaySFX(lastSFXVolume);
        }
        public void BtnBackPressed()
        {
            int key = PlayerPrefs.GetInt("SettingKey");
            if (key == 1)
                GameController.Instance.Home();
            else if (key == 2)
                GameController.Instance.Pause();
        }
    }

}
