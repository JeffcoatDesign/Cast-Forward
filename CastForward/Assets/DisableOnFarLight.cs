using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnFarLight : MonoBehaviour
{
    public Light componentToControl;
    public float maxDistance;
    private PlayerEntity _player;

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
            CheckForPlayer();
        else
        {
            if (Vector3.Distance(_player.transform.position, transform.position) > maxDistance)
            {
                componentToControl.enabled = false;
            }
            else
                componentToControl.enabled = true;
        }
    }

    void CheckForPlayer ()
    {
        _player = FindFirstObjectByType<PlayerEntity>();
    }
}
