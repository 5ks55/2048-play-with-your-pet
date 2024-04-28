using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform backgroundPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshould;
    int activePage;
    private int backgroundIndex;


    private void Start()
    {
        backgroundIndex = PlayerPrefs.GetInt("SelectBackground");
        if (backgroundIndex > 2)
            backgroundIndex = 0;
        activePage = backgroundIndex + 1;
        while (currentPage != activePage)
        {
            if (currentPage < maxPage)
            {
                currentPage++;
                targetPos += pageStep;
                MovePage();
            }
        }

    }

    private void Awake()
    {
        currentPage = 1;
        targetPos = backgroundPagesRect.localPosition;
        dragThreshould = Screen.width / 20;
    }

    private void MovePage()
    {
        backgroundPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        
        for (int i = 0; i < maxPage; i++)
        {
            float scaleFactor = (i + 1 == activePage) ? 0.48f : 0.4f; 
            backgroundPagesRect.GetChild(i).localScale = new Vector3(scaleFactor, scaleFactor, 1.0f);
        }
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            activePage = currentPage; 
            backgroundIndex++;
            MovePage();
        }
    }

    public void Previous()
    {
        if(currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            activePage = currentPage; 
            backgroundIndex--;
            MovePage();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x)
            {
                Previous();
            }
            else 
            { 
                Next(); 
            }
        }
        else
        {
            MovePage();
        }
    }

    public void SetBackground()
    {
        PlayerPrefs.SetInt("SelectBackground", backgroundIndex);
        SceneManager.LoadSceneAsync(0);
    }
}