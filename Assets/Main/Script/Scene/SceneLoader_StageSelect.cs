using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneLoader_StageSelect : MonoBehaviour
{
    [SerializeField] SceneObject stage1;
    [SerializeField] SceneObject stage2;
    [SerializeField] SceneObject stage3;
    [SerializeField] SceneObject setting;
    [SerializeField] SceneObject credit;
    [SerializeField] SceneObject title;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void SceneLoad(SceneObject scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadStage1()
    {
        SceneLoad(stage1);
    }
    public void LoadStage2()
    {
        SceneLoad(stage2);
    }
    public void LoadStage3()
    {
        SceneLoad(stage3);
    }
    public void LoadSetting()
    {
        SceneLoad(setting);
    }
    public void LoadCredit()
    {
        SceneLoad(credit);
    }
    public void LoadTitle()
    {
        SceneLoad(title);
    }

}
