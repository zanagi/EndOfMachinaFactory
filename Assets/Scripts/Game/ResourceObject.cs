using UnityEngine;
using System.Collections;

public class ResourceObject : MonoBehaviour
{
    public SlideArea slideArea;
    private Vector3 velocity;

    private void Start()
    {
        if (!slideArea)
            return;

        CalculateVelocity();
    }

    private void FixedUpdate()
    {
        if(!slideArea || !GameManager.Instance.Idle)
            return;

        if ((slideArea.end.position - transform.position).sqrMagnitude <= Slide.sqrSpeed)
        {
            transform.position = slideArea.end.position;
            if(slideArea.next)
            {
                slideArea = slideArea.next;
                CalculateVelocity();
            } else
            {
                Destroy(gameObject);
            }
            return;
        }
        transform.LookAt(slideArea.end);
        transform.position += velocity * Slide.speed;

    }

    private void CalculateVelocity()
    {
        velocity = (slideArea.end.position - slideArea.start.position).normalized;
        transform.LookAt(slideArea.end);
    }
}
