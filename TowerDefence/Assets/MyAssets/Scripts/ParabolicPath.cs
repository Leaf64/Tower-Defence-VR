using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicPath : MonoBehaviour {

    float angleDeg = 0;
    float angleRad = 0;
    List<Vector3> elementsOfParabola = new List<Vector3>();
    public LayerMask goodLayer;
    public LayerMask badLayer;
    public LineRenderer lineRend;
    public float speed=10f;
    public float delta=0.1f;
    public int maxLoop = 40;
    public GameObject particles;
    GameObject instantiatedParticles;
    public Color goodColor;
    public Color badColor;
    public Transform parabolSource;
    public GameObject teleportedObject;
    bool isEnd = false;//sprawdza czy parabola natrafiła na jakiś obiekt i kończy rysowanie paraboli
    bool isBad = false;//sprawdza czy natrafiła na przeszkodę
    public static bool isTeleporting = false;
    // Use this for initialization
    void Start () {
		
	}

    float WhatsAngle()
    {
        angleDeg = Vector3.Angle(new Vector3(parabolSource.forward.x, 0, parabolSource.forward.z), parabolSource.forward);
        if (parabolSource.forward.y < 0)
        {
            angleDeg = -angleDeg;
        }
        angleRad = (angleDeg / 180) * Mathf.PI;
        return angleRad;
    }
    void DrawParabole(float deltaTime, float startSpeed)
    {
        if (Input.GetKey(KeyCode.Escape)|| Input.GetKey(KeyCode.W))
        {
            isTeleporting = true;
            isEnd = false;//sprawdza czy parabola natrafiła na jakiś obiekt i kończy rysowanie paraboli
            isBad = false;//sprawdza czy natrafiła na przeszkodę
            elementsOfParabola.Clear();//Czyszczenie wektorów paraboli
            lineRend.positionCount = 0;//Usuwanie renderu paraboli
            float T = 0;//resetowanie czasu
            
            int loopCounter = 0;//liczy pętle by przerwać w razie gdyby było ich zbyt dużo
            while (isEnd == false && loopCounter < maxLoop)//pętla która generuje listę wektorów paraboli
            {
                loopCounter++;
                T += deltaTime;
                Vector3 currentPos = WhatsPosition(T, startSpeed);//Względna pozycja aktualnie dodawanego punktu do paraboli
                Vector3 previousPos = WhatsPosition(T - deltaTime, startSpeed);//Względna pozycja poprzednio dodanego punktu do paraboli
                Vector3 currentRealPos = lineRend.transform.position + lineRend.transform.rotation * currentPos;//Bezwzględna pozycja aktualnie dodawanego punktu do paraboli
                Vector3 previousRealPos = lineRend.transform.position + lineRend.transform.rotation * previousPos;//Bezwzględna pozycja poprzednio dodanego punktu do paraboli
                elementsOfParabola.Add(currentPos);
                RaycastHit hit;
                //Debug.DrawRay(previousRealPos, (currentRealPos - previousRealPos), Color.green, 1);
                //Sprawdzenie czy parabola natrafia na teren do teleportacji
                if (Physics.Raycast(previousRealPos, (currentRealPos - previousRealPos), out hit, Vector3.Distance(currentRealPos, previousRealPos), goodLayer))
                {
                    //Rysowanie miejsca teleportacji
                    if (instantiatedParticles == null)
                    {
                        instantiatedParticles = (GameObject)Instantiate(particles, hit.point + hit.normal * 0.05f, Quaternion.LookRotation(hit.normal), transform.root.parent);
                    }
                    else
                    {
                        instantiatedParticles.transform.position = hit.point + hit.normal * 0.05f;
                        instantiatedParticles.transform.rotation = Quaternion.LookRotation(hit.normal);
                    }
                    isEnd = true;
                }
                //Sprawdzanie czy parabola natrafiła na przeszkodę
                if (Physics.Raycast(previousRealPos, (currentRealPos - previousRealPos), out hit, 1, badLayer))
                {
                    isEnd = true;
                    isBad = true;
                }
            }
            //Jeśli parabola natrafiła na przeszkodę lub nie natrafiła na nic
            if (isBad || isEnd == false)
            {
                lineRend.material.color = badColor;
                Destroy(instantiatedParticles);
            }
            //Jeśli parabola natrafiła na obiekt teleportacji
            else
            {
                lineRend.material.color = goodColor;
            }
            //Miejsce zerowe paraboli
            lineRend.positionCount = elementsOfParabola.Count + 1;
            lineRend.SetPosition(0, Vector3.zero);
            //Rysowanie paraboli
            for (int i = 0; i < elementsOfParabola.Count; i++)
            {
                lineRend.SetPosition(i + 1, elementsOfParabola[i]);
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape)|| Input.GetKeyUp(KeyCode.W))
        {
            //Jeśli parabola jest zdatna do teleportacji to przy przyciśnieńciu LPM teleportuj
            if (isEnd && isBad == false)
            {
                isTeleporting = false;
                teleportedObject.transform.position = instantiatedParticles.transform.position + Vector3.up * 2f;
                elementsOfParabola.Clear();//Czyszczenie wektorów paraboli
                lineRend.positionCount = 0;
                Destroy(instantiatedParticles);

            }
            //Jeśli nie jest zdolna do teleportacji usuń teleport
            else
            {
                elementsOfParabola.Clear();//Czyszczenie wektorów paraboli
                lineRend.positionCount = 0;
                Destroy(instantiatedParticles);
            }
        }
        if (!(isEnd && isBad == false))
        {
            Destroy(instantiatedParticles);

        }
    }
    
	Vector3 WhatsPosition(float T,float startSpeed)//Obliczanie pozycji punktu na paraboli
    {
        Vector3 x = new Vector3(0,0,1) * startSpeed * T * Mathf.Cos(WhatsAngle());
        Vector3 y = new Vector3(0,1,0) * ((startSpeed * T * Mathf.Sin(WhatsAngle())) - (4.9f*T*T));
        Vector3 position = x + y;
        return position;
    }
    public void TrackingLost()
    {
        elementsOfParabola.Clear();//Czyszczenie wektorów paraboli
        lineRend.positionCount = 0;
        Destroy(instantiatedParticles);
    }
	// Update is called once per frame
	void Update () {
        DrawParabole(delta, speed);


	}
}
