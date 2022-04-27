// GENERATED AUTOMATICALLY FROM 'Assets/Scenes/Debug/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Beat Player Controls"",
            ""id"": ""82dcabff-e86f-43c7-9661-e110517b9627"",
            ""actions"": [
                {
                    ""name"": ""Left Note"",
                    ""type"": ""Button"",
                    ""id"": ""8613aff6-7c89-4f46-9039-b4b71b178e6c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right Note"",
                    ""type"": ""Button"",
                    ""id"": ""2d3f0de3-17b8-4caf-a05a-25ca66dad0f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up Note"",
                    ""type"": ""Button"",
                    ""id"": ""cd3a8e7a-6680-420e-a04a-cafde773e611"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down Note"",
                    ""type"": ""Button"",
                    ""id"": ""e84a95e4-851a-4010-be05-b988935b0d00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5591b8a5-34fa-4651-8a60-75663364c41b"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": ""Press"",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""Controller"",
                    ""action"": ""Right Note"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""110af25d-aa03-4edd-96bc-dd207362c2a9"",
                    ""path"": ""<Keyboard>/slash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Right Note"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6728c36d-7c5c-49da-bcbb-d4befe359de8"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": ""Press"",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""Controller"",
                    ""action"": ""Up Note"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e699e59c-13c8-4596-9d56-3c0ce9833ac6"",
                    ""path"": ""<Keyboard>/comma"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Up Note"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80f61f18-4ace-4477-bd10-6ee7067d8373"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": ""Press"",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""Controller"",
                    ""action"": ""Down Note"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a6ebd13-8aa1-46e2-b338-769c49cb4707"",
                    ""path"": ""<Keyboard>/period"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Down Note"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9730d7a-f818-4c12-bb7c-912f1ec0a074"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": ""Press"",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""Controller"",
                    ""action"": ""Left Note"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9f464d5-a5a1-42c1-bd46-5158d3b1b6d9"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Left Note"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Beat Player Controls
        m_BeatPlayerControls = asset.FindActionMap("Beat Player Controls", throwIfNotFound: true);
        m_BeatPlayerControls_LeftNote = m_BeatPlayerControls.FindAction("Left Note", throwIfNotFound: true);
        m_BeatPlayerControls_RightNote = m_BeatPlayerControls.FindAction("Right Note", throwIfNotFound: true);
        m_BeatPlayerControls_UpNote = m_BeatPlayerControls.FindAction("Up Note", throwIfNotFound: true);
        m_BeatPlayerControls_DownNote = m_BeatPlayerControls.FindAction("Down Note", throwIfNotFound: true);
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

    // Beat Player Controls
    private readonly InputActionMap m_BeatPlayerControls;
    private IBeatPlayerControlsActions m_BeatPlayerControlsActionsCallbackInterface;
    private readonly InputAction m_BeatPlayerControls_LeftNote;
    private readonly InputAction m_BeatPlayerControls_RightNote;
    private readonly InputAction m_BeatPlayerControls_UpNote;
    private readonly InputAction m_BeatPlayerControls_DownNote;
    public struct BeatPlayerControlsActions
    {
        private @Controls m_Wrapper;
        public BeatPlayerControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftNote => m_Wrapper.m_BeatPlayerControls_LeftNote;
        public InputAction @RightNote => m_Wrapper.m_BeatPlayerControls_RightNote;
        public InputAction @UpNote => m_Wrapper.m_BeatPlayerControls_UpNote;
        public InputAction @DownNote => m_Wrapper.m_BeatPlayerControls_DownNote;
        public InputActionMap Get() { return m_Wrapper.m_BeatPlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BeatPlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IBeatPlayerControlsActions instance)
        {
            if (m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface != null)
            {
                @LeftNote.started -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnLeftNote;
                @LeftNote.performed -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnLeftNote;
                @LeftNote.canceled -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnLeftNote;
                @RightNote.started -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnRightNote;
                @RightNote.performed -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnRightNote;
                @RightNote.canceled -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnRightNote;
                @UpNote.started -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnUpNote;
                @UpNote.performed -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnUpNote;
                @UpNote.canceled -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnUpNote;
                @DownNote.started -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnDownNote;
                @DownNote.performed -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnDownNote;
                @DownNote.canceled -= m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface.OnDownNote;
            }
            m_Wrapper.m_BeatPlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftNote.started += instance.OnLeftNote;
                @LeftNote.performed += instance.OnLeftNote;
                @LeftNote.canceled += instance.OnLeftNote;
                @RightNote.started += instance.OnRightNote;
                @RightNote.performed += instance.OnRightNote;
                @RightNote.canceled += instance.OnRightNote;
                @UpNote.started += instance.OnUpNote;
                @UpNote.performed += instance.OnUpNote;
                @UpNote.canceled += instance.OnUpNote;
                @DownNote.started += instance.OnDownNote;
                @DownNote.performed += instance.OnDownNote;
                @DownNote.canceled += instance.OnDownNote;
            }
        }
    }
    public BeatPlayerControlsActions @BeatPlayerControls => new BeatPlayerControlsActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IBeatPlayerControlsActions
    {
        void OnLeftNote(InputAction.CallbackContext context);
        void OnRightNote(InputAction.CallbackContext context);
        void OnUpNote(InputAction.CallbackContext context);
        void OnDownNote(InputAction.CallbackContext context);
    }
}
