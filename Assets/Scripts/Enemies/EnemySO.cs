using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBehaviour {
    Mele,
    Range,
    Caster
}

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject {
    public string enemyName;
    public float hp;
    public float speed;
    public float attackCooldown;
    public float attackRange;

    public EnemyBehaviour enemyBehaviour;
    
    public int experiencePoints;

    public bool isBoss;
}