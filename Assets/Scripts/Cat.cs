using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private CanvasGroup gameOver;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        GameOver();
    }

    private void GameOver()
    {
        if (gameOver.interactable)
        {
            anim.SetBool("gameOver", true);
        }
        else
        {
            anim.SetBool("gameOver", false);
        }
        

    }
}
