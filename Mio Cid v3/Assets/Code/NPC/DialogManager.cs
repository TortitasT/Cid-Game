using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance = null;

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject title;
    [SerializeField] GameObject content;

    private int index = 0;
    private bool isStarting = false;
    private bool isActive = false;
    private Dialog dialog = null;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isActive)
            {
                NextDialog();
            }
        }
    }

    public bool IsActive()
    {
        return isActive;
    }
    private IEnumerator SetActive()
    {
        yield return new WaitForSeconds(1f);
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

            canvas.SetActive(true);
            GetComponent<CameraFollow>().SetTarget(transform);
            GetComponent<CameraFollow>().SetZoom(15f);

            title.GetComponent<TextMeshProUGUI>().text = this.dialog.titles[index];
            content.GetComponent<TextMeshProUGUI>().text = this.dialog.contents[index];

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
                content.GetComponent<TextMeshProUGUI>().text = dialog.contents[index];
            }
        }
    }

    public void EndDialog()
    {
        isActive = false;
        canvas.SetActive(false);
        GetComponent<CameraFollow>().Reset();
        StateManager.Instance.SetState(StateManager.State.Idle);
    }
}
