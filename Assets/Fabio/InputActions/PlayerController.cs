using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private CharacterInputs characterInputs;
    private InputAction move;
    private InputAction plantSeed;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 playerVelocity = Vector2.zero;

    private void Awake()
    {
        characterInputs = new CharacterInputs();
    }
    private void OnEnable()
    {
        move = characterInputs.Player.Move;
        move.Enable();

        plantSeed = characterInputs.Player.Fire;
        plantSeed.Enable();
        plantSeed.performed += PlantSeed;
    }

    private void OnDisable()
    {
        move.Disable();
        plantSeed.Disable();
    }

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        playerVelocity.x = moveDirection.x * moveSpeed;
        playerVelocity.y = moveDirection.y * moveSpeed;

        rb.velocity = playerVelocity;
    }

    public void PlantSeed(InputAction.CallbackContext context)
    {
        GetComponent<Player>().SpawnRoots();
    }
}
