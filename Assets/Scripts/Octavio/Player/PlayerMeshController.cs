using UnityEngine;
using System.Collections;

public class PlayerMeshController : PlayerComp
{
    public Transform mesh;
    public float rotationSpeed = 10f;

    Coroutine rotateRoutine;

    public void UpdateDirection(FDirection direction)
    {
        Quaternion targetRotation;

        if (direction == FDirection.Right)
            targetRotation = Quaternion.identity;
        else
            targetRotation = Quaternion.Euler(0f, 180f, 0f);

        if (rotateRoutine != null)
            StopCoroutine(rotateRoutine);

        rotateRoutine = StartCoroutine(RotateRoutine(targetRotation));
    }

    IEnumerator RotateRoutine(Quaternion target)
    {
        Quaternion start = mesh.localRotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed * WorldSettings.movementScale;

            mesh.localRotation = Quaternion.Lerp(start, target, t);

            yield return null;
        }

        mesh.localRotation = target;
    }
}