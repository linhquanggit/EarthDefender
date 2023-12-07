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
            SettingPanel.Instance.onMusicVolumeChanged += OnMusicVolumeChanged;
            SettingPanel.Instance.onSFXVolumeChanged += OnSFXVolumeChanged;
        }
        private void OnDisable()
        {
            SettingPanel.Instance.onMusicVolumeChanged -= OnMusicVolumeChanged;
            SettingPanel.Instance.onSFXVolumeChanged -= OnSFXVolumeChanged;
        }
        private void OnMusicVolumeChanged(float volume)
        {
            music.volume = volume;
        }

        public void DisPlayMusic(float volume)
        {
            if (PlayerPrefs.GetInt("VolumeKey") == 1)
                OnMusicVolumeChanged(volume);
            else if (PlayerPrefs.GetInt("VolumeKey") == 0)
                OnMusicVolumeChanged(0f);
        }    
        public void DisplaySFX(float volume)
        {
            if (PlayerPrefs.GetInt("VolumeKey") == 1)
                OnSFXVolumeChanged(volume);
            else if (PlayerPrefs.GetInt("VolumeKey") == 0)
                OnSFXVolumeChanged(0f);
        }    
        private void OnSFXVolumeChanged(float volume)
        {
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