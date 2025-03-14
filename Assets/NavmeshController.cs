using UnityEngine;
using UnityEngine.AI;

public class NavmeshController : MonoBehaviour
{
    private Transform target;
    public float followDistance = 10f;
    public float roamRadius = 10f;
    public float roamInterval = 3f;
    public AudioClip walkingSound;
    public bool playable;
    private NavMeshAgent agent;
    private bool isStopped = false;
    public AudioSource audioSource;
    private float distanceToPlayer;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(Roam), 0f, roamInterval);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (!isStopped && distanceToPlayer <= followDistance)
        {
            agent.SetDestination(target.position);
            PlayWalkingSound();
        }
    }

    void Roam()
    {
        if (target == null) return;

        if (Vector3.Distance(transform.position, target.position) > followDistance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            if (NavMesh.SamplePosition(transform.position + randomDirection, out NavMeshHit hit, roamRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                PlayWalkingSound();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ReverseAI();
        }
    }

    void ReverseAI()
    {
        isStopped = true;
        agent.isStopped = true;
        agent.velocity = -transform.forward * agent.speed;
        Invoke(nameof(ResumeAI), 2f);
    }

    void ResumeAI()
    {
        if (target == null) return;

        isStopped = false;
        agent.isStopped = false;
        agent.velocity = Vector3.zero;
        agent.SetDestination(target.position);
    }

    void PlayWalkingSound()
    {
        if (playable && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.clip = walkingSound;
            audioSource.Play();
        }
    }
}
