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
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Roam", 0f, roamInterval);
    }

    void Update()
    {
        if(target == null) Destroy(this);
        if(target != null) distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (target != null && !isStopped)
        {
            if (distanceToPlayer <= followDistance)
            {
                agent.SetDestination(target.position);
                PlayWalkingSound();
            }
        }
    }

    void Roam()
    {
        if (Vector3.Distance(transform.position, target.position) > followDistance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position + randomDirection, out hit, roamRadius, NavMesh.AllAreas))
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
        Vector3 reverseDirection = -transform.forward;
        agent.velocity = reverseDirection * agent.speed;
        Invoke("ResumeAI", 2f);
    }

    void ResumeAI()
    {
        isStopped = false;
        agent.isStopped = false;
        agent.velocity = Vector3.zero;
        agent.SetDestination(target.position);
    }

    void PlayWalkingSound()
    {
        if (!audioSource.isPlaying & playable)
        {
            audioSource.clip = walkingSound;
            audioSource.Play();
        }
    }
}
