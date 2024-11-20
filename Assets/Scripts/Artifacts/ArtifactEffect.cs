using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArtifactEffect : ScriptableObject {
    public virtual void OnEquip(Character player, Weapon weapon, Face face) { }
    public virtual void OnShoot(Character player, Weapon weapon, Dice dice, Projectile projectile) { }
    public virtual void OnReload(Character player, Weapon weapon) { }
    public virtual void OnSpellCast(Character player) { }
    public virtual void OnTakeDamage(Character player) { }
    public virtual void OnDash(Character player) { }
}