﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
  public Transform target;
	[SerializeField] private int huntPlayerChance = 50;
	[SerializeField] private int speed = 1;
	private Rigidbody2D rb2D;
	private Transform player;

	// Start is called before the first frame update
	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		player = GameObject.FindWithTag("Player").transform;
		FindTarget();
	}

	// Update is called once per frame
	void Update() {
		if (!target)
			FindTarget();
	}

	private void FindTarget() {
		if (Random.Range(0, 100) < huntPlayerChance)
			HuntPlayer();
		else
			HuntRandomHuman();
	}

	private void HuntPlayer() {
		target = player;
	}

	private void HuntRandomHuman() {
		GameObject[] humans = GameObject.FindGameObjectsWithTag("Human");
		if (humans.Length <= 0) {
			HuntPlayer();
			return;
		}

		GameObject human = humans[Random.Range(0, humans.Length - 1)];
		target = human.transform;
	}

	void FixedUpdate() {
		Vector2 direction = target.position - transform.position;
		direction.Normalize();

		Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
		transform.rotation = rotation;
		rb2D.velocity = direction * speed;
	}
}
