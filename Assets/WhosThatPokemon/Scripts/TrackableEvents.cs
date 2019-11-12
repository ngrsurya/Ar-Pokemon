using Vuforia;
using UnityEngine;
using UnityEngine.Events;

public class TrackableEvents : MonoBehaviour, ITrackableEventHandler
{
    [Header("+ then ActivityMAnager/PokemonActivityManager/SubmitName (pokemon name)")]
    public UnityEvent onTargetFound;
    public UnityEvent onTargetLost;

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES
    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if(mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler( this );
    }

    protected virtual void OnDestroy()
    {
        if(mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler( this );
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus )
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if(newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log( "Trackable " + mTrackableBehaviour.TrackableName + " found" );
            onTargetFound.Invoke();
            OnTrackingFound();
        } else if(previousStatus == TrackableBehaviour.Status.TRACKED &&
                   newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log( "Trackable " + mTrackableBehaviour.TrackableName + " lost" );
            onTargetLost.Invoke();
            OnTrackingLost();
        } else
        {

            OnTrackingLost();
        }
    }

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>( true );
        var colliderComponents = GetComponentsInChildren<Collider>( true );
        var canvasComponents = GetComponentsInChildren<Canvas>( true );

        // Enable rendering:
        foreach(var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach(var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach(var component in canvasComponents)
            component.enabled = true;
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>( true );
        var colliderComponents = GetComponentsInChildren<Collider>( true );
        var canvasComponents = GetComponentsInChildren<Canvas>( true );

        // Disable rendering:
        foreach(var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach(var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach(var component in canvasComponents)
            component.enabled = false;
    }

    #endregion // PROTECTED_METHODS
}
