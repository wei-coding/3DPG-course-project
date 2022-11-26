using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateMusicbox : MonoBehaviour
{
    private Animator[] temp;
    private Animator handleAnimator;
    private Animator catAnimator;
    private bool checkPlaying = false;
    public AudioClip nextmp3;
    
    private GameObject ghost;
    private GameObject cat;
    private Vector3 endPos;
    private GameObject musicBoxCam;

    public GameObject MainCam;
    public GameObject MusicboxCam;

    // Start is called before the first frame update
    void Start()
    {
        temp = GameObject.Find("Handle").GetComponentsInChildren<Animator>();
        handleAnimator = temp[0];
        temp = GameObject.Find("cat").GetComponentsInChildren<Animator>();
        catAnimator = temp[0];

        ghost = GameObject.Find("ghost");
        ghost.SetActive(false);
        cat = GameObject.Find("MusicBox_Model/cat");

        endPos = new Vector3(1.5f, 4.7f, -0.311f);
        musicBoxCam = GameObject.Find("Musicbox Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "MusicBox_Model")
                {
                    bool start = handleAnimator.GetBool("musicboxStart");
                    if (!start)
                    {
                        Debug.Log("Play");
                        MusicboxCam.SetActive(true);
                        MainCam.SetActive(false);
                        checkPlaying = true;
                        GetComponent<AudioSource>().Play();
                        handleAnimator.SetTrigger("musicboxStart");
                        catAnimator.SetTrigger("musicboxStart");
                    }
                }
            }
        }
        if (checkPlaying) {
            if (!GetComponent<AudioSource>().isPlaying) {
                Debug.Log("Stop");
                checkPlaying = false;
                //����y�s�n
                GetComponent<AudioSource>().clip = nextmp3;
                GetComponent<AudioSource>().Play();
                //���X��
                ghost.SetActive(true);               
                cat.SetActive(false);
                //musicBoxCam.transform.position = Vector3.Lerp(musicBoxCam.transform.position, endPos, Time.deltaTime * 10);
                musicBoxCam.transform.position = endPos;

                handleAnimator.ResetTrigger("musicboxStart");
                catAnimator.ResetTrigger("musicboxStart");
                handleAnimator.speed = 0;
                catAnimator.speed = 0;

                MainCam.SetActive(true);
                MusicboxCam.SetActive(false);
            }
        }
    }
}