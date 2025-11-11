//csharp Assets\scripts\startExperience.cs
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartExperience2 : MonoBehaviour
{
    [SerializeField] private GameObject sceneObject;
    private SceneChange sceneCtrl;
    [SerializeField] InputActionProperty buttonP;
    private bool started = false;

    private void Start()
    {
        sceneObject = this.gameObject;
        sceneCtrl = sceneObject.GetComponent<SceneChange>();
    }

    private void OnEnable()
    {
        if (buttonP != null && buttonP.action != null)
        {
            buttonP.action.Enable();
            buttonP.action.performed += OnButtonPerformed;
        }
    }

    private void OnDisable()
    {
        if (buttonP != null && buttonP.action != null)
        {
            buttonP.action.performed -= OnButtonPerformed;
            buttonP.action.Disable();
        }
    }

    private void OnButtonPerformed(InputAction.CallbackContext ctx)
    {
        // If you want to check value you can read ctx.ReadValue<float>() or ctx.ReadValue<bool>()
        if (started) return;
        StartExperienceButton();
    }

    public void StartExperienceButton()
    {
        if (started) return;
        started = true;
        StartCoroutine(StartExperienceRoutine());
    }

    private IEnumerator StartExperienceRoutine()
    {
        StartCoroutine(sceneCtrl.FadeBlackoutSquare(true));
        yield return new WaitForSeconds(5f);
        sceneCtrl.StartLoadNextScene();
    }
}