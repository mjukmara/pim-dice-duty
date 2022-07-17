using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public enum ChangeType { MOVE_START, MOVE_PROGRESS, MOVE_FINISH };

    public AttachPoint attachPoint;
    public GameObject nextBeltObject;
    public float moveInterval = 3f;
    public float moveSpeed = 2f;
    public bool pusher = true;
    public float progress = 0f;
    public bool receiving = false;

	public float beltSpeedMult = 1.0f;
	public Animator beltAnimator;
	float lastProgress = 0.0f;

    private float moveTimer = 0f;
    private bool isMoving = false;

    public delegate void OnBeltChange(ChangeType changeType, Belt belt);
    public static OnBeltChange onBeltChange;

    public delegate void OnBeltPassItem(Belt belt, Belt nextBelt, Item item);
    public static OnBeltPassItem onBeltPassItem;

    public delegate void OnBeltDestroyItem(Belt belt, Item item);
    public static OnBeltDestroyItem onBeltDestroyItem;

    void Start()
    {

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
            if (nextBeltObject) {
                Belt nextBelt = nextBeltObject.GetComponent<Belt>();
                if (nextBelt)
                {
                    nextBelt.receiving = true;
                }
            }
            OnMoveStart();
        }
    }

    public void OnMoveStart()
    {
		this.lastProgress = 0.0f;
        this.progress = 0f;
		if (beltAnimator != null)
			beltAnimator.SetFloat("SpeedMult", 0.0f);

        onBeltChange?.Invoke(ChangeType.MOVE_START, this);
        StartCoroutine(Tweener.RunTween(
            PureTween.Tween.InOutBack,
            moveSpeed,
            (progress) => OnMoveProgress(progress),
            () => OnMoveFinish()
        ));
    }

    public void OnMoveProgress(float progress)
    {
		this.lastProgress = this.progress;
        this.progress = progress;
        onBeltChange?.Invoke(ChangeType.MOVE_PROGRESS, this);
        Vector3 attachPointAtPos = transform.position + new Vector3(1f, 0f, 0f) * progress;
        attachPoint.transform.position = attachPointAtPos;

		if (beltAnimator != null)
		{
			float delta = progress - lastProgress;
			beltAnimator.SetFloat("SpeedMult", (delta / Time.deltaTime));
		}
    }

    public void OnMoveFinish()
    {
		if (beltAnimator != null)
		{
			beltAnimator.SetFloat("SpeedMult", 0.0f);
		}

        this.progress = 0.0f;
		this.lastProgress = 0.0f;
        onBeltChange?.Invoke(ChangeType.MOVE_FINISH, this);
        Item item = attachPoint.DetachLast();

        bool passedOnItemToNextBelt = false;
        if (nextBeltObject)
        {
            Belt nextBelt = nextBeltObject.GetComponent<Belt>();
            AttachPoint nextAttachPoint = nextBelt.GetAttachPoint();
            if (nextAttachPoint)
            {
                nextAttachPoint.Attach(item);
                passedOnItemToNextBelt = true;
                onBeltPassItem?.Invoke(this, nextBelt, item);
            }
        }

        if (!passedOnItemToNextBelt)
        {
            onBeltDestroyItem?.Invoke(this, item);
            Destroy(item.gameObject);
        }

        attachPoint.transform.position = transform.position;
        this.isMoving = false;
        if (nextBeltObject) {
            Belt nextBelt2 = nextBeltObject.GetComponent<Belt>();
            if (nextBelt2)
            {
                nextBelt2.receiving = false;
            }
        }
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

    public bool IsMoving()
    {
        return isMoving;
    }

    public bool IsReceiving()
    {
        return receiving;
    }

    public bool IsInteractable()
    {
        return !IsMoving() && !IsReceiving();
    }
}
