using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour {
    [SerializeField]
    private List<Button> buttons;
    private int currentNumber;

    private void Start()
    {
        SelectButton(buttons[0]);
    }

    // Update is called once per frame
    void Update () {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) && buttons[currentNumber].interactable) {
            buttons[currentNumber].onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            RemoveButton(buttons[currentNumber]);
            currentNumber = (currentNumber - 1 + buttons.Count) % buttons.Count;
            SelectButton(buttons[currentNumber]);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            RemoveButton(buttons[currentNumber]);
            currentNumber = (currentNumber + 1 + buttons.Count) % buttons.Count;
            SelectButton(buttons[currentNumber]);
        }
    }

    private void SelectButton(Button target) {
        RectTransform rect = target.GetComponent<RectTransform>();
        rect.localScale = rect.localScale * 1.1f;
    }

    private void RemoveButton(Button target) {
        RectTransform rect = target.GetComponent<RectTransform>();
        rect.localScale = rect.localScale / 1.1f;
    }
}
