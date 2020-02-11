using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPrequel : MonoBehaviour {

    MyStopWatch myStopWatch = new MyStopWatch();
    const int secondsInPrequelScene = 42;

    void Start()
    {
        Rigidbody compRb = GetComponent<Rigidbody>();
        compRb.AddForce(0, 0.25f, 0.25f, ForceMode.Impulse);

        MainMain.enableVisitorCameraIfProd();
    }

    void FixedUpdate() {
        if (myStopWatch.execeedesSeconds(secondsInPrequelScene)) {
            myStopWatch.stop();
            SceneManager.LoadScene("playground", LoadSceneMode.Single);
        }
        
    }

}
