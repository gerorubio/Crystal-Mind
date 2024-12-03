using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public EnemySO enemySO;

    public float currentHp;

    public GameObject alceanistum;
    private GameObject alceanistumContainer;

    private NavMeshAgent agent;

    // Effect counter and flags
    // Burned
    public bool IsBurned { get; set; } = false;
    // Freeze
    private bool isFrozen = false;
    private int freezeStacks = 0;
    private Coroutine frozenCoroutine;
    // Bleed
    private bool isBleeding = false;
    private int bleedStacks = 0;
    private Coroutine bleedCoroutine;
    // Poison
    private bool isPoisoned = false;
    private int poisonStacks = 0;
    private Coroutine poisonCoroutine;

    void Start() {
        if(enemySO == null) {
            Debug.LogError("Error: No EnemySO assigned to " + gameObject.name);
            return;
        }

        currentHp = enemySO.hp;

        alceanistumContainer = GameObject.Find("Alceanistum Container");

        agent = transform.parent.GetComponent<NavMeshAgent>();

        if(alceanistumContainer == null) {
            Debug.Log("Error: No alceanistum container found");
        }
    }

    void Update() {

    }

    public void TakeDamage(float damage) {
        currentHp = Mathf.Max(currentHp - damage, 0);
        if(currentHp <= 0) {
            Die();
        }
    }

    private void Die() {
        ThrowXP();
        Destroy(gameObject);
    }

    private void ThrowXP() {
        if (alceanistum != null) {
            Instantiate(alceanistum, transform.position, Quaternion.Euler(90f, 0, 0));
        } else {
            Debug.LogWarning("Alceanistum prefab is not assigned.");
        }
    }

    public void ApplyBurnedEffect() {
        IsBurned = true;
    }

    public void ApplyFrozenEffect() {
        if(freezeStacks < 3) {
            freezeStacks++;
        }

        if(freezeStacks == 3 && !isFrozen) {
            isFrozen = true;
            frozenCoroutine = StartCoroutine(ProcessFreeze());
        }
    }

    public void ApplyBleedEffect(int value) {
        bleedStacks += value;
        if(!isBleeding) {
            isBleeding = true;
            bleedCoroutine = StartCoroutine(ProcessBleed());
        }
    }

    public void ApplyPoisonEffect(int value) {
        poisonStacks += value;
        if(!isPoisoned) {
            isPoisoned = true;
            poisonCoroutine = StartCoroutine(ProcessPoison());
        }
    }

    private IEnumerator ProcessFreeze() {
        // Remove freeze effect after 1.5 seconds
        agent.isStopped = true;

        yield return new WaitForSeconds(1.5f);
        isFrozen = false;
        freezeStacks = 0;

        agent.isStopped = false;
    }

    private IEnumerator ProcessBleed() {
        // Take damage every 0.5 seconds then reduce stacks minus 1
        while (bleedStacks > 0) {
            TakeDamage(bleedStacks);

            bleedStacks--;

            yield return new WaitForSeconds(0.25f);
        }
        isBleeding = false;
        bleedCoroutine = null;
    }

    private IEnumerator ProcessPoison() {
        // Take damage every 3 seconds the reduce the stack at half
        while(poisonStacks > 20) {
            TakeDamage(poisonStacks);

            poisonStacks /= 2;

            if(poisonStacks < 20) {
                poisonStacks = 0;
            } else {
                yield return new WaitForSeconds(3f);
            }
        }
        isPoisoned = false;
        poisonCoroutine = null;
    }
}