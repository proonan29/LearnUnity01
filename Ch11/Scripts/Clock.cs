using System;
using System.Collections;
using UnityEngine;

public class Clock : MonoBehaviour {
	
	private const float
	hoursToDegrees = -360f / 12f,
	minutesToDegrees = -360f / 60f,
	secondsToDegrees = -360f / 60f,

	speed = 3.0f,	// swing speed
	swing = 20.0f,	// swing angle

	door_time = 0.8f,
	door_angle = 160f,
	door_speed = door_angle/door_time,

	birdmove_time = 0.2f,
	birdmove_distance = 0.5f,
	birdmove_speed = birdmove_distance/birdmove_time;

	public Transform hours, minutes, seconds, pendlum;
	public Transform door_left, door_right, bird;

	public AudioSource SoundCuckoo;
    public AudioSource SoundOpen;
    public AudioSource SoundClose;


    public bool analog;
	public bool trigger;

	// bird movement
	private bool bird_active;
	private int bird_pahse;

	// pendlum & cuckoo 
	private float t;
	private float last_sec;

	void Start()
	{
		t = 0.0f;
		// cuckoo.Play();

		last_sec = 0.0f;
		bird_active = false;
		StartCoroutine(PlayCuckoo());

		trigger = false;
	}

	void Update () {
		float ang;
		DateTime time = DateTime.Now;
		TimeSpan timespan = DateTime.Now.TimeOfDay;

		ang = -90.0f+(float)timespan.TotalHours * hoursToDegrees;
		hours.localRotation = Quaternion.Euler(ang, 0f, 0f);
		ang = -90.0f+(float)timespan.TotalMinutes * minutesToDegrees;
		minutes.localRotation = Quaternion.Euler(ang, 0f, 0f);

		if (analog) {
			ang = -90.0f+(float)timespan.TotalSeconds * secondsToDegrees;
			seconds.localRotation = Quaternion.Euler(ang, 0f, 0f);
		}
		else {
			ang = -90.0f + time.Second * secondsToDegrees;
            seconds.localRotation = Quaternion.Euler(ang, 0f, 0f);
        }

		// pendlum control
		if (time.Second < last_sec || trigger) {
			StartCoroutine(PlayCuckoo());
			trigger = false;
		}
		last_sec = time.Second;

		t += Time.deltaTime * speed;
		ang = -90.0f + Mathf.Sin (t) * swing;
		pendlum.localRotation = Quaternion.Euler(ang, 0f, 0f);
	}

	IEnumerator PlayCuckoo() {
		float t;
		Vector3 door_left_rot = door_left.rotation.eulerAngles;
		Vector3 door_right_rot = door_right.rotation.eulerAngles;

		if (!bird_active) {
			bird_active = true;	

			SoundOpen.Play ();
			// 1. open door in 1 sec
			t = 0.0f;
			while (t < door_time) {
				t += Time.deltaTime;
				door_left.Rotate(
					new Vector3(0f, 0f, Time.deltaTime * door_speed));
				door_right.Rotate(
					new Vector3(0f, 0f, -Time.deltaTime * door_speed));
				yield return null;
			}
			door_left.rotation = 
				Quaternion.Euler(door_left_rot + new Vector3 (0f, 0f, door_angle));
			door_right.rotation = 
				Quaternion.Euler(door_right_rot + new Vector3 (0f, 0f, -door_angle));
			yield return new WaitForSeconds(0.3f);

			// 2. move bird out
			t = 0.0f;
			while (t < birdmove_time) {
				t += Time.deltaTime;
				bird.Translate(
					new Vector3(-Time.deltaTime * birdmove_speed, 0f, 0f));
				yield return null;
			}
			yield return new WaitForSeconds(0.3f);

			// bird sing multiple
			int count = DateTime.Now.Hour;
			if (count == 0) {
				count = 12;
			}
			if (count > 12) {
				count -= 12;
			}

			for (int i = 0; i < count; i++) {
                // 3-1. bird swing
                SoundCuckoo.Play();

                t = 0.0f;
				while (t < 0.4f) {
					t += Time.deltaTime;
					bird.Rotate(
						new Vector3 (0f, -Time.deltaTime * 80.0f, 0f));
					yield return null;
				}

				// 3-2. bird swing back
				t = 0.0f;
				while (t < 0.4f) {
					t += Time.deltaTime;
					bird.Rotate(
						new Vector3 (0f, Time.deltaTime * 80.0f, 0f));
					yield return null;
				}

				yield return new WaitForSeconds (0.3f);
			}

			// 4. move bird back
			t = 0.0f;
			while (t < birdmove_time) {
				t += Time.deltaTime;
				bird.Translate(
					new Vector3(Time.deltaTime * birdmove_speed, 0f, 0f));
				yield return null;
			}

			yield return new WaitForSeconds(0.3f);

            // Final phase, close door
            SoundClose.Play();
            t = 0.0f;
			while (t < door_time) {
				t += Time.deltaTime;
				door_left.Rotate(
					new Vector3(0f, 0f, -Time.deltaTime * door_speed));
				door_right.Rotate(
					new Vector3(0f, 0f, Time.deltaTime * door_speed));
				yield return null;
			}
			door_left.rotation = Quaternion.Euler(door_left_rot);
			door_right.rotation = Quaternion.Euler(door_right_rot);

			bird_active = false;
		}
	}
}
