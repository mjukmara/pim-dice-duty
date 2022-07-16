using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public List<Resource> resources;
    public float spawnInterval = 5f;
    public Belt targetBelt;

    float timer;
    
    void Start()
    {
        timer = Time.time;    
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnInterval)
        {
            timer = 0;
            if (targetBelt)
            {
                if (resources.Count > 0)
                {
                    Resource resource = resources[Random.Range(0, resources.Count)];
                    AttachPoint attachPoint = targetBelt.GetAttachPoint();
                    if (attachPoint)
                    {
                        attachPoint.AttachResource(resource);
                    }
                }
            }
        }
    }
}
