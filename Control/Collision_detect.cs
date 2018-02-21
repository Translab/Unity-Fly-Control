//Author: Mengyu Chen
//Year 2018

namespace VRTK{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Collision_detect : MonoBehaviour {

		public bool colliding = false;
		public Transform colliding_point;

		// Use this for initialization
		void Start () {
			GetComponent<VRTK_HeadsetCollision> ().HeadsetCollisionDetect += new HeadsetCollisionEventHandler (HeadsetCollisionDetect);
			GetComponent<VRTK_HeadsetCollision> ().HeadsetCollisionEnded += new HeadsetCollisionEventHandler (HeadsetCollisionEnded);

			//if use body physics, need to turn off VRTK body collisions and make floorHeightTolerance high enough not to trigger snap to floor
			//GetComponent<VRTK_BodyPhysics> ().enableBodyCollisions = false;
			//GetComponent<VRTK_BodyPhysics> ().floorHeightTolerance = 999.0f;
			//otherwise, will conflict with flying system

			//can also use body physics's collider to check colliding
			//but may have some bugs
			//GetComponent<VRTK_BodyPhysics> ().StartColliding += new BodyPhysicsEventHandler (StartColliding);
			//GetComponent<VRTK_BodyPhysics> ().StopColliding += new BodyPhysicsEventHandler (StopColliding);
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


		//ignore this part
		//		private void StopColliding(object sender, BodyPhysicsEventArgs e){
		//			//colliding = false;
		//		}

	}
}
