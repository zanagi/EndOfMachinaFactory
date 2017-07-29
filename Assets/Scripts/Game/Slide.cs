using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Slide : MonoBehaviour {

    private MeshRenderer meshRenderer;
    [SerializeField]
    private float speed = 0.01f;
    private float offset;

    private void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
	}

    private void FixedUpdate () {
        offset += speed;

        if (offset >= 1)
            offset -= 1;
        meshRenderer.material.mainTextureOffset = new Vector2(0, offset);
	}
}
