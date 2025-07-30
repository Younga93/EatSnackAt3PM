using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] InputActionAsset inputActions;

    private string inputBindingKey = "InputBindings";   //PlayerPrefs 키
    private void Awake()
    {
        //싱글톤
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //이미 변경되어서 저장된 키가 있는 지 확인: 아니라면 기본 바인딩 그대로 유지될것임.
        if (PlayerPrefs.HasKey(inputBindingKey))
        {
            inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(inputBindingKey));
        }
        else
        {
            ResetBinding();
        }
        //입력 감지 시작
        inputActions.Enable();
    }

    //유저가 설정한 키 바인딩 초기화
    public void ResetBinding()
    {
        inputActions.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(inputBindingKey);
        Debug.Log("키 설정 초기화됨");
    }

    //유저가 새로운 키를 입력했을 때 해당 액션에 바인딩 적용
    //InputManager.Instance.StartNewKeyBinding("Jump", 0); //키 바인딩 호출 예시
    public void StartNewKeyBinding(string actionName, int bindingIndex)
    {
        InputAction targetAction = inputActions.FindAction(actionName);

        if (targetAction == null)
        {
            Debug.Log($"{actionName}은 존재하지 않는 액션입니다.");
            return;
        }

        if (bindingIndex < 0 || bindingIndex >= targetAction.bindings.Count)
        {
            Debug.Log($"{actionName}은 {bindingIndex}번째 액션이 존재하지 않습니다.");
            return;
        }

        targetAction.Disable(); //리바인딩 하는 동안 disable
        var rebind = targetAction.PerformInteractiveRebinding(bindingIndex);

        Debug.Log($"새로운 키 바인딩을 시작합니다: {actionName}");
        Debug.Log("원하는 키를 입력해주세요.");

        rebind.OnComplete(
            operation =>
            {
                PlayerPrefs.SetString(inputBindingKey, inputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
                targetAction.Enable();

                Debug.Log($"Rebound '{targetAction}' to '{operation.selectedControl.displayName}'");
                operation.Dispose();
            });
        rebind.Start();
    }
}
