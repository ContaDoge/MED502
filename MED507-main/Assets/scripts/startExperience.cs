using UnityEngine;
using UnityEngine.InputSystem;

public class startExperience : MonoBehaviour
{
    [SerializeField] private GameObject sceneObject;
    private SceneChange sceneCtrl;
    [SerializeField] InputActionProperty buttonP;

    private void Start()
    {
        sceneObject = this.gameObject;
        sceneCtrl = sceneObject.GetComponent<SceneChange>();
    }
    public void StartExperienceButton()
    {
        sceneCtrl.FadeBlackoutSquare(true);
        new WaitForSeconds(5f);
        sceneCtrl.StartLoadNextScene();
    }
    private void Update()
    {
        
    }
}
