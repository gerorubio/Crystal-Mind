using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour {
    // 200ms 5 ticks per second
    private const float TICK_TIMER_MAX = .2f;
    
    private int tick;
    private float tickTimer;

    // event
    public class OnTickEventArgs : EventArgs {
        public int tick;
    }

    public static event EventHandler<OnTickEventArgs> OnTick;

    private void Awake() {
        tick = 0;
    }

    private void Update() {
        tickTimer += Time.deltaTime;

        if(tickTimer >= TICK_TIMER_MAX ) {
            tickTimer -= TICK_TIMER_MAX;
            tick++;
            if(OnTick != null) OnTick(this, new OnTickEventArgs { tick = tick });
        }
    }
}
