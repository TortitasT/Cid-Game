using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteract
{
    [SerializeField] private List<string> titles;
    [SerializeField] private List<string> contents;
    private Dialog dialog = null;

    private void Start()
    {
        dialog = new Dialog(titles, contents);
    }
    public bool Interact()
    {
        Debug.Log(dialog.contents.Count);
        if (!DialogManager.Instance.IsActive() && dialog.contents.Count != 0)
        {
            DialogManager.Instance.BeginDialog(dialog, this.transform);
            return true;
        }
        return false;
    }
}
