using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text HealthText;
    private PlayerHealthComponet HealthComponet;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerEvents.OnPlayerHealthSet += OnPlayerHealthSet;
    }

    private void OnPlayerHealthSet(PlayerHealthComponet healthComponet)
    {
        HealthComponet = healthComponet;
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthComponet)
        {
            HealthText.text = HealthComponet.Health.ToString();
        }
    }
}
