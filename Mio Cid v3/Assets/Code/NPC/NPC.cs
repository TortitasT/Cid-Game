using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteract
{
    [SerializeField] private Dialog dialog = null;

    private void Start()
    {
        string title1 = "title1";
        string title2 = "title2";

        string content1 = "content1";
        string content2 = "content2";

        dialog = new Dialog(new List<string> { title1, title2 }, new List<string> { content1, content2 });
    }
    public void Interact()
    {
        if (!DialogManager.Instance.IsActive())
        {
            DialogManager.Instance.BeginDialog(dialog, this.transform);
        }
    }
}
