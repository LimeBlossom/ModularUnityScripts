using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {

    [SerializeField] private GameObject dialogContainerPrefab;
    [SerializeField] private GameObject dialogContainer;
    [SerializeField] private TextAsset dialogFile;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private IntReference pauseRef;
    [SerializeField] private IntReference unpauseOnInputRef;
    [SerializeField] private UnityEvent events;
    [SerializeField] private MonoBehaviour[] actions;

    [SerializeField] private bool nextOnGrab = false;
    [SerializeField] private bool nextOnMove = false;
    [SerializeField] private bool nextOnTimeBurst = false;
    [SerializeField] private bool nextOnTimeRestore = false;
    [SerializeField] private bool nextOnRotate = false;

    [SerializeField] private DialogAnimationController dialogAnimationController;

    private GameObject nextButton;
    private int iterator;
    private string[] dialogLine;

    private TextMeshProUGUI message;

    private string[] choiceActions = new string[3];

    void Start()
    {
        if(dialogFile != null)
        {
            if (dialogContainer == null)
            {
                dialogContainer = Instantiate(dialogContainerPrefab);
            }
            message = dialogContainer.GetComponentInChildren<TextMeshProUGUI>();
            dialogAnimationController = dialogContainer.GetComponentInChildren<DialogAnimationController>();
            SetDialogFile(dialogFile);
            StartDialog();
        }
    }

    private void OnMove()
    {
        if(pauseRef.value == 1)
        {
            return;
        }
        if (nextOnMove)
        {
            nextOnMove = false;
            NextMessage();
        }
    }

    private void OnGrab()
    {
        if (pauseRef.value == 1)
        {
            return;
        }
        if (nextOnGrab)
        {
            nextOnGrab = false;
            NextMessage();
        }
    }

    private void OnFinishLevel()
    {
        if (pauseRef.value == 1)
        {
            return;
        }
        if (nextOnTimeRestore)
        {
            nextOnTimeRestore = false;
            NextMessage();
        }
    }

    private void OnTimeBurst()
    {
        if (pauseRef.value == 1)
        {
            return;
        }
        if (nextOnTimeBurst)
        {
            nextOnTimeBurst = false;
            NextMessage();
        }
    }

    private void OnRotateItemCW()
    {
        if (pauseRef.value == 1)
        {
            return;
        }
        if (nextOnRotate)
        {
            nextOnRotate = false;
            NextMessage();
        }
    }

    private void OnRotateItemCCW()
    {
        if (pauseRef.value == 1)
        {
            return;
        }
        if (nextOnRotate)
        {
            nextOnRotate = false;
            NextMessage();
        }
    }

    public void SetDialogFile(TextAsset dialog){
		dialogFile = dialog;
		if(dialogFile != null)
		{
			dialogLine = (dialogFile.text.Split('\n'));
		}
	}

	public void NextMessage(){
		message.text = "";
		if (dialogLine [iterator].IndexOf ("@") == 0) {
			iterator++;
			NextMessage ();
			return;
		} else {
			string[] dialogWord = dialogLine [iterator].Split (' ');
			for (int i = 0; i < dialogWord.Length; i++)
            {
                if(CheckInputWord(dialogWord[i]))
                {
                    continue;
                }
                if(CheckWaitWord(dialogWord[i]))
                {
                    continue;
                }
                if(CheckActivationWord(dialogWord[i]))
                {
                    continue;
                }
                if(CheckGameActions(dialogWord[i]))
                {
                    continue;
                }
                if(CheckSpawnActions(dialogWord[i]))
                {
                    continue;
                }
                if(CheckAnimationActions(dialogWord[i]))
                {
                    continue;
                }

                switch(dialogWord[i])
                {
                    case "#ShowChoices":
                        message.gameObject.SetActive(false);
                        nextButton.SetActive(false);
                        break;
                    case "#ShowDialog":
                        message.gameObject.SetActive(true);
                        nextButton.SetActive(true);
                        break;
                    case "#Choice1":
                        choiceActions[0] = dialogWord[i + 1];
                        i++;
                        break;
                    case "#Choice2":
                        choiceActions[1] = dialogWord[i + 1];
                        i++;
                        break;
                    case "#Choice3":
                        choiceActions[2] = dialogWord[i + 1];
                        i++;
                        break;
                    case "#CloseDialog":
                        EndDialog();
                        return;
                    case ".":
                        message.text += ".";
                        break;
                    case "#n":
                        message.text += "\n";
                        break;
                    default:
                        if (dialogWord[i].Contains("#goto:"))
                        {
                            string locationName = dialogWord[i].Split(':')[1];
                            for (int j = 0; j < dialogLine.Length; j++)
                            {
                                if (dialogLine[i] == "@" + locationName)
                                {
                                    iterator = i;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                message.text += dialogWord[i];
                            }
                            else
                            {
                                message.text += " " + dialogWord[i];
                            }
                        }
                        break;
                }
			}
			iterator++;
		}
		if (message.text == "") {
			NextMessage ();
			return;
		}
	}

    private bool CheckSpawnActions(string word)
    {
        switch(word)
        {
            case "#DespawnLeft":
                dialogAnimationController.DestroyCharacter(DialogAnimationController.Direction.LEFT);
                break;
            case "#DespawnRight":
                dialogAnimationController.DestroyCharacter(DialogAnimationController.Direction.RIGHT);
                break;
            case "#SpawnCaptainLeft":
                dialogAnimationController.SpawnCharacter("Captain", DialogAnimationController.Direction.LEFT);
                break;
            case "#SpawnCaptainRight":
                dialogAnimationController.SpawnCharacter("Captain", DialogAnimationController.Direction.RIGHT);
                break;
            case "#SpawnCrewLeft":
                dialogAnimationController.SpawnCharacter("Crew", DialogAnimationController.Direction.LEFT);
                break;
            case "#SpawnCrewRight":
                dialogAnimationController.SpawnCharacter("Crew", DialogAnimationController.Direction.RIGHT);
                break;
            case "#SpawnColonialLeft":
                dialogAnimationController.SpawnCharacter("Colonial", DialogAnimationController.Direction.LEFT);
                break;
            case "#SpawnColonialRight":
                dialogAnimationController.SpawnCharacter("Colonial", DialogAnimationController.Direction.RIGHT);
                break;
            case "#SpawnCookLeft":
                dialogAnimationController.SpawnCharacter("Cook", DialogAnimationController.Direction.LEFT);
                break;
            case "#SpawnCookRight":
                dialogAnimationController.SpawnCharacter("Cook", DialogAnimationController.Direction.RIGHT);
                break;
            case "#SpawnDoctorLeft":
                dialogAnimationController.SpawnCharacter("Doctor", DialogAnimationController.Direction.LEFT);
                break;
            case "#SpawnDoctorRight":
                dialogAnimationController.SpawnCharacter("Doctor", DialogAnimationController.Direction.RIGHT);
                break;
            case "#SpawnSonLeft":
                dialogAnimationController.SpawnCharacter("Son", DialogAnimationController.Direction.LEFT);
                break;
            case "#SpawnSonRight":
                dialogAnimationController.SpawnCharacter("Son", DialogAnimationController.Direction.RIGHT);
                break;
            default:
                return false;
        }
        return true;
    }

    private bool CheckAnimationActions(string word)
    {
        switch (word)
        {
            case "#LeftAgony":
                dialogAnimationController.AnimateCharacter("Agony", DialogAnimationController.Direction.LEFT);
                break;
            case "#RightAgony":
                dialogAnimationController.AnimateCharacter("Agony", DialogAnimationController.Direction.RIGHT);
                break;
            case "#LeftCheer":
                dialogAnimationController.AnimateCharacter("Cheer", DialogAnimationController.Direction.LEFT);
                break;
            case "#RightCheer":
                dialogAnimationController.AnimateCharacter("Cheer", DialogAnimationController.Direction.RIGHT);
                break;
            case "#LeftHorror":
                dialogAnimationController.AnimateCharacter("Horror", DialogAnimationController.Direction.LEFT);
                break;
            case "#RightHorror":
                dialogAnimationController.AnimateCharacter("Horror", DialogAnimationController.Direction.RIGHT);
                break;
            case "#LeftIdle":
                dialogAnimationController.AnimateCharacter("Idle", DialogAnimationController.Direction.LEFT);
                break;
            case "#RightIdle":
                dialogAnimationController.AnimateCharacter("Idle", DialogAnimationController.Direction.RIGHT);
                break;
            case "#LeftQuestion":
                dialogAnimationController.AnimateCharacter("Question", DialogAnimationController.Direction.LEFT);
                break;
            case "#RightQuestion":
                dialogAnimationController.AnimateCharacter("Question", DialogAnimationController.Direction.RIGHT);
                break;
            case "#LeftSad":
                dialogAnimationController.AnimateCharacter("Sad", DialogAnimationController.Direction.LEFT);
                break;
            case "#RightSad":
                dialogAnimationController.AnimateCharacter("Sad", DialogAnimationController.Direction.RIGHT);
                break;
            case "#LeftTalk":
                dialogAnimationController.AnimateCharacter("Talk", DialogAnimationController.Direction.LEFT);
                break;
            case "#RightTalk":
                dialogAnimationController.AnimateCharacter("Talk", DialogAnimationController.Direction.RIGHT);
                break;
            case "#LeftWave":
                dialogAnimationController.AnimateCharacter("Wave", DialogAnimationController.Direction.LEFT);
                break;
            case "#RightWave":
                dialogAnimationController.AnimateCharacter("Wave", DialogAnimationController.Direction.RIGHT);
                break;
            case "#LeftYes":
                dialogAnimationController.AnimateCharacter("Yes", DialogAnimationController.Direction.LEFT);
                break;
            case "#RightYes":
                dialogAnimationController.AnimateCharacter("Yes", DialogAnimationController.Direction.RIGHT);
                break;
            default:
                return false;
        }
        return true;
    }

    private bool CheckGameActions(string word)
    {
        switch(word)
        {
            case "#Pause":
                MessageCenter.InvokeMessage("Pause", "");
                break;
            case "#Unpause":
                MessageCenter.InvokeMessage("Unpause", "");
                break;
            case "#EnableUnpauseOnInput":
                MessageCenter.InvokeMessage("EnableUnpauseOnInput", "");
                break;
            case "#DisableUnpauseOnInput":
                MessageCenter.InvokeMessage("DisableUnpauseOnInput", "");
                break;
            case "#FinishLevel":
                MessageCenter.InvokeMessage("FinishLevel", "");
                break;
            default:
                return false; // Did not hit a case.
        }
        return true; // Found a case.
    }

    private bool CheckActivationWord(string word)
    {
        switch(word)
        {
            case "#Events":
                ActivateEvents();
                break;
            case "#Activate":
                ActivateAll();
                break;
            case "#Activate0":
                Activate(0);
                break;
            case "#Activate1":
                Activate(1);
                break;
            case "#Activate2":
                Activate(2);
                break;
            case "#Activate3":
                Activate(3);
                break;
            default:
                return false; // Did not hit a case.
        }
        return true; // Found a case.
    }

    private bool CheckWaitWord(string word)
    {
        switch(word)
        {
            case "#NextOnGrab":
                nextOnGrab = true;
                break;
            case "#NextOnMove":
                nextOnMove = true;
                break;
            case "#NextOnTimeBurst":
                nextOnTimeBurst = true;
                break;
            case "#NextOnTimeRestore":
                nextOnTimeRestore = true;
                break;
            case "#NextOnRotate":
                nextOnRotate = true;
                break;
            default:
                return false; // Did not hit a case.
        }
        return true; // Found a case.
    }

    private bool CheckInputWord(string word)
    {
        switch (word)
        {
            case "#GrabButton":
                if (Gamepad.current != null && Gamepad.current.lastUpdateTime > Keyboard.current.lastUpdateTime &&
                    Gamepad.current.lastUpdateTime > Mouse.current.lastUpdateTime)
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Grab").
                        GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                else
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Grab").
                        GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                break;
            case "#TimeBurstButton":
                if (Gamepad.current != null && Gamepad.current.lastUpdateTime > Keyboard.current.lastUpdateTime &&
                    Gamepad.current.lastUpdateTime > Mouse.current.lastUpdateTime)
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Time Burst").
                        GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                else
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Time Burst").
                        GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                break;
            case "#TimeRestoreButton":
                if (Gamepad.current != null && Gamepad.current.lastUpdateTime > Keyboard.current.lastUpdateTime &&
                    Gamepad.current.lastUpdateTime > Mouse.current.lastUpdateTime)
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Finish Level").
                        GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                else
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Finish Level").
                        GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                break;
            case "#PauseButton":
                if (Gamepad.current != null && Gamepad.current.lastUpdateTime > Keyboard.current.lastUpdateTime &&
                    Gamepad.current.lastUpdateTime > Mouse.current.lastUpdateTime)
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Pause").
                        GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                else
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Pause").
                        GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                break;
            case "#MoveStyleButton":
                if (Gamepad.current != null && Gamepad.current.lastUpdateTime > Keyboard.current.lastUpdateTime &&
                    Gamepad.current.lastUpdateTime > Mouse.current.lastUpdateTime)
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Move Style").
                        GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                else
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Move Style").
                        GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                break;
            case "#MovementButtons":
                if (Gamepad.current != null && Gamepad.current.lastUpdateTime > Keyboard.current.lastUpdateTime &&
                    Gamepad.current.lastUpdateTime > Mouse.current.lastUpdateTime)
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Move").
                        GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                else
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Move").
                        GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                break;
            case "#RotateButtons":
                if (Gamepad.current != null && Gamepad.current.lastUpdateTime > Keyboard.current.lastUpdateTime &&
                    Gamepad.current.lastUpdateTime > Mouse.current.lastUpdateTime)
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Rotate Item CCW").
                        GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontIncludeInteractions)
                        + "/" + inputActions.FindActionMap("Gameplay").FindAction("Rotate Item CW").
                        GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                else
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Rotate Item CCW").
                        GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions)
                        + "/" + inputActions.FindActionMap("Gameplay").FindAction("Rotate Item CW").
                        GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                break;
            case "#RotatePlayerButtons":
                if (Gamepad.current != null && Gamepad.current.lastUpdateTime > Keyboard.current.lastUpdateTime &&
                    Gamepad.current.lastUpdateTime > Mouse.current.lastUpdateTime)
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Rotate Player").
                        GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                else
                {
                    message.text += inputActions.FindActionMap("Gameplay").FindAction("Rotate Player").
                        GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions);
                }
                break;
            default:
                return false; // Did not hit a case.
        }
        return true; // Found a case.
    }

	public void SetMessage(string newMessage){
		message.text = newMessage;
	}

	public void EndDialog(){
        MessageCenter.InvokeMessage("EnableUnpauseOnInput", "");
        dialogContainer.SetActive (false);
	}

	public void StartDialog(){
		iterator = 0;
		message.text = "";
		dialogContainer.SetActive (true);
		message.gameObject.SetActive (true);
		NextMessage();
	}

	public void ChooseOption(int optionNumber){
		if(choiceActions[optionNumber].Contains("#goto:")){
			string locationName = choiceActions [optionNumber].Split (':') [1];
			for(int i = 0; i < dialogLine.Length; i++){
				if(dialogLine[i] == "@" + locationName) {
					iterator = i;
					NextMessage ();
					break;
				}
			}
		}
	}

    public void ActivateAll()
    {
        if (actions != null && actions.Length > 0)
        {
            foreach (IActivatable action in actions)
            {
                action.Activate();
            }
        }
    }

    public void Activate(int num)
    {
        if(actions != null && actions.Length > num)
        {
            ((IActivatable)actions[num]).Activate();
        }
    }

    public void ActivateEvents()
    {
        events.Invoke();
    }
}