using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerSpawn : MonoBehaviour
{
	private bool isPositionXR = false;

     private void Start()
    {
        if (CheckXRDevice.isPresent())
		{
			transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
			transform.eulerAngles = Vector3.zero;
			isPositionXR = true;
		}
		else
		{
			transform.position = new Vector3(transform.position.x, 2.3f, transform.position.z);
			transform.eulerAngles = Vector3.zero;
		}
    }

	private void Update()
	{
		if (CheckXRDevice.isPresent() && !isPositionXR)
		{
			transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
			transform.eulerAngles = Vector3.zero;
			isPositionXR = true;
		}
		else if (!CheckXRDevice.isPresent() && isPositionXR)
		{
			transform.position = new Vector3(transform.position.x, 2.3f, transform.position.z);
			transform.eulerAngles = Vector3.zero;
			isPositionXR = false;
		}
	}
}

internal static class CheckXRDevice
{
	public static bool isPresent()
	{
		var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
		SubsystemManager.GetInstances(xrDisplaySubsystems);
		foreach (var xrDisplay in xrDisplaySubsystems)
		{
			if (xrDisplay.running)
				return true;
		}
		return false;
	}
}