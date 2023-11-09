using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUndead : MonoBehaviour
{
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private EnemyEntity _enemyEntity;
    [SerializeField] private float _timeToResurrect = 30f;
    [SerializeField] private int _resChances = 1;
    private int _timesResed = 0;
    private bool canRes;

    private float _deathTime = 0f;
    private void Start()
    {
        canRes = _resChances > 0;
    }

    private void OnEnable()
    {
        _enemyEntity.OnDeath += ThisDies;
    }
    private void OnDisable()
    {
        _enemyEntity.OnDeath -= ThisDies;
    }

    private void ThisDies()
    {
        _deathTime = Time.time;
    }

    void Update()
    {
        if (canRes && !_enemyEntity.isAlive)
        {
            if (Time.time - _deathTime > _timeToResurrect)
            {
                Resurrect();
            }
        }
    }

    void Resurrect()
    {
        _timesResed++;
        canRes = _timesResed < _resChances;
        _enemyEntity.Resurrect();
    }
}
