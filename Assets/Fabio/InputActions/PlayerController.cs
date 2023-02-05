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
    private InputAction pauseGame;
    Player player;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 playerVelocity = Vector2.zero;

    private void Awake()
    {
        characterInputs = new CharacterInputs();
    }
    private void OnEnable()
    {
        pauseGame = characterInputs.Player.PauseGame;
        pauseGame.Enable();
        pauseGame.performed += PauseGame;
    }

    private void OnDisable()
    {
        move.Disable();
        plantSeed.Disable();
        pauseGame.Disable();
    }

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        player = transform.GetComponent<Player>();

        if (player.playerID == Player.PlayerID.Player1)
        {
            plantSeed = characterInputs.Player.FireP1;
        }
        else
        {
            plantSeed = characterInputs.Player.FireP2;
        }

        plantSeed.Enable();
        plantSeed.performed += PlantSeed;

        if (player.playerID == Player.PlayerID.Player1)
        {
            move = characterInputs.Player.MoveP1;
        }
        else
        {
            move = characterInputs.Player.MoveP2;
        }

        move.Enable();
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
        if ((player.playerID == Player.PlayerID.Player1 && plantSeed == characterInputs.Player.FireP1) ||
            (player.playerID == Player.PlayerID.Player2 && plantSeed == characterInputs.Player.FireP2))
        {
            player.SpawnRoots();
        }
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        GameManager.i.TogglePause();
    }
}
