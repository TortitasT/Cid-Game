using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance = null;

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject title;
    [SerializeField] GameObject content;
    [SerializeField] GameObject box;

    private int index = 0;
    private bool isStarting = false;
    private bool isActive = false;
    private Dialog dialog = null;
    bool can = false;
    private void Awake()
    {
        canvas.SetActive(false);
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive)
        {
            can = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (isActive && can)
            {
                NextDialog();
                can = false;
            }
        }
    }

    public bool IsActive()
    {
        return isActive;
    }
    private IEnumerator SetActive()
    {
        yield return new WaitForSeconds(0.1f);
        isActive = true;
        isStarting = false;
    }
    public void BeginDialog(Dialog dialog, Transform transform)
    {
        if (!isActive && !isStarting)
        {
            isStarting = true;

            index = 0;
            this.dialog = dialog;

            ShowDialog();
            GetComponent<CameraFollow>().SetTarget(transform);
            GetComponent<CameraFollow>().SetZoom(20f);
            GetComponent<CameraFollow>().SetOffset(new Vector3(0, -3, 0));

            title.GetComponent<TextMeshProUGUI>().text = this.dialog.titles[index];
            content.GetComponent<TextAnimatorPlayer>().ShowText(this.dialog.contents[index]);

            StateManager.Instance.SetState(StateManager.State.Talking);
            StartCoroutine(SetActive());
        }
    }

    public void NextDialog()
    {
        if (isActive)
        {
            index++;

            if (index >= dialog.titles.Count)
            {
                isActive = false;
                EndDialog();
            }
            else
            {
                title.GetComponent<TextMeshProUGUI>().text = dialog.titles[index];
                content.GetComponent<TextAnimatorPlayer>().ShowText(dialog.contents[index]);
            }
        }
    }

    public void EndDialog()
    {
        // isActive = false & state set on end of hide animation below
        HideDialog();
        GetComponent<CameraFollow>().Reset();
        StateManager.Instance.gameObject.GetComponent<AnimationManager>().StopLooking();
    }

    private void ShowDialog()
    {
        canvas.SetActive(true);
        LeanTween.scale(box, new Vector3(0, 0, 0), 0f);
        LeanTween.scale(box, new Vector3(1, 1, 1), 0.3f).setEase(LeanTweenType.easeInOutBack);
    }
    private void HideDialog()
    {
        LeanTween.scale(box, new Vector3(1, 1, 1), 0f);
        LeanTween.scale(box, new Vector3(0, 0, 0), 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            StateManager.Instance.SetState(StateManager.State.Idle);
            isActive = false;

            canvas.SetActive(false);
        });
    }
}
