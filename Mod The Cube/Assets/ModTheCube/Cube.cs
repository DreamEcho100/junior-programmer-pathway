using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer Renderer;
    [SerializeField]
    private float rotationSpeed = -50.0f, opacity = 1.0f;

    private Color[] colors = { new Color(0.5f, 1.0f, 0.3f, 0.4f) };

    void Start()
    {
        Renderer = GetComponent<MeshRenderer>();
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;

        Material material = Renderer.material;
        material.color = colors[Random.Range(0, colors.Length)];//new Color(0.5f, 1.0f, 0.3f, 0.4f);

        StartCoroutine(Translator());
    }

    void FixedUpdate()
    {
        transform.Rotate(
            new Vector3(1, 1, 1) * rotationSpeed * Time.deltaTime
        );
        //transform.Rotate(10.0f * Time.deltaTime, 0.0f, 0.0f);

        transform.localScale += new Vector3(
            Random.Range(-0.1f, 0.1f),
            Random.Range(-0.1f, 0.1f),
            Random.Range(-0.1f, 0.1f)
        );

        Renderer.material.color = new Color(
            Mathf.Sin(Time.time),
            Mathf.Cos(Time.time),
            Mathf.Sin(Time.time * 0.7f)
        );
    }

    IEnumerator Translator()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.6f));
            transform.position += new Vector3(
                Random.Range(-0.1f, 0.1f) * Time.deltaTime,
                Random.Range(-0.1f, 0.1f) * Time.deltaTime,
                Random.Range(-0.1f, 0.1f) * Time.deltaTime
            );
            //transform.localRotation = Quaternion.Euler(
            //    new Vector3(
            //        Random.Range(-180, 180),
            //        Random.Range(-180, 180),
            //        Random.Range(-180, 180)
            //    )
            //);
        }
    }
}
