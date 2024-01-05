using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : MonoBehaviour
{
    public float amplitude = 1f;
    public float frequency = 1f;
    public float moveSpeed = 2f;
    public float lineWidth = 0.1f;

    private LineRenderer lineRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Khởi tạo lineRenderer và vẽ đường cong sin
        InitializeLineRenderer();
        DrawSinWave();
    }

    void InitializeLineRenderer()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 0; // Bắt đầu với 0 điểm để không vẽ ngay từ đầu
    }

    void DrawSinWave()
    {
        int resolution = 100;
        lineRenderer.positionCount = resolution;

        for (int i = 0; i < resolution; i++)
        {
            float t = i / (float)(resolution - 1);
            float x = t * 10;
            float y = amplitude * Mathf.Sin(frequency * x);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
        }
    }

    void Update()
    {
        MoveAlongSinWave();
    }

    void MoveAlongSinWave()
    {
        Vector3 currentPos = transform.position;

        // Lấy tọa độ của nhân vật trên đường cong sin
        float sinX = currentPos.x + moveSpeed * Time.deltaTime;

        // Lấy giá trị y tương ứng với đường cong sin
        float sinY = amplitude * Mathf.Sin(frequency * sinX);

        // Di chuyển nhân vật theo đường cong sin
        rb.MovePosition(new Vector2(sinX, sinY));
    }
}
