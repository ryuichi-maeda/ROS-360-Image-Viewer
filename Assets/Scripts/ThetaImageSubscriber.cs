using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;
using RosMessageTypes.Std;

public class ThetaImageSubscriber : MonoBehaviour
{
    public string topicName = "/client1/theta_compressed";

    public Skybox skybox;
    public float displayFrequency = 72.0f; // Up to 90Hz?
    private Texture2D texture2D;
    private byte[] imageData;

    // Start is called before the first frame update
    void Start()
    {
        // ROSConnection.SetPortPref(9090);
        // ROSConnection.EditedPort = 9090;
        // ROSConnection.GetOrCreateInstance().Subscribe<StringMsg>(topicName, RenderThetaImage);
        ROSConnection.GetOrCreateInstance().Subscribe<CompressedImageMsg>(topicName, RenderThetaImage);
        texture2D = new Texture2D(1, 1);
        texture2D.Apply();
        skybox.material = new Material(Shader.Find("Skybox/Panoramic"));
        OVRPlugin.systemDisplayFrequency = displayFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void RenderThetaImage(StringMsg msg)
    private void RenderThetaImage(CompressedImageMsg msg)
    {
        // Debug.Log("Received Theta Image Message");

        texture2D.LoadImage(msg.data);
        // texture2D.filterMode = FilterMode.Point;

        // 下2つは画質を良くするようだが、あんまり効果ない？
        // texture2D.filterMode = FilterMode.Trilinear;
        // texture2D.anisoLevel = 9;
        
        // texture2D.Apply();
        skybox.material.SetTexture("_MainTex", texture2D);
        // skybox.material.mainTexture = texture2D;
        // skybox.material.mainTexture.wrapMode = TextureWrapMode.Clamp;
    }
}
