using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public Resource initialResource;
    public AttachPoint attachPoint;
    public GameObject nextBeltObject;
    public float moveInterval = 3f;
    public float moveSpeed = 2f;
    public bool pusher = true;

    private float moveTimer = 0f;
    private bool isMoving = false;

    void Start()
    {
        if (initialResource)
        {
            attachPoint.AttachResource(initialResource);
        }
    }

    void Update()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer > moveInterval)
        {
            moveTimer = 0f;
            if (pusher) {
                Move();
            }
        }
    }

    public void Move()
    {
        if (attachPoint.GetAttachments().Count == 0) return;

        Debug.Log("Moving: " + gameObject);
        if (nextBeltObject)
        {
            Belt nextBelt = nextBeltObject.GetComponent<Belt>();
            AttachPoint nextAttachPoint = nextBelt.GetAttachPoint();
            if (nextAttachPoint)
            {
                if (nextAttachPoint.GetAttachments().Count > 0)
                {
                    Debug.Log("Next belt has attachment");
                    if (nextBelt)
                    {
                        Debug.Log("Invoking next belt to move");
                        nextBelt.Move();
                    }
                }
            }
        }

        if (!this.isMoving)
        {
            this.isMoving = true;
            StartCoroutine(Tweener.RunTween(
                PureTween.Tween.InOutQuad,
                moveSpeed,
                (progress) =>
                {
                    Vector3 attachPointAtPos = transform.position + new Vector3(1f, 0f, 0f) * progress;
                    attachPoint.transform.position = attachPointAtPos;
                },
                () =>
                {
                    Resource resource = attachPoint.DetachLastResource();
                    if (nextBeltObject)
                    {
                        Belt nextBelt = nextBeltObject.GetComponent<Belt>();
                        AttachPoint nextAttachPoint = nextBelt.GetAttachPoint();
                        if (nextAttachPoint)
                        {
                            nextAttachPoint.AttachResource(resource);
                        }
                    }
                    attachPoint.transform.position = transform.position;
                    this.isMoving = false;
                }
            ));
        }
    }

    public AttachPoint GetAttachPoint()
    {
        return this.attachPoint;
    }
}
