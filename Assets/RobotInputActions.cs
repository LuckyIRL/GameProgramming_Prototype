//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.9.0
//     from Assets/RobotInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;

public partial class @RobotInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @RobotInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""RobotInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""2dbc9f8f-cd6e-4124-baa6-da63cfda542a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8bfe2a91-439e-4c3e-8524-6ff0281ae319"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c23b8d94-6222-4028-bf07-99b593ec3288"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aiming"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8423e3d4-164b-4382-8bd6-7eede817b389"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprinting"",
                    ""type"": ""PassThrough"",
                    ""id"": ""04488fe6-be72-4f22-b88f-eae548214e22"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reel-In"",
                    ""type"": ""Button"",
                    ""id"": ""e35196e4-f69c-462b-86f5-c7ace8b332e6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reel-out"",
                    ""type"": ""Button"",
                    ""id"": ""c76507c3-412b-4e00-a335-8c438fcf5097"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""82274dba-5904-4b8e-8d8f-3be4a2ab1c15"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""8e86c6f1-bf99-4d0f-96ec-5169919dcbd8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""be6a342b-e273-4e1c-9477-7dd835aee83b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7d41a1fe-9dfe-4b74-8801-b216a1ac43a2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fb78cc29-1fa3-410b-8beb-a4eee9dbbc6f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""85bcf23c-0f69-47c5-9184-1cc61553aff3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7e3594b3-110c-4bc5-b651-4983dbad9e12"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de5c1011-3451-4998-b1a4-2930753c989a"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a472b870-76e3-4bea-b7e0-61dca5fa8c80"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8625a33-cc51-4890-abce-d25c08212148"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c09aab5-c67a-471c-85eb-b4091e32a59a"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprinting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""369b236d-9e21-4ba7-8af2-5ac06ce820e0"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprinting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c766ee57-3fff-47d0-b704-200aa2d065ed"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reel-In"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c34c1402-198a-4817-9ee2-0c33ab7bba8e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reel-out"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI_Interactions"",
            ""id"": ""43050a89-cc6e-4d05-814c-52878459476a"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""76416c17-5d22-43ae-b0fb-27d98cce54eb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Value"",
                    ""id"": ""90105fe0-9858-4358-ac07-fc1d954ec127"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""8abc8a38-a058-4721-9a09-f395b4d81b52"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""Value"",
                    ""id"": ""01bdb1fd-9200-433f-9665-b9f0784ce508"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""f5b9ffd1-9989-4aaf-a78e-63ab68971c8f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""Value"",
                    ""id"": ""3796119a-6286-4840-b730-111aa7a06939"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""Button"",
                    ""id"": ""59b429fc-ff17-45e5-bccb-f38c002dbd39"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""43cdfa72-76bc-4994-bc21-cc71a5b9ed45"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a5412f09-be89-4651-91db-81435e3942a7"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8d5d648-52f0-4147-b08f-8974de19a276"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1f584ca-ce93-42b5-91a2-33c6838d3f8d"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43714551-ef29-4b89-a135-9cddfe86bb85"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24aca4a0-943e-4550-99e1-65dbf034a25d"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e15659c-6aaf-4d8f-9ec1-a31a16b6b5a5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e60ed5d-8eb2-4d18-b5a5-fd67d834bebd"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f4da4457-50f2-4fa9-9a8c-941748cf9d7f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d87e677b-37a0-4f04-8ce3-72ab037647e0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8314feac-9946-4b5e-8627-56e4626cf018"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""87824791-cf59-427c-a6fa-8cee11115e80"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3396fe4f-d37a-4fdf-8ca0-83e3cfb931a0"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Aiming = m_Player.FindAction("Aiming", throwIfNotFound: true);
        m_Player_Sprinting = m_Player.FindAction("Sprinting", throwIfNotFound: true);
        m_Player_ReelIn = m_Player.FindAction("Reel-In", throwIfNotFound: true);
        m_Player_Reelout = m_Player.FindAction("Reel-out", throwIfNotFound: true);
        // UI_Interactions
        m_UI_Interactions = asset.FindActionMap("UI_Interactions", throwIfNotFound: true);
        m_UI_Interactions_Navigate = m_UI_Interactions.FindAction("Navigate", throwIfNotFound: true);
        m_UI_Interactions_Submit = m_UI_Interactions.FindAction("Submit", throwIfNotFound: true);
        m_UI_Interactions_Cancel = m_UI_Interactions.FindAction("Cancel", throwIfNotFound: true);
        m_UI_Interactions_Point = m_UI_Interactions.FindAction("Point", throwIfNotFound: true);
        m_UI_Interactions_Click = m_UI_Interactions.FindAction("Click", throwIfNotFound: true);
        m_UI_Interactions_ScrollWheel = m_UI_Interactions.FindAction("ScrollWheel", throwIfNotFound: true);
        m_UI_Interactions_MiddleClick = m_UI_Interactions.FindAction("MiddleClick", throwIfNotFound: true);
        m_UI_Interactions_RightClick = m_UI_Interactions.FindAction("RightClick", throwIfNotFound: true);
    }

    ~@RobotInputActions()
    {
        Debug.Assert(!m_Player.enabled, "This will cause a leak and performance issues, RobotInputActions.Player.Disable() has not been called.");
        Debug.Assert(!m_UI_Interactions.enabled, "This will cause a leak and performance issues, RobotInputActions.UI_Interactions.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Aiming;
    private readonly InputAction m_Player_Sprinting;
    private readonly InputAction m_Player_ReelIn;
    private readonly InputAction m_Player_Reelout;
    public struct PlayerActions
    {
        private @RobotInputActions m_Wrapper;
        public PlayerActions(@RobotInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Aiming => m_Wrapper.m_Player_Aiming;
        public InputAction @Sprinting => m_Wrapper.m_Player_Sprinting;
        public InputAction @ReelIn => m_Wrapper.m_Player_ReelIn;
        public InputAction @Reelout => m_Wrapper.m_Player_Reelout;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Aiming.started += instance.OnAiming;
            @Aiming.performed += instance.OnAiming;
            @Aiming.canceled += instance.OnAiming;
            @Sprinting.started += instance.OnSprinting;
            @Sprinting.performed += instance.OnSprinting;
            @Sprinting.canceled += instance.OnSprinting;
            @ReelIn.started += instance.OnReelIn;
            @ReelIn.performed += instance.OnReelIn;
            @ReelIn.canceled += instance.OnReelIn;
            @Reelout.started += instance.OnReelout;
            @Reelout.performed += instance.OnReelout;
            @Reelout.canceled += instance.OnReelout;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Aiming.started -= instance.OnAiming;
            @Aiming.performed -= instance.OnAiming;
            @Aiming.canceled -= instance.OnAiming;
            @Sprinting.started -= instance.OnSprinting;
            @Sprinting.performed -= instance.OnSprinting;
            @Sprinting.canceled -= instance.OnSprinting;
            @ReelIn.started -= instance.OnReelIn;
            @ReelIn.performed -= instance.OnReelIn;
            @ReelIn.canceled -= instance.OnReelIn;
            @Reelout.started -= instance.OnReelout;
            @Reelout.performed -= instance.OnReelout;
            @Reelout.canceled -= instance.OnReelout;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI_Interactions
    private readonly InputActionMap m_UI_Interactions;
    private List<IUI_InteractionsActions> m_UI_InteractionsActionsCallbackInterfaces = new List<IUI_InteractionsActions>();
    private readonly InputAction m_UI_Interactions_Navigate;
    private readonly InputAction m_UI_Interactions_Submit;
    private readonly InputAction m_UI_Interactions_Cancel;
    private readonly InputAction m_UI_Interactions_Point;
    private readonly InputAction m_UI_Interactions_Click;
    private readonly InputAction m_UI_Interactions_ScrollWheel;
    private readonly InputAction m_UI_Interactions_MiddleClick;
    private readonly InputAction m_UI_Interactions_RightClick;
    public struct UI_InteractionsActions
    {
        private @RobotInputActions m_Wrapper;
        public UI_InteractionsActions(@RobotInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_UI_Interactions_Navigate;
        public InputAction @Submit => m_Wrapper.m_UI_Interactions_Submit;
        public InputAction @Cancel => m_Wrapper.m_UI_Interactions_Cancel;
        public InputAction @Point => m_Wrapper.m_UI_Interactions_Point;
        public InputAction @Click => m_Wrapper.m_UI_Interactions_Click;
        public InputAction @ScrollWheel => m_Wrapper.m_UI_Interactions_ScrollWheel;
        public InputAction @MiddleClick => m_Wrapper.m_UI_Interactions_MiddleClick;
        public InputAction @RightClick => m_Wrapper.m_UI_Interactions_RightClick;
        public InputActionMap Get() { return m_Wrapper.m_UI_Interactions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UI_InteractionsActions set) { return set.Get(); }
        public void AddCallbacks(IUI_InteractionsActions instance)
        {
            if (instance == null || m_Wrapper.m_UI_InteractionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UI_InteractionsActionsCallbackInterfaces.Add(instance);
            @Navigate.started += instance.OnNavigate;
            @Navigate.performed += instance.OnNavigate;
            @Navigate.canceled += instance.OnNavigate;
            @Submit.started += instance.OnSubmit;
            @Submit.performed += instance.OnSubmit;
            @Submit.canceled += instance.OnSubmit;
            @Cancel.started += instance.OnCancel;
            @Cancel.performed += instance.OnCancel;
            @Cancel.canceled += instance.OnCancel;
            @Point.started += instance.OnPoint;
            @Point.performed += instance.OnPoint;
            @Point.canceled += instance.OnPoint;
            @Click.started += instance.OnClick;
            @Click.performed += instance.OnClick;
            @Click.canceled += instance.OnClick;
            @ScrollWheel.started += instance.OnScrollWheel;
            @ScrollWheel.performed += instance.OnScrollWheel;
            @ScrollWheel.canceled += instance.OnScrollWheel;
            @MiddleClick.started += instance.OnMiddleClick;
            @MiddleClick.performed += instance.OnMiddleClick;
            @MiddleClick.canceled += instance.OnMiddleClick;
            @RightClick.started += instance.OnRightClick;
            @RightClick.performed += instance.OnRightClick;
            @RightClick.canceled += instance.OnRightClick;
        }

        private void UnregisterCallbacks(IUI_InteractionsActions instance)
        {
            @Navigate.started -= instance.OnNavigate;
            @Navigate.performed -= instance.OnNavigate;
            @Navigate.canceled -= instance.OnNavigate;
            @Submit.started -= instance.OnSubmit;
            @Submit.performed -= instance.OnSubmit;
            @Submit.canceled -= instance.OnSubmit;
            @Cancel.started -= instance.OnCancel;
            @Cancel.performed -= instance.OnCancel;
            @Cancel.canceled -= instance.OnCancel;
            @Point.started -= instance.OnPoint;
            @Point.performed -= instance.OnPoint;
            @Point.canceled -= instance.OnPoint;
            @Click.started -= instance.OnClick;
            @Click.performed -= instance.OnClick;
            @Click.canceled -= instance.OnClick;
            @ScrollWheel.started -= instance.OnScrollWheel;
            @ScrollWheel.performed -= instance.OnScrollWheel;
            @ScrollWheel.canceled -= instance.OnScrollWheel;
            @MiddleClick.started -= instance.OnMiddleClick;
            @MiddleClick.performed -= instance.OnMiddleClick;
            @MiddleClick.canceled -= instance.OnMiddleClick;
            @RightClick.started -= instance.OnRightClick;
            @RightClick.performed -= instance.OnRightClick;
            @RightClick.canceled -= instance.OnRightClick;
        }

        public void RemoveCallbacks(IUI_InteractionsActions instance)
        {
            if (m_Wrapper.m_UI_InteractionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUI_InteractionsActions instance)
        {
            foreach (var item in m_Wrapper.m_UI_InteractionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UI_InteractionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UI_InteractionsActions @UI_Interactions => new UI_InteractionsActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnAiming(InputAction.CallbackContext context);
        void OnSprinting(InputAction.CallbackContext context);
        void OnReelIn(InputAction.CallbackContext context);
        void OnReelout(InputAction.CallbackContext context);
    }
    public interface IUI_InteractionsActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnMiddleClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
    }
}
