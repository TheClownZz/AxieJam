using System.Text;
using Unity.Profiling;
using UnityEngine;

public class RenderStatsScript : MonoBehaviour
{
    [Header("Config")]
    public bool setPassCall;
    public bool drawCall;
    public bool vertices;
    public bool triangles;
    public bool memory;
    public bool GC;
    public bool mainThread;

    float deltaTime = 0.0f;
    string statsText;
    ProfilerRecorder setPassCallsRecorder;
    ProfilerRecorder drawCallsRecorder;
    ProfilerRecorder verticesRecorder;
    ProfilerRecorder trianglesRecorder;
    ProfilerRecorder systemMemoryRecorder;
    ProfilerRecorder gcMemoryRecorder;
    ProfilerRecorder mainThreadTimeRecorder;

    void OnEnable()
    {
        if (setPassCall)
        {
            setPassCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
        }
        if (drawCall)
        {
            drawCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
        }
        if (vertices)
        {
            verticesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
        }
        if (triangles)
        {
            trianglesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
        }
        if (memory)
        {
            systemMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
        }
        if (GC)
        {
            gcMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        }
        if (mainThread)
        {
            mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "Main Thread", 15);
        }
    }

    void OnDisable()
    {
        if (setPassCall)
        {
            setPassCallsRecorder.Dispose();
        }
        if (drawCall)
        {
            drawCallsRecorder.Dispose();
        }
        if (vertices)
        {
            verticesRecorder.Dispose();
        }
        if (triangles)
        {
            trianglesRecorder.Dispose();
        }
        if (memory)
        {
            systemMemoryRecorder.Dispose();
        }
        if (GC)
        {
            gcMemoryRecorder.Dispose();
        }
        if (mainThread)
        {
            mainThreadTimeRecorder.Dispose();
        }
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        var sb = new StringBuilder(500);

        sb.AppendLine($"Frame Time: {(deltaTime * 1000.0f):0.0} ms");
        sb.AppendLine($"FPS: {(1.0f / deltaTime):0.}");

        if (setPassCall && setPassCallsRecorder.Valid)
            sb.AppendLine($"SetPass Calls: {setPassCallsRecorder.LastValue}");
        if (drawCall && drawCallsRecorder.Valid)
            sb.AppendLine($"Draw Calls: {drawCallsRecorder.LastValue}");
        if (vertices && verticesRecorder.Valid)
            sb.AppendLine($"Vertices: {verticesRecorder.LastValue:n0} K");
        if (triangles && trianglesRecorder.Valid)
            sb.AppendLine($"Triangles: {trianglesRecorder.LastValue:n0} K");
        if (memory && systemMemoryRecorder.Valid)
            sb.AppendLine($"System Memory: {systemMemoryRecorder.LastValue / (1024 * 1024)} MB");
        if (GC && gcMemoryRecorder.Valid)
            sb.AppendLine($"GC Memory: {gcMemoryRecorder.LastValue / (1024 * 1024)} MB");
        // if (mainThreadTimeRecorder.Valid)
        //     sb.AppendLine($"Frame Time: {GetRecorderFrameAverage(mainThreadTimeRecorder) * (1e-6f):F1} ms");
        statsText = sb.ToString();
    }

    // static double GetRecorderFrameAverage(ProfilerRecorder recorder)
    // {
    //     var samplesCount = recorder.Capacity;
    //     if (samplesCount == 0)
    //         return 0;

    //     double r = 0;
    //     unsafe
    //     {
    //         var samples = stackalloc ProfilerRecorderSample[samplesCount];
    //         recorder.CopyTo(samples, samplesCount);
    //         for (var i = 0; i < samplesCount; ++i)
    //             r += samples[i].Value;
    //         r /= samplesCount;
    //     }

    //     return r;
    // }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 120;
        style.normal.textColor = Color.red;
        // style.normal.textColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);

        GUI.Label(new Rect(10, 10, 300, 360), statsText, style);
    }
}
