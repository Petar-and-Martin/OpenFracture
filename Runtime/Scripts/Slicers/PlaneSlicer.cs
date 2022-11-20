using UnityEngine;
using UnityEngine.TestTools;
using System;

[ExcludeFromCoverage]
public class PlaneSlicer : MonoBehaviour
{
    public float RotationSensitivity = 1f;
    public float currentMagnitude;
    public bool collided = false;

    public void OnTriggerStay(Collider collider)
    {
        var material = collider.gameObject.GetComponent<MeshRenderer>().material;
        if (material.name.StartsWith("HighlightSlice"))
        {
            material.SetVector("CutPlaneNormal", this.transform.up);
            material.SetVector("CutPlaneOrigin", this.transform.position);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        var material = collider.gameObject.GetComponent<MeshRenderer>().material;
        if (material.name.StartsWith("HighlightSlice"))
        {
            material.SetVector("CutPlaneOrigin", Vector3.positiveInfinity);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        // Debug.Log("Collision:" + collision.gameObject.name);
        var force = (collision.impulse / Time.fixedDeltaTime).magnitude;
        //if (collision.gameObject.tag == "Fruit") Debug.Log("Force applied: " + (collision.impulse / Time.fixedDeltaTime).magnitude + " " + collision.gameObject.tag);

        if (!collided )
        {
            if (collision.gameObject.tag == "Fruit") Debug.Log("Enough: " + (collision.impulse / Time.fixedDeltaTime).magnitude + " " + collision.gameObject.tag);

            var mesh = this.GetComponent<MeshFilter>().sharedMesh;
            var center = mesh.bounds.center;
            var extents = mesh.bounds.extents;
            //   currentMagnitude = magnitude;
            collided = true;


            extents = new Vector3(extents.x * this.transform.localScale.x,
                                  extents.y * this.transform.localScale.y,
                                  extents.z * this.transform.localScale.z);

            // Cast a ray and find the nearest object
            RaycastHit[] hits = Physics.BoxCastAll(this.transform.position, extents, this.transform.forward, this.transform.rotation, extents.z);

            foreach (RaycastHit hit in hits)
            {
                var obj = hit.collider.gameObject;
                var sliceObj = obj.GetComponent<Slice>();

                if (sliceObj != null)
                {
                    sliceObj.GetComponent<MeshRenderer>()?.material.SetVector("CutPlaneOrigin", Vector3.positiveInfinity);
                    sliceObj.ComputeSlice(this.transform.up, this.transform.position);
                }
            }
        }

    }

    public void ReadyToCollide()
    {
        //yield return new WaitForSeconds(2);

        collided = false;
    }

    void OnCollisionExit(Collision collision)
    {
        ReadyToCollide();
    }

    void OnTriggernEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        Debug.Log("Trigger:" + collision.gameObject.name);
    }


}
