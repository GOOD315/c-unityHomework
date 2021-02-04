using UnityEngine;


public class MyAdvancedGUI : MonoBehaviour
{
    public Color myColor;         // Градиент цвета

    [HideInInspector] public GameObject currentHitObject;  // текущий объект в который попала пуля
    private GameObject lastHitObject;     // последний объект который рассматривался
    private MeshRenderer objColor;      // Ссылка на рендер объекта
    private Transform objTransform;     // ссылка на трансформ объекта
    private Transform staticObjTransform; // статичный трансформ для первого случая, что бы взять максиум и минимум position

    // костыль, поскольку staticObjTransform не хотел быть статичным и обновлялся вместе с обычным трансформом 
    // если будете это читать, напишите пожалуйста как надо было сделать это
    private float xx;
    private float yy;
    private float zz;


    void OnGUI()
    {

        if (currentHitObject != lastHitObject)
        {
            objTransform = currentHitObject.GetComponent<Transform>();
            objColor = currentHitObject.GetComponent<MeshRenderer>();
            // staticObjTransform = currentHitObject.GetComponent<Transform>();
            xx = objTransform.position.x;
            yy = objTransform.position.y;
            zz = objTransform.position.z;


            lastHitObject = currentHitObject;
        }

        if (currentHitObject != null)
        {
            // меняем цвет и трансформ
            objColor.material.color = RGBSlider(new Rect(10, 10, 200, 20), objColor.material.color);
            objTransform = MovementSliders(new Rect(10, 160, 200, 20), objTransform, xx, yy, zz);
            
            // возвращаем их объекту
            currentHitObject.GetComponent<Transform>().position = objTransform.position;
            currentHitObject.GetComponent<MeshRenderer>().material.color = objColor.material.color;
        }

    }

    // Отрисовка пользовательского слайдера
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMinValue, float sliderMaxValue, string labelText) // ДЗ добавить MinValue
    {
        // создаём прямоугольник с координатами в пространстве и заданой шириной и высотой 
        Rect labelRect = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(labelRect, labelText);   // создаём Label на экране

        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height); // Задаём размеры слайдера
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, 0.0f, sliderMaxValue); // Вырисовываем слайдер и считываем его параметр
        return sliderValue; // Возвращаем значение слайдера
    }

    // Отрисовка тройной слайдер группы, каждый слайдер отвечает за свой цвет
    Color RGBSlider(Rect screenRect, Color rgb)
    {
        // Используя пользовательский слайдер, создаём его
        rgb.r = LabelSlider(screenRect, rgb.r, 0.0f, 1.0f, "Red");

        // делаем промежуток
        screenRect.y += 20;
        rgb.g = LabelSlider(screenRect, rgb.g, 0.0f, 1.0f, "Green");

        screenRect.y += 20;
        rgb.b = LabelSlider(screenRect, rgb.b, 0.0f, 1.0f, "Blue");

        screenRect.y += 20;
        rgb.a = LabelSlider(screenRect, rgb.a, 0.0f, 1.0f, "alpha");

        return rgb; // возвращаем цвет
    }

    Transform MovementSliders(Rect screenRect, Transform transf, float xx, float yy, float zz)
    {
        float x = transf.position.x;
        float y = transf.position.y;
        float z = transf.position.z;

        x = MovementSlider2(screenRect, x, xx - 50, xx + 50, "X COORD");

        screenRect.y += 20;
        y = MovementSlider2(screenRect, y, yy - 50, yy + 50, "Y COORD");

        screenRect.y += 20;
        z = MovementSlider2(screenRect, z, zz - 50, zz + 50, "Z COORD");

        transf.position = new Vector3(x, y, z);
        return (transf);
    }

    float MovementSlider2(Rect screenRect, float value, float minValue, float maxValue, string labelText)
    {
        // создаём прямоугольник с координатами в пространстве и заданой шириной и высотой 
        Rect labelRect = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(labelRect, labelText);   // создаём Label на экране

        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height);
        value = GUI.HorizontalSlider(sliderRect, value, minValue, maxValue);
        return value;
    }

}
