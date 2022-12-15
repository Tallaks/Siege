# GameControls Class

Namespace: Kulinaria.Siege.Runtime.Infrastructure.Inputs


## Properties

| Name | Type                                            | Summary |
|---|-------------------------------------------------|---|
| **asset** | InputActionAsset                                |  |
| **bindingMask** | InputBinding?                                   |  |
| **devices** | ReadOnlyArray\<InputDevice\>?                   |  |
| **controlSchemes** | ReadOnlyArray\<InputControlScheme\>             |  |
| **bindings** | IEnumerable\<InputBinding\>                     |  |
| **CameraActions** | [CameraActionsActions](CameraActionsActions.md) |  |
| **KeyboardMouseScheme** | InputControlScheme                              |  |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **Contains(InputAction action)** | bool |  |
| **Disable()** | void |  |
| **Dispose()** | void |  |
| **Enable()** | void |  |
| **FindAction(string actionNameOrId, bool throwIfNotFound)** | InputAction |  |
| **FindBinding(InputBinding bindingMask, out InputAction action)** | int |  |
| **GetEnumerator()** | IEnumerator\<InputAction\> |  |
