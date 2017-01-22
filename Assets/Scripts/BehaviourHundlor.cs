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
    float maxTimer;
    float tempTimer;

    void Awake () {
        instance = this;
        //-----------------//
        maxTimer = 2.5f;
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
		SFDAData.Add("Trabajaremos con mucho esfuerzo para que su extracción sea exitosa: \n" +
			"Los sistemas de comunicación trabajan aún en una mínima capacidad, \n" +
			"es mejor si confirma nuestras órdenes a través de la antena… ");
		SFDAData.Add("Las pulsaciones de luz son la forma más certera de \n" +
			"cruzar la barrera temporal que nos separa…");

		//--------- TK01 (Arreglaste  la torre)
		SFDAData.Add("La señal lumínica llega fuerte y clara, \n" +
			"excelente trabajo restableciendo el pulsar de la antena…");
		SFDAData.Add("No hay porque alterarse S18, te sacaremos de ahí \n" +
			"en menos de lo que implosiona una supernova… Ehhhm, mala analogía… ");
		SFDAData.Add("¿Qué venía después? ¿Dónde dejaron el protocolo de extracción? \n" +
			"¡Maldito personal de limpieza!");
		SFDAData.Add("Lamentamos el percance Ese, ¿podemos llamarte Ese?... \n" +
			"Al parecer esta sección del viejo protocolo extracción de SFA está incompleto… \n" +
			"Por aquí se menciona un vehículo cerca a tu locación…");
		SFDAData.Add("¡Cuidado con esos ánimos! \n" +
			"Las lecturas del sonar indican que el vehículo debería estar cerca a una masa de agua… \n" +
			"¿Tienes idea de donde podría estar, Ese?... \n" +
			"Al encontrarlo vuelve a enviar una señal lumínica con la antena… \n" +
			"Es probable que la siguiente vez que recibas un mensaje como este, no sea yo quien lo envíe… \n" +
			"Y aprovecha el paisaje: ¡La supernova se debe ver hermosa desde donde estás!");

		//--------- TK02 (Click en la antena)
		SFDAData.Add("La señal lumínica llega fuerte y clara, \n" +
			"excelente trabajo restableciendo \n" +
			"el pulsar de la antena…");
		SFDAData.Add("Quiero que sepa que dejarlo varado no ha sido un acto intencional \n" +
			"y que sus bisnietos desean conocerlo con ansias. \n" +
			"Trabajaremos con mucho esfuerzo para que su extracción sea exitosa: \n" +
			"Los sistemas de comunicación trabajan aún en una mínima capacidad...");
		SFDAData.Add("...es mejor si confirma nuestras órdenes a través de la antena… \n" +
			"Las pulsaciones de luz son la forma más certera \n" +
			"de cruzar la barrera temporal que nos separa…");
		SFDAData.Add("...Otro texto??");

		//--------- TK0x (Encuentras la nave y envías señal lumínica)
		SFDAData.Add("¿Sujeto 18?, aquí Torre de Control estableciendo contacto nuevamente… \n" +
			"Vemos que ha sido capaz de ubicar el vehículo…");
		SFDAData.Add("Sin prisa, pero sin pausa se llega seguro. \n" +
			"Al parecer el estado del vehículo es un tanto deficiente: \n" +
			"Primero habrá que sacarlo de esa ciénaga y luego ver... \n" +
			"si es posible repararlo en esas condiciones con los materiales que se tenga al alcance… \n" +
			"Lo que está claro es que hará falta gas Borbicoide y algunos componentes como... \n" +
			"[Sonido de estática - comunicación ininteligible]");
		SFDAData.Add("Así de fácil. \n" +
			"Póngase en contacto apenas logre reunir esos materiales… \n" +
			"Si la supernova avanza, la extracción fallará, le ruego ¡Apúrese! \n" +
			"...no quiero perder mi empleo, ni a otro sujeto, ya sería el tercero este mes...");

		//--------- TK0x (Exploras y reúnes los materiales / Sacas la nave del agua  y haces el puzzle Borbicoide) (Una vez resuelto, das tap de nuevo a la antena)
		SFDAData.Add(" ¡Éxito! Has logrado condensar el gas Borbicoide y reunir los componentes… ");
		SFDAData.Add("No hay tiempo que perder Ese, \n" +
			"es hora de sacar el vehículo a flote y pilotarlo hacia el agujero negro más cercano…  \n" +
			"[Sonido de estática]");
		SFDAData.Add("Por favor tómese un tiempo para responder a \n" +
			"esta encuesta sobre el servicio de extracción que acaba de recibir… \n" +
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

    static public void ResetTask (object _objReference) {
        foreach (Puzzle puzzle in AdvStoryMonoger.instance.activePuzzles) {
            foreach (Task task in puzzle.tasks) {
                if (task.target == (string)_objReference) {
                    task.done = false;
                }
            }
        }
    }

    static public void WindmillScriptCheck (object _objReference) {
        (_objReference as Transform).parent.GetComponent<WindmillCheck> ().Sum ((_objReference as Transform).name);
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
