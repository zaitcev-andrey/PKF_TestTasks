using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public GameObject OpenedInterface;
    public GameObject ClosedInterface;

    public GameObject ListCellPrefab;
    public GameObject CheckboxPrefab;
    public GameObject EyePrefab;

    public Sprite TurnOnCheckbox;
    public Sprite TurnOffCheckbox;
    public Sprite TurnOnEye;
    public Sprite TurnOffEye;

    public List<CellComponents> AllObjectsInInterface;
    private List<GameObject> AllObjectsOnScene;
    

    void Start()
    {
        OpenedInterface.SetActive(true);
        ClosedInterface.SetActive(false);

        // добавл€ем в список интерфейса все объекты с тегом "ForInterface" на сцене
        AllObjectsOnScene = new List<GameObject>(GameObject.FindGameObjectsWithTag("ForInterface"));
        AllObjectsInInterface = new List<CellComponents>();
        for (int i = 0; i < AllObjectsOnScene.Count; i++)
        {
            GameObject go = Instantiate(ListCellPrefab, transform);
            GameObject checkBox = Instantiate(CheckboxPrefab, go.transform);
            GameObject eye = Instantiate(EyePrefab, go.transform);
            
            // об€зательно создаЄм копию, так как метод принимает значение по ссылке. ѕередава€ i, мы всегда
            // бы получали в качестве индекса размер нашего массива, т.е. 3, что было бы ошибкой
            int copy_i = i;
            // нам нужно передавать в обработчик кнопки ещЄ и индекс того элемента, дл€ которого была нажата кнопка
            // дл€ передачи аргумента(индекса) необходимо обернуть метод в ещЄ один метод(через л€мбда выражение)
            checkBox.GetComponent<Button>().onClick.AddListener(() => CellCheckbox.SwitchLocalCheckbox(copy_i));
            eye.GetComponent<Button>().onClick.AddListener(() => CellEye.SwitchLocalEye(copy_i));

            AllObjectsInInterface.Add(new CellComponents(checkBox, eye));
        }       
    }

    void Update()
    {
        for (int i = 0; i < AllObjectsInInterface.Count; i++)
        {
            CellComponents obj = AllObjectsInInterface[i];
            if (obj.IsChange)
            {
                if(obj.IsCheckBoxChange)
                {
                    if (obj.CheckboxPrefab.GetComponent<Image>().sprite == TurnOnCheckbox)
                    {
                        obj.CheckboxPrefab.GetComponent<Image>().sprite = TurnOffCheckbox;
                        obj.IsCheckBoxTurnOn = false;
                    }
                    else
                    {
                        obj.CheckboxPrefab.GetComponent<Image>().sprite = TurnOnCheckbox;
                        obj.IsCheckBoxTurnOn = true;
                    }

                    obj.IsCheckBoxChange = false;
                }
                if(obj.IsEyeChange)
                {
                    if (obj.EyePrefab.GetComponent<Image>().sprite == TurnOnEye)
                    {
                        obj.EyePrefab.GetComponent<Image>().sprite = TurnOffEye;
                        AllObjectsOnScene[i].SetActive(false);
                    }
                    else
                    {
                        obj.EyePrefab.GetComponent<Image>().sprite = TurnOnEye;
                        AllObjectsOnScene[i].SetActive(true);
                    }

                    obj.IsEyeChange = false;
                }

                obj.IsChange = false;
            }

            // здесь мен€ем прозрачность дл€ объектов на сцене
            if(obj.IsCheckBoxTurnOn)
            {
                if(obj.CurrentOpacity != obj.NewOpacity)
                {
                    obj.CurrentOpacity = obj.NewOpacity;

                    MeshRenderer mr = AllObjectsOnScene[i].GetComponent<MeshRenderer>();
                    Color col = mr.material.color;
                    col.a = (float)obj.CurrentOpacity / 100;
                    mr.material.color = col;
                }
            }
        }
    }

    // дл€ удобства создадим класс, хран€щий все внутренние префабы €чейки в списке интерфейса
    public class CellComponents
    {
        public GameObject CheckboxPrefab;
        public GameObject EyePrefab;

        public bool IsCheckBoxChange = false;
        public bool IsEyeChange = false;
        public bool IsChange = false;

        public bool IsCheckBoxTurnOn = false;
        public int CurrentOpacity = 100;
        public int NewOpacity = 100;

        public CellComponents(GameObject checkboxPrefab, GameObject eyePrefab)
        {
            CheckboxPrefab = checkboxPrefab;
            EyePrefab = eyePrefab; 
        }
    }

    public void CloseInterfaceButton()
    {
        OpenedInterface?.SetActive(false);
        ClosedInterface?.SetActive(true);
    }

    public void OpenInterfaceButton()
    {
        OpenedInterface?.SetActive(true);
        ClosedInterface?.SetActive(false);
    }
}
