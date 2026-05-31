using UnityEngine;
using UnityEngine.Serialization;

public class Simulation : MonoBehaviour
{
    public Transform minSimulation;
    public Transform maxSimulation;
    public Transform player;
    [FormerlySerializedAs("material")]
    public Material simulationMaterial;
    public RenderTexture renderTexture;

    public Material instanceMaterial;

    private Vector3 lastPosition;
    private RenderTexture lastFrame;

    private void Awake()
    {
        lastFrame = new RenderTexture(renderTexture.width, renderTexture.height, renderTexture.depth, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        instanceMaterial = new Material(simulationMaterial);
        Shader.SetGlobalVector("_MinPosSimulation", minSimulation.position);
        Shader.SetGlobalVector("_MaxPosSimulation", maxSimulation.position);

        lastPosition = player.position;
    }

    void Update()
    {
        Debug.Log(lastFrame.sRGB);
        var direction = player.position - lastPosition;
        instanceMaterial.SetVector("_Direction", direction.normalized);
        instanceMaterial.SetVector("_PlayerPosition", player.position);
        instanceMaterial.SetTexture("_LastFrame", lastFrame);

        Graphics.Blit(null, renderTexture, instanceMaterial);
        Graphics.Blit(renderTexture, lastFrame);

        lastPosition = player.position;
    }
}
