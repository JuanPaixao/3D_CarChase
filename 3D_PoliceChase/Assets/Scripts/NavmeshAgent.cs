using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshAgent : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform target;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = target.position;
    }
    void FixedUpdate()
    {

    }
    public IEnumerator FindTarget()
    {
        while (0 < 1)
        {
            _agent.destination = target.position;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
