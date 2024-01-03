using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LayerData
{
    public Transform transform;
    public Vector2 speed;
}

[Serializable]
public class ParallaxController : MonoBehaviour
{
    // Create a ParallaxController which handles the parallax effect for a list of layers. It shouold be using a reference point and the camera's position to calculate the parallax effect.
    // The layers should be a list of LayerData, which contains a transform and a speed.
    // The transform is the transform of the layer.
    // The speed is the speed of the layer.

    [field: SerializeField] public Transform ReferencePoint { get; private set; } = null;
    [field: SerializeField] public List<LayerData> Layers { get; private set; } = new List<LayerData>();

    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        Vector2 dir = cam.position - ReferencePoint.position;

        foreach (LayerData layer in Layers)
        {
            layer.transform.localPosition = dir * layer.speed;
        }
    }
}








// private float _startingPos, //This is the starting position of the sprites.
//         _lengthOfSprite; //This is the length of the sprites.
// public float AmountOfParallax; //This is amount of parallax scroll. 
//                                // private Camera MainCamera; //Reference of the camera.


// private void Start()
// {
//     //Getting the starting X position of sprite.
//     _startingPos = transform.position.x;
//     //Getting the length of the sprites.
//     _lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;

//     // MainCamera = Camera.main;
// }



// private void FixedUpdate()
// {
//     Vector3 Position = Camera.main.transform.position;
//     float Temp = Position.x * (1 - AmountOfParallax);
//     float Distance = Position.x * AmountOfParallax;

//     Vector3 NewPosition = new Vector3(_startingPos + Distance, transform.position.y, transform.position.z);

//     transform.position = NewPosition;

//     if (Temp > _startingPos + (_lengthOfSprite / 2))
//     {
//         _startingPos += _lengthOfSprite;
//     }
//     else if (Temp < _startingPos - (_lengthOfSprite / 2))
//     {
//         _startingPos -= _lengthOfSprite;
//     }
// }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// [Serializable]
// public class LayerData
// {
//     public Transform transform;
//     public float length;
//     public Vector2 speed;
//     [HideInInspector] public float startingPos;
// }

// public class ParallaxController : MonoBehaviour
// {
//     [field: SerializeField] public List<LayerData> Layer { get; private set; } = new List<LayerData>();

//     private void Start()
//     {
//         foreach (LayerData layer in Layer)
//         {
//             layer.startingPos = layer.transform.position.x;
//         }
//     }



//     private void Update()
//     {
//         foreach (LayerData layer in Layer)
//         {
//             Vector3 Position = Camera.main.transform.position;
//             float Temp = Position.x * (1 - layer.speed.x);
//             float Distance = Position.x * layer.speed.x;

//             Vector3 NewPosition = new Vector3(layer.startingPos + Distance, layer.transform.position.y, layer.transform.position.z);

//             transform.position = NewPosition;

//             if (Temp > layer.startingPos + (layer.length / 2))
//             {
//                 layer.startingPos += layer.length;
//             }
//             else if (Temp < layer.startingPos - (layer.length / 2))
//             {
//                 layer.startingPos -= layer.length;
//             }
//         }
//     }
// }










// [field: SerializeField] public Transform Target { get; private set; } = null;
// [field: SerializeField] public Transform Reference { get; private set; } = null;
// [field: SerializeField] public float Speed { get; private set; } = 10f;
// [field: SerializeField] public List<LayerData> Layers { get; private set; } = new List<LayerData>();

// // private Vector2 lastPosition;

// private void Start()
// {
//     // if (!Target) Target = Camera.main.transform;
//     Target = Camera.main.transform;
//     // Target = Game.Manager.Niamh.transform;
//     if (!Reference) Reference = transform;
// }

// // private void Start()
// // {
// //     lastPosition = Target.position;
// // }

// private void FixedUpdate()
// {
//     Vector2 dir = Target.position - Reference.position;

//     foreach (LayerData layer in Layers)
//     {
//         // Vector2 newPos = new Vector2(
//         //     dir.x * layer.speed.x,
//         //     dir.y * layer.speed.y
//         // );

//         // layer.transform.localPosition = Vector2.Lerp(layer.transform.localPosition, newPos, Time.deltaTime);

//         layer.transform.localPosition = dir * layer.speed;
//     }

//     // lastPosition = Target.position;
// }