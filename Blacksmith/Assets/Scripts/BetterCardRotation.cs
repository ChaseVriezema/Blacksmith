using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BetterCardRotation : MonoBehaviour
{

    public GameObject[] HideWhenBack; 

    private bool showingBack = false;

    private void Update ()
    {
        var cam = Camera.current;
        bool passedThroughColliderOnCard = cam != null && Vector3.Dot(transform.forward, cam.transform.position - transform.position) > 0.0f;

        if (passedThroughColliderOnCard != showingBack)
        {
            showingBack = passedThroughColliderOnCard;
            foreach (var obj in HideWhenBack)
                    obj.SetActive(!showingBack);
        }
    }
}