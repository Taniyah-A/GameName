using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private static readonly int Bounce = Animator.StringToHash("Bounce");
    public Animator animator;




    public void PlayBounce()
    {
        if (animator != null)
        {

            animator.SetTrigger(Bounce);
        }
    }
}