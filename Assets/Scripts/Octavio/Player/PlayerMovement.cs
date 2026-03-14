using System.Collections;
using UnityEngine;

public class PlayerMovement : PlayerComp
{
    public Rigidbody rb;
    public Collider playerCollider;

    public float speed = 5f;

    public bool canMove = true;
    public bool canDown = true;

    public float stepDistance = 1.2f;

    float distanceAccumulator;
    Vector3 lastPosition;

    AudioSource stepSource;
    AudioDataResult? stepAudio;

    bool isDropping;

    void Start()
    {
        lastPosition = transform.position;

        stepAudio = AudioManager.Library.GetAudio(0);

        stepSource = gameObject.AddComponent<AudioSource>();
        stepSource.playOnAwake = false;
    }

    void Update()
    {
        if (canMove)
            Move();

        if (!isDropping)
            HandleFootsteps();
    }

    public void Move()
    {
        Vector3 dir = controller.GetDirectionVector();

        float scaledSpeed = speed * WorldSettings.movementScale;

        rb.linearVelocity = new Vector3(
            dir.x * scaledSpeed,
            rb.linearVelocity.y,
            rb.linearVelocity.z
        );
    }

    void HandleFootsteps()
    {
        if (stepAudio == null)
            return;

        float distance = Vector3.Distance(transform.position, lastPosition);
        distanceAccumulator += distance;

        if (distanceAccumulator >= stepDistance)
        {
            stepSource.transform.position = transform.position;
            stepSource.spatialBlend = stepAudio.Value.Space == AudioSpace.ThreeD ? 1f : 0f;

            stepSource.PlayOneShot(
                stepAudio.Value.Clip,
                stepAudio.Value.Volume
            );

            distanceAccumulator = 0f;
        }

        lastPosition = transform.position;
    }

    public void Stop()
    {
        rb.linearVelocity = Vector3.zero;
        canMove = false;
    }

    public void Resume()
    {
        canMove = true;
    }

    public void Down()
    {
        if (canDown)
            StartCoroutine(DownRoutine());

        Debug.Log("Down");
    }

    IEnumerator DownRoutine()
    {
        Stop();

        isDropping = true;

       
        AudioManager.Play(1, transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            controller.floorDetector.IgnoreFloor(hit.collider);
        }

        canDown = false;
        playerCollider.isTrigger = true;

        yield return new WaitUntil(() => controller.floorDetector.isGrounded == false);
        yield return new WaitUntil(() => controller.floorDetector.isGrounded == true);

        playerCollider.isTrigger = false;

        controller.floorDetector.ResetIgnoredFloor();

        canDown = true;

        isDropping = false;

        distanceAccumulator = 0f;

        Resume();
    }
}