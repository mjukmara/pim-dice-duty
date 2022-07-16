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

        TryInvokeNextBeltToMove();

        if (!this.isMoving)
        {
            this.isMoving = true;
            StartCoroutine(Tweener.RunTween(
                PureTween.Tween.InOutQuad,
                moveSpeed,
                (progress) => OnMoveProgress(progress),
                () => OnMoveFinished()
            ));
        }
    }

    public void OnMoveProgress(float progress)
    {
        Vector3 attachPointAtPos = transform.position + new Vector3(1f, 0f, 0f) * progress;
        attachPoint.transform.position = attachPointAtPos;
    }

    public void OnMoveFinished()
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

    public void TryInvokeNextBeltToMove()
    {
        if (nextBeltObject)
        {
            Belt nextBelt = nextBeltObject.GetComponent<Belt>();
            AttachPoint nextAttachPoint = nextBelt.GetAttachPoint();
            if (nextAttachPoint)
            {
                if (nextAttachPoint.GetAttachments().Count > 0)
                {
                    if (nextBelt)
                    {
                        nextBelt.Move();
                    }
                }
            }
        }
    }

    public AttachPoint GetAttachPoint()
    {
        return this.attachPoint;
    }
}
