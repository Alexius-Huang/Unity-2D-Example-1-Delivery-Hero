# Unity Example Project Notes

### Transform
Able to transform the target object which applies the script.

```csharp
// Translation
transform.Translate(1f, 0, 0);

// Rotation
transform.Rotate(0, 0, 0.1f);
```

### Serialize Field
Able to show the UI in Unity in order to control/adjust the value of the field:

```csharp
public class Drive : MonoBehaviour
{
    [SerializeField] float moveSpeed = .1f;

    // ...
}
```

### Input System (Old, Simple Version)
Navigate to Edit > Project Settings, inside there will be a Input Manager panel which shows the available input options.

Be sure that the input is based on string reference:

```csharp
float steerAmount = Input.GetAxis("Horizontal") * steerSpeed;
transform.Rotate(0, 0, steerAmount);

float moveAmount = Input.GetAxis("Vertical") * moveSpeed;
transform.Translate(moveAmount, 0, 0);
```

### Time.deltaTime

By definition, Time.deltaTime is the completion time in seconds since the last frame. This helps us to make the game frame-independent. That is, regardless of the fps, the game will be executed at the same speed.

More on [this post](https://medium.com/star-gazers/understanding-time-deltatime-6528a8c2b5c8).

```csharp
float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
transform.Rotate(0, 0, steerAmount);

float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
transform.Translate(moveAmount, 0, 0);
```

### Collider2D & Rigidbody2D
Depend on shape, we Collider2D have circular, capsule and box collider; it can act as either a **collider** or **trigger**.

> Using **Edit Collider** can scale or rearrange the collision boundary

In order to simulate object collision, an object must also have rigid body behaviour, this is the component Rigidbody2D does: it **provides physics properties** like mass, drag forces, gravitation ... etc.

> Object with Rigidbody2D default will have **Gravity Scale** with value 1, which means default it has gravity turned on. To turn off, simply set its value to 0.

> Object with collider component but no Rigidbody2D will act as static, fixed object; but if Rigidbody2D applied, it could be moved or pushed around

#### Collider as a Trigger

When **Is Trigger** is turned on in Collider2D component, it will be act as a trigger and will no longer block Rigidbody2D objects.

#### Events

```csharp
class Collision : Monobehaviour
{
    // Collider event
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision!");
    }

    // Trigger event
    void OnTriggerEnter2D(Collision2D other)
    {
        Debug.Log("Trigger Enter!");
    }

    void OnTriggerExit2D(Collision2D other)
    {
        Debug.Log("Trigger Enter!");
    }
}
```

### Sprites

Sprites are made of pixels and **resolutions** refer to number of pixels of the image. (High resolution means image with many pixels)

In Unity, the Unity Unit is used in transform and the grid system generally. For new asset, one Unity Unit is default to contain 100 pixels, but depending on sprites size, if the sprite image size is 32 by 32, it would appear one third smaller in a unity grid.

Hence, if we know sprite image is 32 by 32 pixels, we could set 32 pixels per Unity Unit.

### Camera

To adjust the view of the camera, you could adjust the value of the **Size**.

To let camera to follow a specific game object, we could create a reference, get that game object's position and then apply to the camera:

```csharp
class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject target;

    void LateUpdate()
    {
        transform.position = target.transform.position + new Vector3(0, 0, -10f);
    }
}
```

The reason why we add another Vector with negative Z-axis value is because if camera is at Z-axis 0 value, it will not "see" the whole game view.

Instead of using the `Update` phase, we use `LateUpdate` since we need to figure out the target object's updated position first and then we update the camera position in order to prevent jitteryness.

See [Execution Order](https://docs.unity3d.com/Manual/ExecutionOrder.html).

> LateUpdate is called once per frame, after Update has finished. Any calculations that are performed in Update will have completed when LateUpdate begins. A common use for LateUpdate would be a following third-person camera. If you make your character move and turn inside Update, you can perform all camera movement and rotation calculations in LateUpdate. This will ensure that the character has moved completely before the camera tracks its position.

### Tags

Unity allows you to tag anything on game object in order to easily check the object's category.

```csharp
class Delivery : MonoBehaviour
{
    [SerializeField] float pickupPackageDelay = .3f;

    // ...

    void OnTriggerEnter2D(Collision2D other)
    {
        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package picked up!");
            Destroy(other.gameObject, pickupPackageDelay);
        }
    }
}
```

### GetComponent Function
If we want to access the other component in the script from the object, we could use the `GetComponent` function; for instance, we could access the `SpriteRenderer` and apply different color tint of the object:

```csharp
class Delivery : MonoBehaviour
{
    [SerializeField] Color32 noPackageColor = new Color32(255, 255, 255, 255);
    [SerializeField] Color32 hasPackageColor = new Color32(255, 0, 0, 255);
    SpriteRenderer spriteRenderer;

    // ...

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = noPackageColor;
    }

    // ...

    void OnTriggerEnter2D(Collision2D other)
    {
        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package picked up!");
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, pickupPackageDelay);
        }

        // ...
    }
}
```
