using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EarthDenfender
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<AudioManager>();
                return instance;
            }
        }
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource sfx;
       // [SerializeField] private AudioSource echo;

        [SerializeField] AudioClip homeMusicClip;
        [SerializeField] AudioClip battleMusicClip;

        [SerializeField] AudioClip rocketSFXClip;
        [SerializeField] AudioClip bombSFXClip;
        [SerializeField] AudioClip hitSFXClip;
        [SerializeField] AudioClip destroySFXClip;
        [SerializeField] AudioClip gameOverSFXClip;

        private void OnEnable()
        {
            SettingPanel.Instance.OnMusicVolumeChanged += SetMusicVolume;
            SettingPanel.Instance.OnSFXVolumeChanged += SetSFXVolume;
        }
        private void OnDisable()
        {
            SettingPanel.Instance.OnMusicVolumeChanged -= SetMusicVolume;
            SettingPanel.Instance.OnSFXVolumeChanged -= SetSFXVolume;
        }
        private void SetMusicVolume(float volume)
        {
            music.volume = volume;
        }

        private void SetSFXVolume(float volume)
        {
            sfx.volume = volume;
        }
        public void OnVolumeChanged(float volume)
        {
            music.volume = volume;
            sfx.volume = volume;
        }    
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }
        public void PlayHomeMusic()
        {
            if (music.clip == homeMusicClip)
                return;
            music.loop = true;
            music.clip = homeMusicClip;
            music.Play();
        }
        public void PlayBattleMusic()
        {
            if (music.clip == battleMusicClip)
                return;
            music.loop = true;
            music.clip = battleMusicClip;
            music.Play();
        }

        public void PlayRocketSFXClip()
        {
            sfx.pitch = Random.Range(1f, 2f);
            sfx.PlayOneShot(rocketSFXClip);
        }
        public void PlayBombSFXClip()
        {
            sfx.pitch = Random.Range(1f, 2f);
            sfx.PlayOneShot(bombSFXClip);
        }
        public void PlayDestroySFXClip()
        {
            sfx.PlayOneShot(destroySFXClip);
        }
        public void PlayHitSFXClip()
        {
            sfx.pitch = Random.Range(1f, 2f);
            sfx.PlayOneShot(hitSFXClip);
        }
        public void PlayGameOverSFXClip()
        {
            sfx.pitch = 2f;
            sfx.PlayOneShot(gameOverSFXClip);
        }
    }

}