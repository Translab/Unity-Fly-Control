//Author: Mengyu Chen
//Year 2018

namespace VRTK{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Collision_detect : MonoBehaviour {

		[Tooltip("don't need to touch, for read by controller fly script")]
		public bool colliding = false; //don't need to touch, for read by controller fly script
		[Tooltip("not used, for debug use")]
		public Transform colliding_point; //not used, for debug use
		[Tooltip("determines the size of the collider")]
		public float userHeight = 1.6f; //determines the size of the collider

		// Use this for initialization
		void Start () {
			//get the collision event handler from the headset collision script
			GetComponent<VRTK_HeadsetCollision> ().HeadsetCollisionDetect += new HeadsetCollisionEventHandler (HeadsetCollisionDetect);
			GetComponent<VRTK_HeadsetCollision> ().HeadsetCollisionEnded += new HeadsetCollisionEventHandler (HeadsetCollisionEnded);

			//make the headset collider our main collider, and increase its size to approximately height of human
			GetComponent<VRTK_HeadsetCollision> ().colliderRadius = userHeight / 2.0f;
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		//for use with headset collision
		private void HeadsetCollisionDetect(object sender, HeadsetCollisionEventArgs e){
			colliding = true;
			colliding_point = e.currentTransform;
		}

		private void HeadsetCollisionEnded(object sender, HeadsetCollisionEventArgs e){
			colliding = false;
		}

	}
}
