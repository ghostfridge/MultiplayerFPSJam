//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.0
//     from Assets/Settings/InputMain.inputactions
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

public partial class @InputMain: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMain()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMain"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c64af664-14b0-47cb-9c62-33dbce18b040"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""1b4797ea-bf7d-4609-89cb-d39344521de5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""55b44dd1-53eb-44dd-b001-50a85953f199"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Value"",
                    ""id"": ""3d322fe6-c51d-4504-92ab-0bb562344f63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""EquipPrimaryWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""1dbab4c9-55b2-4319-bcf4-bc5716efba9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""EquipSecondWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""989e0ed8-9259-4e04-a28d-1e9e7ea5391d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ToggleCursorCapture"",
                    ""type"": ""Button"",
                    ""id"": ""34227fa3-580b-4a31-a50a-fbe05118b709"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CaptureCursor"",
                    ""type"": ""Button"",
                    ""id"": ""f58775d7-5fe8-4132-a179-b196f557a3ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Value"",
                    ""id"": ""043aac8f-6e2b-4eb9-868a-2a8c6a78dce1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""OpenChat"",
                    ""type"": ""Button"",
                    ""id"": ""286abbad-c482-463e-862c-8f48cc9943f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a00df2c0-179a-4fc6-90b0-f084320576a5"",
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
                    ""id"": ""8dacb746-0401-44e4-948b-4d64d7d12aeb"",
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
                    ""id"": ""54300e7b-27a1-4f27-9315-76a9ea2bc70e"",
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
                    ""id"": ""c1b3fc6b-e761-4bfc-b264-2c65f8e3dd35"",
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
                    ""id"": ""f90c68e0-fb32-40b9-ae75-f969fc65b9b9"",
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
                    ""id"": ""0a0a3e43-0dbf-4bff-8049-c90c64487371"",
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
                    ""id"": ""a46d8c2c-bb88-4d00-9e56-3e5a6b6427ca"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c2b391e-fca8-478e-94ec-90813310e725"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EquipPrimaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8ecf37a-3385-40fa-9ceb-76f625aea1c3"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EquipSecondWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb285fd3-d79c-42b0-b1db-fa291b2dafa0"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCursorCapture"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a1f0fb9-71e5-4059-b7b2-f30025bde013"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CaptureCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""089c98c8-74f6-4eb7-8d2b-6c3f197a21f7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""321ed552-7b07-4874-8330-76565ff48027"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenChat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
        m_Player_Crouch = m_Player.FindAction("Crouch", throwIfNotFound: true);
        m_Player_EquipPrimaryWeapon = m_Player.FindAction("EquipPrimaryWeapon", throwIfNotFound: true);
        m_Player_EquipSecondWeapon = m_Player.FindAction("EquipSecondWeapon", throwIfNotFound: true);
        m_Player_ToggleCursorCapture = m_Player.FindAction("ToggleCursorCapture", throwIfNotFound: true);
        m_Player_CaptureCursor = m_Player.FindAction("CaptureCursor", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_OpenChat = m_Player.FindAction("OpenChat", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Crouch;
    private readonly InputAction m_Player_EquipPrimaryWeapon;
    private readonly InputAction m_Player_EquipSecondWeapon;
    private readonly InputAction m_Player_ToggleCursorCapture;
    private readonly InputAction m_Player_CaptureCursor;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_OpenChat;
    public struct PlayerActions
    {
        private @InputMain m_Wrapper;
        public PlayerActions(@InputMain wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Crouch => m_Wrapper.m_Player_Crouch;
        public InputAction @EquipPrimaryWeapon => m_Wrapper.m_Player_EquipPrimaryWeapon;
        public InputAction @EquipSecondWeapon => m_Wrapper.m_Player_EquipSecondWeapon;
        public InputAction @ToggleCursorCapture => m_Wrapper.m_Player_ToggleCursorCapture;
        public InputAction @CaptureCursor => m_Wrapper.m_Player_CaptureCursor;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @OpenChat => m_Wrapper.m_Player_OpenChat;
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
            @Crouch.started += instance.OnCrouch;
            @Crouch.performed += instance.OnCrouch;
            @Crouch.canceled += instance.OnCrouch;
            @EquipPrimaryWeapon.started += instance.OnEquipPrimaryWeapon;
            @EquipPrimaryWeapon.performed += instance.OnEquipPrimaryWeapon;
            @EquipPrimaryWeapon.canceled += instance.OnEquipPrimaryWeapon;
            @EquipSecondWeapon.started += instance.OnEquipSecondWeapon;
            @EquipSecondWeapon.performed += instance.OnEquipSecondWeapon;
            @EquipSecondWeapon.canceled += instance.OnEquipSecondWeapon;
            @ToggleCursorCapture.started += instance.OnToggleCursorCapture;
            @ToggleCursorCapture.performed += instance.OnToggleCursorCapture;
            @ToggleCursorCapture.canceled += instance.OnToggleCursorCapture;
            @CaptureCursor.started += instance.OnCaptureCursor;
            @CaptureCursor.performed += instance.OnCaptureCursor;
            @CaptureCursor.canceled += instance.OnCaptureCursor;
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @OpenChat.started += instance.OnOpenChat;
            @OpenChat.performed += instance.OnOpenChat;
            @OpenChat.canceled += instance.OnOpenChat;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Crouch.started -= instance.OnCrouch;
            @Crouch.performed -= instance.OnCrouch;
            @Crouch.canceled -= instance.OnCrouch;
            @EquipPrimaryWeapon.started -= instance.OnEquipPrimaryWeapon;
            @EquipPrimaryWeapon.performed -= instance.OnEquipPrimaryWeapon;
            @EquipPrimaryWeapon.canceled -= instance.OnEquipPrimaryWeapon;
            @EquipSecondWeapon.started -= instance.OnEquipSecondWeapon;
            @EquipSecondWeapon.performed -= instance.OnEquipSecondWeapon;
            @EquipSecondWeapon.canceled -= instance.OnEquipSecondWeapon;
            @ToggleCursorCapture.started -= instance.OnToggleCursorCapture;
            @ToggleCursorCapture.performed -= instance.OnToggleCursorCapture;
            @ToggleCursorCapture.canceled -= instance.OnToggleCursorCapture;
            @CaptureCursor.started -= instance.OnCaptureCursor;
            @CaptureCursor.performed -= instance.OnCaptureCursor;
            @CaptureCursor.canceled -= instance.OnCaptureCursor;
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @OpenChat.started -= instance.OnOpenChat;
            @OpenChat.performed -= instance.OnOpenChat;
            @OpenChat.canceled -= instance.OnOpenChat;
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
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnEquipPrimaryWeapon(InputAction.CallbackContext context);
        void OnEquipSecondWeapon(InputAction.CallbackContext context);
        void OnToggleCursorCapture(InputAction.CallbackContext context);
        void OnCaptureCursor(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnOpenChat(InputAction.CallbackContext context);
    }
}
