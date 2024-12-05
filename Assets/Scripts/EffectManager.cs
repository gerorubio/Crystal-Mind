using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {
    public ParticleSystem burnedParticle;
    public ParticleSystem freezeParticle;
    public ParticleSystem bleedParticle;
    public ParticleSystem poisonParticle;

    public void EnableEffect(string effect) {
        ParticleSystem particle = GetParticle(effect);
        if(particle != null) {
            particle.Play();
        }
    }

    public void DisableEffect(string effect) {
        ParticleSystem particle = GetParticle(effect);
        if (particle != null) {
            particle.Stop();
        }
    }

    public void DisableAllEffects() {
        if(burnedParticle != null) {
            burnedParticle.Stop();
        }
        if (freezeParticle != null) {
            freezeParticle.Stop();
        }
        if (bleedParticle != null) {
            bleedParticle.Stop();
        }
        if (poisonParticle != null) {
            poisonParticle.Stop();
        }
    }

    private ParticleSystem GetParticle(string effect) {
        switch(effect) {
            case "burn":
                return burnedParticle;
            case "freeze":
                return freezeParticle;
            case "poison":
                return poisonParticle;
            case "bleed":
                return bleedParticle;
            default:
                Debug.LogError("Effect not recognize");
                return null;
        }
    }
}
