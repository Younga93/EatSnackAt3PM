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

        //테스트코드
        ResetBinding();
        //테스트코드 끝

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
    //테스트코드
    private void Start()
    {
        InputManager.Instance.StartNewKeyBinding("Jump", 0);
    }
    //테스트코드 끝

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

        //테스트코드
        Debug.Log("현재 jump: " + inputActions.FindAction(actionName).bindings[0]);
        Debug.Log("PlayerPref 저장 jump: " + PlayerPrefs.GetString(inputBindingKey));
        //테스트코드 끝

        InputAction targetAction = inputActions.FindAction(actionName);

        if (targetAction == null)   //target Action 예외처리
        {
            Debug.Log($"{actionName}은 존재하지 않는 액션입니다.");
            return;
        }

        if (bindingIndex < 0 || bindingIndex >= targetAction.bindings.Count)    //index 예외처리
        {
            Debug.Log($"{actionName}은 {bindingIndex}번째 액션이 존재하지 않습니다.");
            return;
        }

        targetAction.Disable(); //리바인딩 하는 동안 disable
        var rebind = targetAction.PerformInteractiveRebinding(bindingIndex).WithControlsExcluding("Mouse"); //마우스 제외한 입력장치만 받게 함. (디버그로그 테스트하는데 마우스 스크롤로 바뀌어서 변경함)

        Debug.Log($"새로운 키 바인딩을 시작합니다: {actionName}");
        Debug.Log("원하는 키를 입력해주세요.");

        //
        rebind.OnComplete(
            operation =>
            {
                string newKey = operation.selectedControl.path;
                if (!IsBindingConflict(actionName, newKey))
                {
                    PlayerPrefs.SetString(inputBindingKey, inputActions.SaveBindingOverridesAsJson()); //PlayerPrefs에 변경사항 저장
                    PlayerPrefs.Save();
                    Debug.Log($"Rebound '{targetAction}' to '{operation.selectedControl.displayName}'");    //변경 로그
                }
                else
                {
                    Debug.Log($"Rebound failed");    //변경 로그
                }

                targetAction.Enable();
                operation.Dispose();
            });
        rebind.Start(); //리바인딩 시작

        //테스트코드
        Debug.Log("현재 jump: " +inputActions.FindAction(actionName).bindings[0]);
        Debug.Log("PlayerPref 저장 jump: " + PlayerPrefs.GetString(inputBindingKey));
        //테스트코드 끝
    }
    bool IsBindingConflict(string actionName, string newBindingPath)
    {
        if (!newBindingPath.StartsWith('<')) //만약 받은 path가 <로 시작하지 않으면 binding.effectivePath와 형식이 맞지 않음으로 포맷팅 새로함.
        {
            newBindingPath = newBindingPath.Replace("/Keyboard/", "<Keyboard>/");
        }
        foreach (var action in inputActions)
        {
            if (action.name == actionName)
                continue;
            foreach (var binding in action.bindings)
            {
                Debug.Log($"binding.effectivePath: {binding.effectivePath}");
                Debug.Log($"newBindingPath: {newBindingPath}");
                if (binding.effectivePath == newBindingPath)
                {  
                    return true; //이미 사용중임
                }

            }
        }
        return false;
    }
}
