using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] InputActionAsset inputActions;

    public string inputBindingKey { get; private set; } = "InputBindings";   //PlayerPrefs 키
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
        if (!PlayerPrefs.HasKey(inputBindingKey))
        {
            ResetBinding();
        }
        inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(inputBindingKey));
        //입력 감지 시작
        inputActions.Enable();

    }
    ////테스트코드
    //private void Start()
    //{
    //    InputManager.Instance.StartNewKeyBinding("Jump", 0);
    //}
    //테스트코드 끝

    //유저가 설정한 키 바인딩 초기화
    public void ResetBinding()
    {
        inputActions.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(inputBindingKey);
        PlayerPrefs.SetString(inputBindingKey, inputActions.SaveBindingOverridesAsJson());
        PlayerPrefs.Save();
        Debug.Log("키 설정 초기화됨");
    }

    //유저가 새로운 키를 입력했을 때 해당 액션에 바인딩 적용
    //InputManager.Instance.StartNewKeyBinding("Jump", 0); //키 바인딩 호출 예시
    public void StartNewKeyBinding(string actionName, int bindingIndex, Action<string> onBindingComplete)
    {
        InputAction targetAction = inputActions.FindAction(actionName);

        if (targetAction == null)   //target Action 예외처리
        {
            Debug.Log($"{actionName}은 존재하지 않는 액션입니다.");
            onBindingComplete?.Invoke("");  //실패시 "" 반환
            return;
        }

        if (bindingIndex < 0 || bindingIndex >= targetAction.bindings.Count)    //index 예외처리
        {
            Debug.Log($"{actionName}은 {bindingIndex}번째 액션이 존재하지 않습니다.");
            onBindingComplete?.Invoke("");  //실패시 "" 반환
            return;
        }

        //SystemPanel로 메시지 띄움
        UIManager.Instance.ShowSystemMessagePanel($"Press key for {actionName}", false);

        targetAction.Disable(); //리바인딩 하는 동안 disable
        var rebind = targetAction.PerformInteractiveRebinding(bindingIndex).WithControlsExcluding("Mouse"); //마우스 제외한 입력장치만 받게 함. (디버그로그 테스트하는데 마우스 스크롤로 바뀌어서 변경함)

        Debug.Log($"새로운 키 바인딩을 시작합니다: {actionName}");

        //바인딩 변경 전 오버라이드 저장하기
        //var previousOverride = targetAction.bindings[bindingIndex].overridePath;
        string previousOverride = !string.IsNullOrEmpty(targetAction.bindings[bindingIndex].overridePath)
            ? targetAction.bindings[bindingIndex].overridePath
            : targetAction.bindings[bindingIndex].path;

        rebind.OnComplete(
            operation =>
            {
                string newKey = operation.selectedControl.path;
                if(operation.selectedControl.name.ToLower() == "escape")
                {
                    Debug.Log($"ESC를 눌러 취소합니다.");    //변경 로그
                    targetAction.ApplyBindingOverride(bindingIndex, previousOverride);
                    SaveAndInvokeEmpty();
                }
                else if (!IsBindingConflict(actionName, newKey))
                {
                    SaveAndInvokeNewKey(newKey);
                }
                else
                {
                    targetAction.ApplyBindingOverride(bindingIndex, previousOverride);
                    SaveAndInvokeEmpty();
                }

                UIManager.Instance.CloseSystemMessagePanel();
                targetAction.Enable();
                operation.Dispose();

                void SaveAndInvokeEmpty()
                {
                    PlayerPrefs.SetString(inputBindingKey, inputActions.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();
                    onBindingComplete?.Invoke("");
                    Debug.Log("리바인딩이 취소되거나 실패하여 이전 바인딩으로 돌아갑니다.");
                }
                void SaveAndInvokeNewKey(string key)
                {
                    PlayerPrefs.SetString(inputBindingKey, inputActions.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();
                    onBindingComplete?.Invoke(key);
                    Debug.Log($"Rebound '{targetAction}' to '{key}'");
                }
            });
        rebind.Start(); //리바인딩 시작
    }
    bool IsBindingConflict(string actionName, string newBindingPath)
    {
        //newBindingPath = GetFormattedKeyBoardValue(newBindingPath);
        if (!newBindingPath.StartsWith('<')) //만약 받은 path가 <로 시작하지 않으면 binding.effectivePath와 형식이 맞지 않음으로 포맷팅 새로함.
        {
            newBindingPath = newBindingPath.Replace("/Keyboard/", "<Keyboard>/");
        }
        foreach (var action in inputActions)
        {
            if (action.name == actionName)
                continue;

            Debug.Log($"binding.effectivePath: {action.bindings[0].effectivePath}");
            Debug.Log($"newBindingPath: {newBindingPath}");
            foreach (var binding in action.bindings)
            {
                if (binding.effectivePath == newBindingPath)
                {
                    Debug.Log($"{binding.effectivePath == newBindingPath}가 true임");
                    return true;
                }
                Debug.Log($"{binding.effectivePath == newBindingPath}가 false임");
            }
            //if (action.bindings[0].effectivePath == newBindingPath) //각 액션은 키 하나랑만 바인딩 되어있음.
            //{
            //    return true; //이미 사용중임
            //}
        }
        return false;
    }
    public string GetFormattedKeyBoardValue(string inputBindingKey) // device/key 형식으로 출력됨
    {
        string formattedKeyValue;

        if(inputBindingKey.StartsWith('<'))
        {
            formattedKeyValue = inputBindingKey.Replace(">", "").Substring(1);  //<탈락 시키고, >를 지움
        }
        else if (inputBindingKey.StartsWith('/'))
        {
            formattedKeyValue = inputBindingKey.Substring(1);   //제일 처음 /탈락
            //formattedKeyValue = "<" + formattedKeyValue.Replace("/", ">/");
        }
        else
        {
            formattedKeyValue = inputBindingKey;
        }
        return formattedKeyValue;
    }
    public Dictionary<string,string> GetCurrentBindingDictionary()
    {
        Dictionary<string, string> currentBinding = new Dictionary<string, string>();

        foreach (var action in inputActions)
        {
            currentBinding.Add(action.name, GetFormattedKeyBoardValue(action.bindings[0].effectivePath));  //각 키는 바인딩 하나만 갖고 있음.
        }

        return currentBinding;
    }
}
