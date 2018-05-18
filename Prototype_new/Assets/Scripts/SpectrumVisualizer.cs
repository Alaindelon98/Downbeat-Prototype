using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumVisualizer : MonoBehaviour {

    private const int SAMPLE_SIZE = 1024;
    public float rmsValue;
    public float dbValue;
    public float pitchValue;

    public float maxVisualScale = 25.0f;
    public float visualModifier = 50.0f;
    public float smoothSpeed = 10.0f;
    public int amountVisual = 10;
    public float keepPercentage = 0.5f;

    public Texture2D tex;
    public Transform spectrumParent;

    public Color spectrumColor;


    private AudioSource source;
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;

    private Transform[] visualList;
    private float[] visualScale;

    void Start ()
    {
        source = GetComponent<AudioSource>();
        samples = new float[SAMPLE_SIZE];
        spectrum = new float[SAMPLE_SIZE];
        sampleRate = AudioSettings.outputSampleRate;

        SpawnLine();
	}
	
    private void SpawnLine()
    {
        visualScale = new float[amountVisual];
        visualList = new Transform[amountVisual];

        for (int i = 0; i < amountVisual; i++)
        {
            //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            GameObject go = new GameObject();
            go.transform.parent = spectrumParent;
            Vector3 goPos = spectrumParent.position;
            goPos.x -= 12f;
            //go.transform.position = goPos;
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>() as SpriteRenderer;
            sr.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 5.0f);

            Color tempColor = sr.color;
            tempColor = spectrumColor;
            //tempColor.a = 0.2f;
           
            sr.color = tempColor;

            sr.sortingLayerName = "BackPlayer";
            sr.sortingOrder = -1;
            visualList[i] = go.transform;
            visualList[i].position = goPos + (Vector3.right * i);
        }
    }
	void Update ()
    {
        AnalyzeSound();
        UpdateVisual();
    }

    void UpdateVisual()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)((SAMPLE_SIZE * keepPercentage)/ amountVisual);

        while(visualIndex < amountVisual)
        {
            int j = 0;
            float sum = 0;
            while(j < averageSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY = sum / averageSize * visualModifier;
            visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;
            if (visualScale[visualIndex] < scaleY)
                visualScale[visualIndex] = scaleY;

            if (visualScale[visualIndex] > maxVisualScale)
                visualScale[visualIndex] = maxVisualScale;

            visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualIndex++;
        }
    }


    private void AnalyzeSound()
    {
        source.GetOutputData(samples, 0);

        //Get RMS
        int i = 0;
        float sum = 0;
        for (; i < SAMPLE_SIZE; i++)
        {
            sum += samples[i] * samples[i];
        }
        rmsValue = Mathf.Sqrt(sum / SAMPLE_SIZE);

        //Get DB value
        dbValue = 20 * Mathf.Log10(rmsValue / 0.1f);

        //Get Sound Spectrum
        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        //Find pitch
        float maxV  = 0;
        int maxN =  0;
        for (i = 0; i < SAMPLE_SIZE; i++)
        { // find max 
            //Debug.Log("Spectrum " + i + ": " + spectrum[i]);
            if (spectrum[i] > maxV && spectrum[i] > 0.02f)
            {
                maxV = spectrum[i];
                maxN = i; // maxN is the index of max
                          
            }
           
        }

        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < SAMPLE_SIZE - 1)
        { // interpolate index using neighbours
            var dL = spectrum[maxN - 1] / spectrum[maxN];
            var dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        pitchValue = freqN * (sampleRate / 2) / SAMPLE_SIZE; // convert index to frequency
    }


}
