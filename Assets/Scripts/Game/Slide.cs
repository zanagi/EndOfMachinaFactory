using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Slide : MonoBehaviour {

    public static readonly float speed = 0.03f;
    public static readonly float sqrSpeed = speed * speed;

    private MeshRenderer meshRenderer;
    private float offset;

    private void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
	}

    private void FixedUpdate () {
        if (!GameManager.Instance.Idle)
            return;

        offset += speed;

        if (offset >= 1)
            offset -= 1;
        meshRenderer.material.mainTextureOffset = new Vector2(0, offset);
	}
}
