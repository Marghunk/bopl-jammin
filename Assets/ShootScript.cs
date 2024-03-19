using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public bool toggleDebug = true;
    public Color debugInputColor;
    public SplineGravity gravity;
    Rigidbody2D rb;
    Inputs inputs;

    [Header("Coen's shit")]
    public GameObject triangle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = GetComponent<SplineGravity>();
        inputs = GetComponent<Inputs>();
    }



    private void Update()
    {
        // Get input.
        int gamepadIndex = inputs.GamepadIndex();
        float hz = Input.GetAxis($"Horizontal-GP-{gamepadIndex}") + Input.GetAxis($"Horizontal");
        float vt = Input.GetAxis($"Vertical-GP-{gamepadIndex}") + Input.GetAxis($"Vertical");
        Vector2 inputDirection = new Vector2(hz, vt);
        float inputMagnitude = inputDirection.magnitude;
        inputDirection.Normalize();


        #region coen's shit
        
        
        
        if (Input.GetButton($"Shoot-GP-{gamepadIndex}"))
        {
            Debug.Log("Fire!");
            // Draw the input direction.
            //if (toggleDebug) Debug.DrawRay(transform.position, inputDirection * inputMagnitude, debugInputColor);
            triangle.transform.position = transform.position + ((Vector3)inputDirection * 0.5f);

        }

        if (Input.GetButtonDown($"Shoot-GP-{gamepadIndex}"))
        {
            Debug.Log("A");
            StartCoroutine(SlowGravity());
        }

        if (Input.GetButtonUp($"Shoot-GP-{gamepadIndex}"))
        {
            GetComponent<PlayerSpellInfo>().CastSpell((transform.GetChild(0).position - transform.position).normalized);
        }

        #endregion
    }

    /// <summary>
    /// Flawed way of reducing speed when using ability. Maybe make a glide button?
    /// </summary>
    /// <returns></returns>
    IEnumerator SlowGravity()
    {
        AirMovement am = GetComponent<AirMovement>();
        float ogSpeed = am.speed;

        am.gravityScale = 0.25f; // Fall at half speed when shoot is held
        am.speed= 0; // Do not turn when shooting

        while (!Input.GetButtonUp($"Shoot-GP-{inputs.GamepadIndex()}"))
        {
            yield return null;
        }

        am.speed = ogSpeed;
        am.gravityScale = 1;

        yield return null;
    }
}
