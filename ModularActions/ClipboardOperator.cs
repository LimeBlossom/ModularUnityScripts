using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardOperator : MonoBehaviour
{
    [SerializeField] private StringReference toOperate;
    [SerializeField] private string preText = "";
    
    public void CopyStringToClipboard()
    {
        TextEditor te = new TextEditor { text = preText + toOperate.value };
        te.SelectAll();
        te.Copy();
    }

    public void PasteStringFromClipboard()
    {
        TextEditor te = new TextEditor();
        te.Paste();
        toOperate.variable.value = te.text;
    }
}
