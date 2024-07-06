using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damamageTextPrefab; //we add "prefab" at the end because we implying that
    public GameObject healthTextPrefab;//we are gonna create instances of these

    public Canvas gameCanvas;

    private void Awake()//Awake happen once
    {
        gameCanvas = FindObjectOfType<Canvas>();//as long as you only have 1 canvas to find that is in the root

    }

    private void OnEnable()//whether this UIManager is made active in the scene/This is a lifecycle method/OnEnable can happen multiple time
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;//these function will run when these events are invoke
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        //create a text at character hit
        //Camera.main -> your active camera, worldtoscreenpoint mean we take the world position of the character and we turn that into a point on the canvas,
        //and then we just put in the position that we take.
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damamageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();//(instantiate a copy of our prefab and we gonna set the text on it)

        tmpText.text = damageReceived.ToString();//turn int to string to assign

    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {

        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif

#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
                Application.Quit();
#elif (UNITY_WEBGL)
                SceneManager.LoadScene("QuitScene");
#endif

        }
    }

}