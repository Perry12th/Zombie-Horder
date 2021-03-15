using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health;

public class PlayerHealthComponet : HealthCompoment
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerEvents.Invoke_OnPlayerHealthSet(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
