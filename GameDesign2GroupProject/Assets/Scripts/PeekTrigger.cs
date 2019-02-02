using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PeekTrigger : MonoBehaviour {

    [SerializeField] List<Platform> redPlatforms = new List<Platform>();
    [SerializeField] List<Platform2> bluePlatforms = new List<Platform2>();

    [SerializeField] float cameraFOV;

    [SerializeField] GameObject cameraPeekLocation;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            foreach (Platform plat in redPlatforms) {
                if (plat.activate == false) {
                    plat.toDeactivate = true;
                } else {
                    plat.toDeactivate = false;
                }
                plat.GetComponent<Renderer>().enabled = true;
            }

            foreach (Platform2 plat in bluePlatforms) {
                if (plat.activate == false) {
                    plat.toDeactivate = true;
                } else {
                    plat.toDeactivate = false;
                }
                plat.GetComponent<Renderer>().enabled = true;
            }

            LevelManager._instance.peeking = true;
            CameraFocusPeek();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            foreach (Platform plat in redPlatforms) {
                if (plat.toDeactivate == true) {
                    plat.GetComponent<Renderer>().enabled = false;
                }
            }

            foreach (Platform2 plat in bluePlatforms) {
                if (plat.toDeactivate == true) {
                    plat.GetComponent<Renderer>().enabled = false;
                }
            }

            LevelManager._instance.peeking = false;
            LevelManager._instance.CameraFocusPlayer();
        }
    }

    void CameraFocusPeek() {
        Camera.main.transform.position = new Vector3(cameraPeekLocation.transform.position.x, cameraPeekLocation.transform.position.y, Camera.main.transform.position.z);

        Camera.main.fieldOfView = cameraFOV;

        CinemachineVirtualCamera vcam = Camera.main.gameObject.GetComponent<CinemachineVirtualCamera>();
        vcam.m_Follow = cameraPeekLocation.transform;
    }

}
