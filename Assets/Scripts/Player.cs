using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private bool stopTouch = false;

    [SerializeField] private float swipeRange;

    private bool up;
    private bool right;
    private bool left;
    private bool down;

    void Update()
    {
        Swipe();
    }

    public bool GetSwipeUp()
    {
        return up;
    }

    public bool GetSwipeDown()
    {
        return down;
    }

    public bool GetSwipeRight()
    {
        return right;
    }

    public bool GetSwipeLeft()
    {
        return left;
    }

    public void SetSwipeReset()
    {
        up = false;
        right = false;
        left = false;
        down = false;
    }

    public void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if (!stopTouch)
            {
                if (Distance.x < -swipeRange) //Left
                {
                    up = false;
                    right = false;
                    left = true;
                    down = false;
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange) //Right
                {
                    up = false;
                    right = true;
                    left = false;
                    down = false;
                    stopTouch = true;
                }
                else if (Distance.y > swipeRange) //Up
                {
                    up = true;
                    right = false;
                    left = false;
                    down = false;
                    stopTouch = true;
                }
                else if (Distance.y < -swipeRange) //Down
                {
                    down = true;
                    up = false;
                    right = false;
                    left = false;
                    stopTouch = true;
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            up = false;
            right = false;
            left = false;
            down = false;
        }
    }
}
