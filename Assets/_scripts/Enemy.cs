using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 1;
    public float movementSpeed;
    public bool backAndForth = false;
    public bool waitBetweenTargets = false;
    public float waitTime = 0;
    bool moving = false;
    private int _targetIndex = 0;
    private int direction = 1;
    public List<Transform> _patrolTargets = new List<Transform>();
    private List<Vector2> _patrolPositions = new List<Vector2>();
    void Start()
    {
        for (int i = 0; i < _patrolTargets.Count; i++)
        {
            _patrolPositions.Add(_patrolTargets[i].position);
        }
    }

    void Update()
    {
        if(!moving)
        {
            StartCoroutine(MovePosition());
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Transform t in _patrolTargets)
        {
            Gizmos.DrawWireSphere(t.position, .5f);
        }
    }

    IEnumerator MovePosition()
    {
        moving = true;
        while(transform.position != (Vector3)_patrolPositions[_targetIndex])
        {
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _patrolPositions[_targetIndex], step);
            yield return new WaitForSeconds(.001f);
        }

        if (waitBetweenTargets)
            yield return new WaitForSeconds(waitTime);

        if(backAndForth)
        {
            if(_targetIndex + direction >= _patrolPositions.Count || _targetIndex + direction < 0)
            {
                direction *= -1;
            }
            _targetIndex += direction;
        }
        else
        {
            if(_targetIndex + 1 < _patrolPositions.Count)
            {
                _targetIndex++;
            }
            else
            {
                _targetIndex = 0;
            }
        }
        moving = false;
    }
}
