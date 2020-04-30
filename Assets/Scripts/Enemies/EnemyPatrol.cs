using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _speedEnemy = 5f;
    [SerializeField] private GameObject _enemyPrefab;
    private int indexPointPath = 0;
    private bool _flagDirectionPatrolRise = true;
    private GameObject _enemy;
    private Vector3[] _enemyPath;

    private void Start()
    {
        indexPointPath = 0;
        _flagDirectionPatrolRise = true;

        EnemyPathPoint[] enemyPathPoints= transform.GetComponentsInChildren<EnemyPathPoint>();
        _enemyPath = new Vector3[enemyPathPoints.Length];
        for (int i = 0; i < _enemyPath.Length; i++)
        {
            _enemyPath[i] = enemyPathPoints[i].transform.position;
        }
        _enemy =Instantiate(_enemyPrefab, _enemyPath[0], Quaternion.identity, transform);
    }

    private void Update()
    {
        if (Vector3.Distance(_enemy.transform.position, _enemyPath[indexPointPath]) <= 0.05f)
        {
            if (_flagDirectionPatrolRise)
            {
                indexPointPath++;
                if(indexPointPath >= _enemyPath.Length)
                {
                    indexPointPath = _enemyPath.Length - 2;
                    _flagDirectionPatrolRise = false;
                }
            }
            else
            {
                indexPointPath--;
                if (indexPointPath < 0)
                {
                    indexPointPath = 1;
                    _flagDirectionPatrolRise = true;
                }
            }
        }
        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _enemyPath[indexPointPath], Time.deltaTime * _speedEnemy);
    }
}