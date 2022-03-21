using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class VfxManager : MonoBehaviour
{
    public GameObject asset;
    public GameObject ps;
    public ParticleSystem particleSystem;
    public Slider slider;
    public Slider explosionSlider;
    public Text fpsDisplay;
    public float time;
    public GameObject explosion;
    public float explosionSpawnRate = 1f;

    private bool _isAnimation = false;
    private bool _isParticle = false;
    
    public void AssetToggle()
    {
        if (_isAnimation)
        {
            asset.SetActive(false);
            _isAnimation = false;
        }
        else if (!_isAnimation)
        {
            asset.SetActive(true);
            _isAnimation = true;
        }
    }

    public void PsToggle()
    {
        if (_isParticle)
        {
            ps.SetActive(false);
            _isParticle = false;
        }
        else if (!_isParticle)
        {
            ps.SetActive(true);
            _isParticle = true;
        }
    }

    public void SliderValue()
    {
        var em = particleSystem.emission;
        em.rateOverTime = slider.value;
    }
    
    public void ExplosionSliderValue()
    {
        explosionSpawnRate = explosionSlider.value;
    }

    private void Start()
    {
        _isAnimation = true;
        _isParticle = true;

        slider.minValue = 1f;
        slider.maxValue = 500f;

        StartCoroutine(ExplosionEffects());
    }

    private void Update()
    {
        time += (Time.deltaTime - time) * 0.1f;
        float fps = 1.0f / time;
        fpsDisplay.text = $"Fps: {Mathf.Ceil(fps).ToString()}";
    }

    private IEnumerator ExplosionEffects()
    {
        while (true)
        {
            Vector3 random = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
            Instantiate(explosion, random, Quaternion.identity);

            yield return new WaitForSeconds(explosionSpawnRate);
        }
    }
}
