using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public enum ChangeType { MOVE_START, MOVE_PROGRESS, MOVE_FINISH };

    public Resource initialResource;
    public AttachPoint attachPoint;
    public GameObject nextBeltObject;
    public float moveInterval = 3f;
    public float moveSpeed = 2f;
    public bool pusher = true;

    private float moveTimer = 0f;
    private bool isMoving = false;

    public delegate void OnBeltChange(ChangeType changeType, Belt belt);
    public static OnBeltChange onBeltChange;

    public delegate void OnBeltPassResource(Belt belt, Belt nextBelt, Resource resource);
    public static OnBeltPassResource onBeltPassResource;

    public delegate void OnBeltDestroyResource(Belt belt, Resource resource);
    public static OnBeltDestroyResource onBeltDestroyResource;

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
            onBeltChange?.Invoke(ChangeType.MOVE_START, this);
            StartCoroutine(Tweener.RunTween(
                PureTween.Tween.InOutBack,
                moveSpeed,
                (progress) => OnMoveProgress(progress),
                () => OnMoveFinished()
            ));
        }
    }

    public void OnMoveProgress(float progress)
    {
        onBeltChange?.Invoke(ChangeType.MOVE_PROGRESS, this);
        Vector3 attachPointAtPos = transform.position + new Vector3(1f, 0f, 0f) * progress;
        attachPoint.transform.position = attachPointAtPos;
    }

    public void OnMoveFinished()
    {
        onBeltChange?.Invoke(ChangeType.MOVE_FINISH, this);
        Resource resource = attachPoint.DetachLastResource();

        bool passedOnResourceToNextBelt = false;
        if (nextBeltObject)
        {
            Belt nextBelt = nextBeltObject.GetComponent<Belt>();
            AttachPoint nextAttachPoint = nextBelt.GetAttachPoint();
            if (nextAttachPoint)
            {
                nextAttachPoint.AttachResource(resource);
                passedOnResourceToNextBelt = true;
                onBeltPassResource?.Invoke(this, nextBelt, resource);
            }
        }

        if (!passedOnResourceToNextBelt)
        {
            onBeltDestroyResource?.Invoke(this, resource);
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
