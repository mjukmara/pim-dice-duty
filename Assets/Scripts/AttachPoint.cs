using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPoint : MonoBehaviour
{
    public List<Attachment> attachments = new List<Attachment>();

    public class Attachment
    {
        public Resource resource;
        public GameObject prefabInstance;

        public Attachment(Resource resource, GameObject prefabInstance)
        {
            this.resource = resource;
            this.prefabInstance = prefabInstance;
        }
    }

    public void AttachResource(Resource resource)
    {
        if (!resource) return;

        GameObject prefabInstance = Instantiate(resource.displayPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.transform.localPosition = Vector3.zero;
        prefabInstance.transform.localScale = Vector3.one;
        attachments.Add(new Attachment(resource, prefabInstance));
    }

    public Resource DetachResource(Resource resource)
    {
        if (!resource) return null;

        Attachment foundAttachment = this.attachments.Find(attachment => attachment.resource == resource);
        if (foundAttachment == null) return null;

        Destroy(foundAttachment.prefabInstance);
        this.attachments.Remove(foundAttachment);

        return foundAttachment.resource;
    }

    public Resource DetachLastResource()
    {
        if (this.attachments.Count == 0) return null;

        Attachment foundAttachment = this.attachments[this.attachments.Count - 1];
        if (foundAttachment == null) return null;

        Destroy(foundAttachment.prefabInstance);
        this.attachments.Remove(foundAttachment);

        return foundAttachment.resource;
    }

    public List<Attachment> GetAttachments()
    {
        return this.attachments;
    }
}
