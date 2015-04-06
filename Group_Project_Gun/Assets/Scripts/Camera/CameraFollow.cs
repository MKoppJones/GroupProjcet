using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 5f;        // The speed with which the camera will be following.
		public Vector3 point;
		public float speed = 2f;
		public float x = 0f;
        Vector3 offset;                     // The initial offset from the target.


        void Start ()
        {
            // Calculate the initial offset.
            offset = transform.position - target.position;
			point = target.transform.position;

			offset = new Vector3 (target.position.x + 1f, target.position.y + 1f, target.position.z  -22f);
        }


        void FixedUpdate ()
        {
			if (target && Input.GetMouseButton (1)){
			offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * speed, Vector3.up) * offset;
			transform.position = target.position + offset;
			transform.LookAt (target.position);
			
				// Create a postion the camera is aiming for based on the offset from the target.
				Vector3 targetCamPos = target.position + offset;

				// Smoothly interpolate between the camera's current position and it's target position.
				transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);

			}  
			}
    }
}