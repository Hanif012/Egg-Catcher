using InputSamples.Gestures;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private Animator animator;
    InputAction Swipe;
    
    [SerializeField]
    private GestureController gestureController;


    void Start()
    {
        // Swipe = InputSystem.actions.FindAction("Swipe");
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    //    if(currentSwipeInput.TravelDistance > 100)
    //    {
    //        animator.SetTrigger("Attack");
    //        Debug.Log("Attack");
    //    }
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }
}
