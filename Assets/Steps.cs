using UnityEngine;

public class Steps : MonoBehaviour
{
    public float stepInterval = 0.5f; // Intervalo en segundos entre pasos
    private float stepTimer;
    private bool isMoving = false;
    private bool isStepSoundPlaying = false; // Para saber si el sonido ya está sonando
    private bool onConcrete = false; // Indica si el jugador está en una superficie de concreto

    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if (!isMoving)
            {
                if (onConcrete)
                {
                    AkSoundEngine.PostEvent("Play_concreteSteps", gameObject);
                }
                else
                {
                    AkSoundEngine.PostEvent("Play_Steps", gameObject);
                }

                isMoving = true;
                isStepSoundPlaying = true;
            }

            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval && !isStepSoundPlaying)
            {
                if (onConcrete)
                {
                    AkSoundEngine.PostEvent("Play_concreteSteps", gameObject);
                }
                else
                {
                    AkSoundEngine.PostEvent("Play_Steps", gameObject);
                }

                stepTimer = 0;
                isStepSoundPlaying = true;
            }
        }
        else
        {
            if (isStepSoundPlaying)
            {
                if (onConcrete)
                {
                    AkSoundEngine.PostEvent("Stop_concreteSteps", gameObject);
                }
                else
                {
                    AkSoundEngine.PostEvent("Stop_Steps", gameObject);
                }

                isStepSoundPlaying = false;
            }

            stepTimer = 0;
            isMoving = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Concrete"))
        {
            onConcrete = true;
            AkSoundEngine.PostEvent("Stop_Steps", gameObject); // Detén los pasos por defecto al entrar
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Concrete"))
        {
            onConcrete = false;
            AkSoundEngine.PostEvent("Stop_concreteSteps", gameObject); // Detén los pasos de concreto al salir
        }
    }
}
