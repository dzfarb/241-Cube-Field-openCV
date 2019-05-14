using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerController : PlayerController {

    new void Start() {
		base.Start();
		//hide this
		GetComponent<Renderer>().enabled = false;
	}

	new void FixedUpdate() {
		base.FixedUpdate();
		//only go forward, don't speed up
		rb.velocity = new Vector3(0, 0, BASE_SPEED);
	}

	new void OnTriggerEnter(Collider other) {
		//disable collisions
	}

}
