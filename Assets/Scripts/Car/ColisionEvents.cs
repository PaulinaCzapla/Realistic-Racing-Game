using Car.WheelsManagement;
using Events.ScriptableObjects;
using RaceManagement.ControlPoints;
using SoundManagement;
using UnityEngine;

public class ColisionEvents : MonoBehaviour
{
    [SerializeField] private SoundEventChannelSO playSound;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if (other.TryGetComponent(out ControlPoint controlPoint))
        {
            foreach (var point in controlPoint.spawnPoints)
            {
                if (!point.activeSelf)
                { 
                    point.SetActive(true);
                }
                
            }
        }
        if (other.TryGetComponent(out CarMovementController controller))
        {
            playSound.RaiseEvent(SoundName.CarColision);
        }

        if (other.TryGetComponent(out WallColision wall))
        {
            playSound.RaiseEvent(SoundName.TrackColision);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if (other.gameObject.TryGetComponent(out CarMovementController controller))
        // {
        //     playSound.RaiseEvent(SoundName.CarColision);
        // }

        if (other.gameObject.TryGetComponent(out WallColision wall))
        {
            playSound.RaiseEvent(SoundName.TrackColision);
        }
    }
}