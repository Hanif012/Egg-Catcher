using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Hit();
        }
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
