using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HorrorGame
{
    [RequireComponent(typeof(Light))]
    [RequireComponent(typeof(AudioSource))]
    public class LightSwitch : MonoBehaviour
    {
        [SerializeField] private AudioClip switchSound1;
        [SerializeField] private AudioClip switchSound2;
        [SerializeField] private AudioClip batteryChangeSound;

        private Light _light;
        private AudioSource _audioSource;

        public Image batteryAmount;

        public float maxEnergy = 100f;
        public float energy = 100f;
        public float energyDecreaseSpeed = 0.1f;

        void Awake()
        {
            energy = maxEnergy;
            _light = GetComponent<Light>();
            _audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_light.enabled && energy > 0)
            {
                energy -= Mathf.Clamp(energyDecreaseSpeed * Time.deltaTime, 0f, 100f);
            }
            else if(energy <= 0.1f){
                energy = 0;
            }

            if (energy > 10)
            {
                _light.intensity = 1e+07f;
            }
            else if (energy <= 10 && energy >0f)
            {
                _light.intensity = 1e+06f;
            }
            else if (energy == 0f)
            {
                _light.intensity = 0f;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _light.enabled = !_light.enabled;
                PlaySwitchSound();
            }

            if (Input.GetKeyDown(KeyCode.R)) {
                if (energy != maxEnergy)
                {
                    bool hasBattery = false;
                    foreach (Item item in Inventory.Instance.items)
                    {
                        if (item is BatteryItem)
                        {
                            if (item.Count == 0)
                            {
                                MessageManager.Instance.ShowMessage("Don't have battery");
                            }
                            else {
                                _audioSource.clip = batteryChangeSound;
                                _audioSource.Play();
                                hasBattery = true;
                                item.Count -= 1;
                                energy = maxEnergy;
                                if (Inventory.Instance.onItemChangedCallBack != null)
                                {
                                    Inventory.Instance.onItemChangedCallBack.Invoke();
                                }
                                
                            }
                        }
                    }
                    if (!hasBattery) {
                        MessageManager.Instance.ShowMessage("Don't have battery");
                    }
                }
                else {
                    MessageManager.Instance.ShowMessage("Light battery is full");
                }
            }
        }

        private void FixedUpdate()
        {
            batteryAmount.rectTransform.localScale =new Vector3(energy / maxEnergy,1,1);
            batteryAmount.color = new Color(Mathf.Clamp01((1-energy / maxEnergy)*2), Mathf.Clamp01((energy / maxEnergy)*2), batteryAmount.color.b);
        }

        private void PlaySwitchSound()
        {
            if (_light.enabled)
            {
                _audioSource.clip = switchSound1;
            }
            else
            {
                _audioSource.clip = switchSound2;
            }
            _audioSource.Play();
        }
    }
}

