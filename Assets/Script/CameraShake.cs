using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        Instance = this;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float force)
    {
        if (impulseSource != null)
        {
            Debug.Log("Shake con Cinemachine Activado.");
            impulseSource.GenerateImpulseWithForce(force);
        }
    }
}
