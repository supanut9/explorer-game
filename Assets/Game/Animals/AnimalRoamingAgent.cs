using UnityEngine;
using UnityEngine.AI;

namespace ExplorerGame.Animals
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class AnimalRoamingAgent : MonoBehaviour
    {
        [SerializeField] private float roamRadius = 8f;
        [SerializeField] private Vector2 idleDurationRange = new(1.5f, 4f);

        private NavMeshAgent agent;
        private Vector3 origin;
        private float nextMoveTime;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            origin = transform.position;
            ScheduleNextMove();
        }

        private void Update()
        {
            if (Time.time < nextMoveTime)
            {
                return;
            }

            if (agent.pathPending || agent.remainingDistance > 0.5f)
            {
                return;
            }

            var randomOffset = Random.insideUnitSphere * roamRadius;
            randomOffset.y = 0f;
            if (NavMesh.SamplePosition(origin + randomOffset, out var hit, roamRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }

            ScheduleNextMove();
        }

        private void ScheduleNextMove()
        {
            nextMoveTime = Time.time + Random.Range(idleDurationRange.x, idleDurationRange.y);
        }
    }
}
