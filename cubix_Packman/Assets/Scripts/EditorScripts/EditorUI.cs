using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorUI : MonoBehaviour {

    public GameObject Editor;
    public LevelEditor editorScript;

    public Button part01;
    public Button part02;
    public Button part03;
    public Button part04;
    public Button part05;
    public GameObject SavePanel;
    public GameObject LoadPanel;
    public InputField inputField_SaveAs;
    public InputField inputField_LoadFrom;
    bool canEnterLevelName;
    public Camera mainCamera;
    public Camera editorCamera;
    ColorBlock deselectColor;
    ColorBlock selectColor;
    public bool enteringFlag = false;
    private void Start()
    {
        canEnterLevelName = false;
        selectColor.normalColor = Color.green;
        selectColor.colorMultiplier = 1;
        deselectColor.normalColor = Color.white;
        deselectColor.colorMultiplier = 1;
        editorScript = Editor.GetComponent<LevelEditor>();
    }
    private void Update()
    {
        if (Input.GetKeyDown("s") && !editorScript.firstTime)
        {
            editorScript.isSaving = true;
            SavePanel.SetActive(true);
            inputField_SaveAs.ActivateInputField();
            canEnterLevelName = true;
        }
        if((Input.GetKeyDown(KeyCode.Return) || enteringFlag) && canEnterLevelName)
        {
            canEnterLevelName = false;
            enteringFlag = false;
            SaveManager.levelName = inputField_SaveAs.text.ToString();
            editorScript.Save();
            Debug.Log(SaveManager.levelName);
            SavePanel.SetActive(false);
        }

        switch (editorScript.currentPrefab)
        {
            case PrefabType.Part01:
                part01.colors = selectColor;
                part02.colors = deselectColor;
                part03.colors = deselectColor;
                part04.colors = deselectColor;
                part05.colors = deselectColor;
                break;
            case PrefabType.Part02:
                part02.colors = selectColor;
                part01.colors = deselectColor;
                part03.colors = deselectColor;
                part04.colors = deselectColor;
                part05.colors = deselectColor;
                break;
            case PrefabType.Part03:
                part03.colors = selectColor;
                part02.colors = deselectColor;
                part01.colors = deselectColor;
                part04.colors = deselectColor;
                part05.colors = deselectColor;
                break;
            case PrefabType.Part04:
                part04.colors = selectColor;
                part02.colors = deselectColor;
                part03.colors = deselectColor;
                part01.colors = deselectColor;
                part05.colors = deselectColor;
                break;
            case PrefabType.Part05:
                part05.colors = selectColor;
                part02.colors = deselectColor;
                part03.colors = deselectColor;
                part04.colors = deselectColor;
                part01.colors = deselectColor;
                break;

        }
    }
    public void OnLoad(string Name)
    {
        editorScript.firstTime = false;
        Name = inputField_LoadFrom.text.ToString();
        SaveManager.levelName = Name;
        if(editorScript.Load() == 0)
        {
            Debug.Log("no level match found..");
        }
        LoadPanel.SetActive(false);
        editorScript.ChangeMazeSize();

    }
    public void DoneEntering()
    {
        enteringFlag = true;
    }
    public void OnSave()
    {
        editorScript.Save();
    }
    public void SizeChange(int index)
    {
        mainCamera.orthographicSize = index + 3;
        editorCamera.orthographicSize = index + 3;
        editorScript.MazeSize = index + 1;
        editorScript.ChangeMazeSize();
        editorScript.firstTime = false;
        SaveManager.levelSize = editorScript.MazeSize;
    }

}
