# Object Pool
Implementation of Expandable Object Pool pattern in Unity. Quick and easy to apply to any Unity project.
I come up to this implementation while developing multiple projects. I decided to share this implementation as I find it efficient and easy to use.

## Hot it works
Object pool will hold configured objects as not active. When object is returned from pool, it will be set as active and put to specified position. When pooled object is disabled it will return itself back to pool. This implementation is efficient if you have multiple conditions when object should be returned to pool (i.e. when object goes out of bounds, object collides enemy, lifetime of the object expired etc.)

## Demo
The solution includes interactive demo of the pattern. Open Assets/Scenes/Demo to try it out.

![alt text](https://github.com/mikhailShpirko/unity-object-pool/blob/main/demo.png)

## Setup
1. Include all scripts from a folder Assets/Scripts/ObjectPool to your project
2. Configure objects that you are going to use for pooling 
   1. Fill PoolObjectType enum with object types relevant to your project
   2. Attach PoolableObject script component to objects/prefabs that you are going to use for pooling and select relevant type
3. Configure object pool on scene/s
   1. Attach Pool script component to any object on your scene
   2. Configure Pool Object Settings of the script component
      1. Set number of poolable object types to be used
      2. Set number of instances to create for each poolable object type
      3. Select Type for each opoolable bject type
      4. Attach prefab or object each poolable object type

## Usage
Use function GetFromPool of Pool component to retrieve object from pool.

```csharp
/// <summary>
/// Retrieves object from pool. Object will set as active on scene and positioned on specified location. Pool will expand if there are no free objects
/// </summary>
/// <param name="type">Pool Object Type</param>
/// <param name="position">Position to be set to the object</param>
/// <typeparam name="T">Component to return</typeparam>
/// <returns>Component of the object from pool</returns>
public T GetFromPool<T>(PoolObjectType type, Vector3 position) where T : MonoBehaviour
```

Example:
```csharp
[SerializeField] //if you want to assign it in editor
private Pool _objectPool; 

//or
private void Awake()
{
  _objectPool = FindObjectOfType<Pool>();
}

...

//get object of type Pool from object pool
//place it at coordinates x = 0f, y = 0f, z = 0f
//cubdeFromPool will reference component PoolableObject of the object returned
var cubeFromPool = _objectPool.GetFromPool<PoolableObject>(PoolObjectType.Cube, Vector3.zero);

```

Disable the object to return it to pool.
```csharp

//if you don't want to call PoolableObject component
gameObject.SetActive(false);

//or if you already saved PoolableObject component to local variable
cubeFromPool.Disable();
...

## QA
The implementation is covered with Unit Tests. Tests are localted at Assets/Tests. Make sure you enabled Test Runner (Windows > General -> Test Runner)
