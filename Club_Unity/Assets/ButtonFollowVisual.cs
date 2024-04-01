using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonFollowVisual : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    private Vector3 offset;
    private Transform pokeAttachTransform;
    private bool isFollowing = false;
    private XRBaseInteractable interactable;
    [SerializeField] AudioClip[] clips; // drag and add audio clips in the inspector
    AudioSource audioSource;
    private int clipIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        audioSource = GetComponent<AudioSource>();
    }

    public void Follow(BaseInteractionEventArgs hover){
        if (hover.interactorObject is XRPokeInteractor){
            XRPokeInteractor interactor = (XRPokeInteractor) hover.interactorObject;
            isFollowing = true;
            Debug.Log("Follow has been set!");
            // if (clipIndex >= clips.Length - 1){
            //     clipIndex = 0;
            // }else{ clipIndex++; }
            // audioSource.clip = clips[clipIndex];
            // audioSource.Play();
            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (isFollowing){
            // visualTarget.position = pokeAttachTransform.position + offset;
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);
            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
    }
}
