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
        public Action<float> OnMusicVolumeChanged;
        public Action<float> OnSFXVolumeChanged;

        private float musicVolume;
        private float sfxVolume;
        private void Start()
        {
            musicBar.value = 1f;
            sfxBar.value = 1f;

            

        }
        public void OnMusicBarValueChanged()
        {
            musicVolume = musicBar.value;
            OnMusicVolumeChanged(musicVolume);
            
        }
        public void OnSFXBarValueChanged()
        {
            sfxVolume = sfxBar.value;
            OnSFXVolumeChanged(sfxVolume);
        }

        /*public void OnToggleMusicSelected()
        {
            if (tgMusic.isOn)
            {
                musicBar.gameObject.SetActive(true);
                OnMusicVolumeChanged(musicVolume);
            }
            else
            {
                musicBar.gameObject.SetActive(false);
                OnMusicVolumeChanged(0f);
            }

        }
        public void OnToggleSFXSelected()
        {
            if (tgSFX.isOn)
            {
                sfxBar.gameObject.SetActive(true);
                OnSFXVolumeChanged(sfxVolume);
            }
            else
            {
                sfxBar.gameObject.SetActive(false);
                OnSFXVolumeChanged(0f);
            }
        }*/

        public void BtnBackPressed()
        {
            GameController.Instance.Pause();
        }
    }
}
