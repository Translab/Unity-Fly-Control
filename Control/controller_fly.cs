//Author: Mengyu Chen
//Year 2018

namespace VRTK{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class controller_fly : MonoBehaviour{

		//gameobject reference 
 		public GameObject CameraRig;
		public GameObject Pointing_hand; //link your controller here as your pointing reference, or use your headset as facing reference

		//flying param
		public float fly_speed = 1.0f; //fly speed by default 1.0
		private Vector3 fly_velocity;
		public Vector3 facing_direction; //just for reference, showing where your pointing object is facing
		private Vector3 fly_acceleration;
		private float fly_acce_max = 0.2f;
		private float speed_compensation = 0.1f;
		private bool flying = false;

		//Collision Detect
		public bool collision_detection = true; //need to add a "VRTK_HeadsetCollision" script in your playarea object
		public float collision_bouncing_factor = 0.2f; //determines how far you get bounce back when you hit something
		public GameObject playarea; //link your playarea object here

		//landing height
		public bool gravity = true; //check this if you want gravity to return back to the middle plane / terrain
		public float landing_height = 0.0f; //typically 0 or matches with terrain / plane height
		public float landing_detection_threshold = 0.1f; //tolerence of landing detection, can be much less if your gravity factor isn't very high
		private Vector3 reference_landing_point;
		private float reference_distance; //distance between flying body and reference plane
		private Vector3 fall_velocity;
		private Vector3 gravity_acceleration;

		//determines how much your gravity acceleration can be, 
		//if your acceleration is too high, you may not land properly
		public float gravity_factor = 0.05f; 
		private float grav_acce_max;
		private bool onGround = true;
		private bool onObject = false;

		//collision detect
		private Collision_detect collision_detect;
		private Vector3 bouncing_velocity;

		//floating effect 
		public bool floating_effect = true;
		public float floating_factor = 0.003f; //amplitude of floating
		public float floating_intensity = 1.0f; //floating frequency
		private Vector3 floating_temp_pos = new Vector3(0,0,0);

		void Start(){
			GetComponent<VRTK_ControllerEvents> ().TriggerPressed += new ControllerInteractionEventHandler (DoTriggerPressed);
			GetComponent<VRTK_ControllerEvents> ().TriggerReleased += new ControllerInteractionEventHandler (DoTriggerReleased);


			//
			if (collision_detection) {
				collision_detect = playarea.GetComponent<Collision_detect> ();
			}
			grav_acce_max = fly_speed * 0.1f * gravity_factor;
		}


		void Update(){
			facing_direction = Pointing_hand.transform.rotation * Vector3.forward;
			reference_distance = Mathf.Abs(CameraRig.transform.position.y - landing_height);

			//switch on ground status
			if (reference_distance < landing_detection_threshold) {
				onGround = true;
			} else {
				if (flying) { //only when flying, switch onGround to be false
					//this means, if simply floating, then its okay to exceed the landing detection threshold
					onGround = false;
				}
			}
				
			//check collision status
			if (collision_detection) {
				if (collision_detect.colliding) {
					onObject = true;
				} else if (!collision_detect.colliding) {
					onObject = false;
				}
			} else {
				onObject = false;
			}

			if (flying) {
				fly_acceleration = facing_direction;
				if (fly_acceleration.magnitude > fly_acce_max) {
					fly_acceleration = Vector3.ClampMagnitude (fly_acceleration, fly_acce_max);
				}
				fly_velocity += fly_acceleration * fly_speed * speed_compensation;
				CameraRig.transform.position += fly_velocity;

				if (onObject) {
					CameraRig.transform.position = collision_detect.colliding_point.position - facing_direction * collision_bouncing_factor;
					fly_velocity = Vector3.zero;
				} 
				fly_velocity *= 0.5f; //not zeros but decays velocity, to leave some inertia
				fly_acceleration = Vector3.zero; //zeros acceleration
			} else {
				if (gravity) {
					if (onObject) {
						fall_velocity *= 0.0f;
					}
					if (!onGround && !onObject) {
						reference_landing_point = new Vector3 (CameraRig.transform.position.x, landing_height, CameraRig.transform.position.z);
						gravity_acceleration = reference_landing_point - CameraRig.transform.position;
						if (gravity_acceleration.magnitude > grav_acce_max) {
							gravity_acceleration = Vector3.ClampMagnitude (gravity_acceleration, grav_acce_max);
						}
						fall_velocity += gravity_acceleration;

						CameraRig.transform.position += fall_velocity;
						gravity_acceleration = gravity_acceleration * 0.0f; // zeros acceleration
					} else if (onGround && !onObject){
						fall_velocity *= 0.0f;// zeros velocity to stop movement
						if (floating_effect) {
							floating_temp_pos = CameraRig.transform.position;
							floating_temp_pos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * floating_intensity) * floating_factor;
							CameraRig.transform.position = floating_temp_pos;
						} 
					}
				}
				if (floating_effect) {
					if (!onObject) {
						floating_temp_pos = CameraRig.transform.position;
						floating_temp_pos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * floating_intensity) * floating_factor;
						CameraRig.transform.position = floating_temp_pos;
				
					}
				}
			}
		}

		private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
		{
			VRTK_Logger.Info("Controller on index '" + index + "' " + button + " has been " + action
				+ " with a pressure of " + e.buttonPressure + " / trackpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)");
		}

		private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e){
			
			flying = true;
			//DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "pressed", e);
		}

		private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
		{
			flying = false;
			//DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "released", e);
		}
	}
}