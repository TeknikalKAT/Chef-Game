using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SatisfactionBarBehaviour : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Color low;
    [SerializeField] Color high;
    [SerializeField] Vector3 offset;

    public void SetSatisfaction(float health, float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth - health;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);

    }
    void Start()
    {
        StartCoroutine(StartSlider());
    }


    // Update is called once per frame
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);

    }

    IEnumerator StartSlider()
    {
        slider.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        slider.gameObject.SetActive(true);
    }
}
