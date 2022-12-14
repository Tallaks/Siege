//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/Settings/GameControls.inputactions
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

namespace Kulinaria.Siege.Runtime.Infrastructure.Inputs
{
    public partial class @GameControls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""CameraActions"",
            ""id"": ""e6c2b38c-23f7-40a0-a02d-f3391bbb0f0f"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8a0a10be-3c4b-4260-945d-e18e12a3651f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""2c628513-27f7-416f-a4f4-fdbad4041339"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""8ca734ce-dfa5-4f56-b422-88d661491f76"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""f3aed1eb-7cf0-467c-8730-5d85d51b4aa0"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PointPosition"",
                    ""type"": ""Value"",
                    ""id"": ""83ce06e0-c109-440b-b6d4-f587d5de0c70"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""167e1128-7007-43c1-af2f-865cfd08fe8a"",
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
                    ""id"": ""69aabe96-4b88-49eb-83d4-c8b3bfd12cef"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""00d7f582-ccad-402c-81b3-a53e5d89ca1a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2263db38-97ad-4e0a-9426-32b62f1cff6a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ad9cef06-ab90-4396-8724-1307e0f7ed34"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""3b7e91ac-4372-496b-8f62-217d28921782"",
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
                    ""id"": ""dbbf32c4-b19d-4038-8fa0-2ce91706604c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""afbfde64-058d-4cdd-b38b-ea7452d8a0e2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7ed111e2-7832-46f1-bafe-f363ef2b38eb"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ee3d19a4-827d-487f-92b7-8eccd484d6f6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b69aa22c-c1d9-4df9-be30-d1d7fedafa6c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Alt+LeftButtton"",
                    ""id"": ""b2a9646d-d1a6-4257-ae78-dd4c379536c5"",
                    ""path"": ""TwoModifiers"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier1"",
                    ""id"": ""34d8087e-b1bd-4ca0-8938-a4288c7fcb37"",
                    ""path"": ""<Keyboard>/leftAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Modifier2"",
                    ""id"": ""bfa397d5-5057-40ce-8ad7-9f39a7d26878"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Binding"",
                    ""id"": ""2e16c277-235a-4aed-83c4-f641648de68f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fc87f66c-2be4-48c7-8a1e-488cd241a3d2"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c07fb288-1550-4e39-8c1f-9086f7a3cec7"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""PointPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // CameraActions
            m_CameraActions = asset.FindActionMap("CameraActions", throwIfNotFound: true);
            m_CameraActions_Move = m_CameraActions.FindAction("Move", throwIfNotFound: true);
            m_CameraActions_Click = m_CameraActions.FindAction("Click", throwIfNotFound: true);
            m_CameraActions_Rotate = m_CameraActions.FindAction("Rotate", throwIfNotFound: true);
            m_CameraActions_Zoom = m_CameraActions.FindAction("Zoom", throwIfNotFound: true);
            m_CameraActions_PointPosition = m_CameraActions.FindAction("PointPosition", throwIfNotFound: true);
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

        // CameraActions
        private readonly InputActionMap m_CameraActions;
        private ICameraActionsActions m_CameraActionsActionsCallbackInterface;
        private readonly InputAction m_CameraActions_Move;
        private readonly InputAction m_CameraActions_Click;
        private readonly InputAction m_CameraActions_Rotate;
        private readonly InputAction m_CameraActions_Zoom;
        private readonly InputAction m_CameraActions_PointPosition;
        public struct CameraActionsActions
        {
            private @GameControls m_Wrapper;
            public CameraActionsActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_CameraActions_Move;
            public InputAction @Click => m_Wrapper.m_CameraActions_Click;
            public InputAction @Rotate => m_Wrapper.m_CameraActions_Rotate;
            public InputAction @Zoom => m_Wrapper.m_CameraActions_Zoom;
            public InputAction @PointPosition => m_Wrapper.m_CameraActions_PointPosition;
            public InputActionMap Get() { return m_Wrapper.m_CameraActions; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CameraActionsActions set) { return set.Get(); }
            public void SetCallbacks(ICameraActionsActions instance)
            {
                if (m_Wrapper.m_CameraActionsActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnMove;
                    @Click.started -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnClick;
                    @Click.performed -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnClick;
                    @Click.canceled -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnClick;
                    @Rotate.started -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnRotate;
                    @Rotate.performed -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnRotate;
                    @Rotate.canceled -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnRotate;
                    @Zoom.started -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnZoom;
                    @PointPosition.started -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnPointPosition;
                    @PointPosition.performed -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnPointPosition;
                    @PointPosition.canceled -= m_Wrapper.m_CameraActionsActionsCallbackInterface.OnPointPosition;
                }
                m_Wrapper.m_CameraActionsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Click.started += instance.OnClick;
                    @Click.performed += instance.OnClick;
                    @Click.canceled += instance.OnClick;
                    @Rotate.started += instance.OnRotate;
                    @Rotate.performed += instance.OnRotate;
                    @Rotate.canceled += instance.OnRotate;
                    @Zoom.started += instance.OnZoom;
                    @Zoom.performed += instance.OnZoom;
                    @Zoom.canceled += instance.OnZoom;
                    @PointPosition.started += instance.OnPointPosition;
                    @PointPosition.performed += instance.OnPointPosition;
                    @PointPosition.canceled += instance.OnPointPosition;
                }
            }
        }
        public CameraActionsActions @CameraActions => new CameraActionsActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        public interface ICameraActionsActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnClick(InputAction.CallbackContext context);
            void OnRotate(InputAction.CallbackContext context);
            void OnZoom(InputAction.CallbackContext context);
            void OnPointPosition(InputAction.CallbackContext context);
        }
    }
}
