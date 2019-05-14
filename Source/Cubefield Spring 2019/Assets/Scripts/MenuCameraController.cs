using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : CameraController
{
   new void Update() {
		base.Update();
		cameraTransform.eulerAngles = Vector3.zero;
	}
}
