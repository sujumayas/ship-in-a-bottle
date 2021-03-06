using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public class MonoInstruction {
    public string methodName;
    public object parameter;

    public MonoInstruction (string _methodName, object _parameter) {
        methodName = _methodName;
        parameter = _parameter;
    }
}

public class BehaviourHundlor : MonoBehaviour {

    public delegate void MonoAction ();
    public MonoAction monoAction;
    static public BehaviourHundlor instance;
	static public readonly List<string> SFDAData = new List<string>();
    List<MonoInstruction> actionQueue = new List<MonoInstruction> ();
    public float maxTimer;
    public float tempTimer;

    public object lastReference;

    void Awake () {
        instance = this;
        //-----------------//
        //maxTimer = 5.0f;
        tempTimer = maxTimer;
    }

    // Use this for initialization
    void Start () {
		//--------- TK00 (Antes de arreglar la torre)
		SFDAData.Add("Aquí torre de control… \n" +
			"Estableciendo primer contacto… \n" +
			"¿Sujeto 18 me copia?");
		SFDAData.Add("Quiero que sepa que dejarlo varado \n" +
			"no ha sido un acto intencional y que \n" +
			"sus bisnietos desean conocerlo con ansias. ");
		SFDAData.Add("Trabajaremos con mucho esfuerzo \n" +
			"para que su extracción sea exitosa: \n" +
			"Los sistemas de comunicación están fallando, \n" +
			"confirme nuestras órdenes a través de la antena.");
		SFDAData.Add("Las pulsaciones de luz son la forma más certera de \n" +
			"cruzar la barrera temporal que nos separa…");

		//--------- TK01 (Arreglaste  la torre)
		SFDAData.Add("La señal lumínica llega fuerte y clara, \n" +
			"¡Excelente trabajo!\n" +
			"Has restablecido el pulsar de la antena…");
		SFDAData.Add("No hay porque alterarse S18, te sacaremos de ahí \n" +
			"en menos de lo que implosiona una supernova… \n" +
			"Ehhhm, mala analogía… ");
		SFDAData.Add("¿Qué venía después? \n" +
			"¿Dónde dejaron el protocolo de extracción? \n" +
			"¡Maldito personal de limpieza!");
		SFDAData.Add("Lamentamos el percance Ese, \n" +
            "¿podemos llamarte Ese?… \n" +
			"Se menciona un vehículo cerca a tu locación…");
		SFDAData.Add("¡Cuidado con esos ánimos! \n" +
			"Las lecturas del sonar indican que \n" +
			"el vehículo debería estar cerca a una masa de agua… \n" +
            "Encuéntralo y vuelve a enviar una señal lumínica…");
		SFDAData.Add("Y aprovecha el paisaje: \n" +
			"¡La supernova se debe ver hermosa desde ahí!");

		//--------- TK0x (Encuentras la nave y envías señal lumínica)
		SFDAData.Add("¿Sujeto 18?, Aquí Torre de Control \n" +
			"estableciendo contacto nuevamente… \n" +
			"Vemos que ha sido capaz de \n" + 
            "ubicar el vehículo…");
        SFDAData.Add ("Sin prisa, pero sin pausa se llega seguro. \n" +
            "Al parecer el estado del vehículo\n" + 
            "es un tanto deficiente:");
        SFDAData.Add ("Primero habrá que sacarlo de esa ciénaga y luego ver… \n" +
            "si es posible repararlo en esas condiciones \n" +
            "con los materiales que se tenga al alcance…");
        SFDAData.Add ("Lo que está claro es que hará falta será un poco de \n" +
            "gas Borbicoide y algunos componentes como… \n" +
			"[Sonido de estática - comunicación ininteligible]");
        SFDAData.Add ("Así de fácil. \n" +
            "Póngase en contacto apenas \n" +
            "logre reunir esos materiales… \n");
        SFDAData.Add ("Si la supernova avanza, \n" +
			"la extracción fallará, le ruego ¡Apúrese! \n" +
            "…no quiero perder mi empleo, \n" +
            "ni a otro sujeto, ya sería el tercero este mes…");

		//--------- TK1x (Exploras y reúnes los materiales / Sacas la nave del agua  y haces el puzzle Borbicoide) (Una vez resuelto, das tap de nuevo a la antena)
		SFDAData.Add(" ¡Éxito! Has logrado condensar el gas Borbicoide\n" +
            "y reunir los componentes… ");
		SFDAData.Add("No hay tiempo que perder Ese, \n" +
			"es hora de sacar el vehículo a flote y\n" +
            "pilotarlo hacia el agujero negro más cercano…  \n"+
			"[Sonido de estática]");
		SFDAData.Add("Por favor tómese un tiempo para responder a \n" +
			"esta encuesta sobre el servicio de\n" +
            "extracción que acaba de recibir… \n" +
			"del 1 al 10… [Sonido de estática]");
		SFDAData.Add("Fin de la transmisión…");
    }
	// Update is called once per frame
	void Update () {
        if (monoAction != null) {
            monoAction ();
        } else if (actionQueue.Count > 0) {
            AddToMono (actionQueue[0].methodName, ref actionQueue[0].parameter);
            actionQueue.RemoveAt (0);
        }
    }

    public void AddToMono (string _methodName, ref object _runtimeObject) {
        Type actionsType = typeof (BehaviourHundlor);
        MethodInfo actionFound = actionsType.GetMethod (_methodName);
        Debug.Log ("HERE");
        if (actionFound != null && monoAction == null) {
            Debug.Log ("THEN HERE");
            monoAction = (MonoAction) Delegate.CreateDelegate (typeof (MonoAction), _runtimeObject, actionFound);
        }
    }

    public void EnqueueAction (MonoInstruction action) {
        actionQueue.Add (action);
    }

    static public void WalkToObjectNode (object _objReference) {
        if (GameControl.instance.mainCharacter.GetComponent<Animator>().runtimeAnimatorController != 
            GameControl.instance.mainCharacter.GetComponent<CharacterAnimations> ().moving) {
            GameControl.instance.mainCharacter.GetComponent<Animator> ().runtimeAnimatorController =
            GameControl.instance.mainCharacter.GetComponent<CharacterAnimations> ().moving;
        }
        if (((_objReference as Transform).position.x - GameControl.instance.mainCharacter.transform.position.x < 0) &&
            !GameControl.instance.mainCharacter.GetComponent<SpriteRenderer> ().flipX) {
            GameControl.instance.mainCharacter.GetComponent<SpriteRenderer> ().flipX = true;
        } else if (((_objReference as Transform).position.x - GameControl.instance.mainCharacter.transform.position.x > 0) &&
            GameControl.instance.mainCharacter.GetComponent<SpriteRenderer> ().flipX) {
            GameControl.instance.mainCharacter.GetComponent<SpriteRenderer> ().flipX = false;
        }
        GameControl.instance.mainCharacter.position = Vector3.MoveTowards (GameControl.instance.mainCharacter.position, (_objReference as Transform).position,
            GameControl.instance.mainCharMovePace * Time.deltaTime);
        if (GameControl.instance.mainCharacter.position == (_objReference as Transform).position) {
            GameControl.instance.mainCharacter.GetComponent<Animator> ().runtimeAnimatorController =
            GameControl.instance.mainCharacter.GetComponent<CharacterAnimations> ().idle;
            Debug.Log ("I made it!");
            instance.lastReference = _objReference;
            instance.monoAction = null;
            AdvStoryMonoger.instance.Check ((_objReference as Transform).parent.GetComponent<ClickableEntity> ().currentID);
            
        }
    }

    static public void PlayWorkAnimation (object _objReference) {
        if ((_objReference as Animator).runtimeAnimatorController != (_objReference as Animator).GetComponent<CharacterAnimations> ().working) {
            (_objReference as Animator).runtimeAnimatorController = (_objReference as Animator).GetComponent<CharacterAnimations> ().working;
        }
        if (!AnimatorIsPlaying (_objReference as Animator)) {
            (_objReference as Animator).runtimeAnimatorController = (_objReference as Animator).GetComponent<CharacterAnimations> ().idle;
            instance.monoAction = null;
        }
    }

    static public void PlayOneShotAnimation (object _objReference) {
        if ((_objReference as Animator).runtimeAnimatorController != (_objReference as Animator).GetComponent<AnimationHolder> ().defaultAnimation) {
            (_objReference as Animator).runtimeAnimatorController = (_objReference as Animator).GetComponent<AnimationHolder> ().defaultAnimation;
        }
        if (!AnimatorIsPlaying(_objReference as Animator)) {
            instance.monoAction = null;
        }
    }

    static public void SetLoopAnimation (object _objReference) {
        if ((_objReference as Animator).runtimeAnimatorController != (_objReference as Animator).GetComponent<AnimationHolder> ().defaultAnimation) {
            (_objReference as Animator).runtimeAnimatorController = (_objReference as Animator).GetComponent<AnimationHolder> ().defaultAnimation;
        }
        instance.monoAction = null;
    }

    static public void DisableObject (object _objReference) {
        (_objReference as GameObject).SetActive (false);
        instance.monoAction = null;
    }

    static public void SwapHoverText (object _objReference) {
        string temp = (_objReference as ClickableEntity).hoverText;
        (_objReference as ClickableEntity).hoverText = (_objReference as ClickableEntity).alternateHover;
        (_objReference as ClickableEntity).alternateHover = temp;
        instance.monoAction = null;
    }

    static public void TranslateObjectUp (object _objReference) {
        (_objReference as Transform).Translate (0, 25, 0);
        instance.monoAction = null;
    }

    static public void ResetTask (object _objReference) {
        Debug.LogWarning ((string) _objReference);
        Debug.LogWarning (AdvStoryMonoger.instance.activePuzzles.Count);
        foreach (Puzzle puzzle in AdvStoryMonoger.instance.activePuzzles) {
            foreach (Task task in puzzle.tasks) {
                if (task.target == (string)_objReference) {
                    Debug.LogWarning ("Setting " + task.target + "'s done to false");
                    task.done = false;
                }
            }
        }
    }
    static public void ClearActionQueue (object _objReference) {
        while (instance.actionQueue.Count != 0) {
            instance.actionQueue.RemoveAt (0);
        }
    }

    public void WindmillScriptCheck (object _objReference) {
        (instance.lastReference as Transform).parent.parent.GetComponent<WindmillCheck> ().Sum ((instance.lastReference as Transform).parent.name);
        instance.monoAction = null;
    }
    public void ButtonScriptCheck (object _objReference) {
        (_objReference as Transform).GetComponent<ButtonCheck> ().Test ((instance.lastReference as Transform).name);
        instance.monoAction = null;
    }

    static public bool AnimatorIsPlaying (Animator animator) {
        return animator.GetCurrentAnimatorStateInfo (0).normalizedTime <= 1;
    }
	static public void SetNextTCCComsText(object _objReference){
		
        if ((int)_objReference != GameControl.instance.currentCommsIndex) {
            GameControl.instance.currentCommsIndex++;
            GameControl.instance.tCommsText.SetText (SFDAData[(int) _objReference]);
        }
        instance.tempTimer -= Time.deltaTime;
        if (instance.tempTimer <= 0) {
            GameControl.instance.tCommsText.SetText ("");
            instance.tempTimer = instance.maxTimer;
            instance.monoAction = null;
        }
	}
}
