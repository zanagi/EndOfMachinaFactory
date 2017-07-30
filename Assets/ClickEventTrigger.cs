using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEventTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject eventPrefab;

	public void TriggerEvent()
    {
        if (!GameManager.Instance.Idle)
            return;
        Instantiate(eventPrefab, transform);
        GameManager.Instance.SetState(GameState.Event);
    }
}
