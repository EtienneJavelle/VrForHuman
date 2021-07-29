using UnityEngine;

public class Runner : MonoBehaviour {

    #region Fields

    private Transform currentTarget;
    private int pathIndex = 0;

    private RunnerManager runnerManager;
    private Rigidbody rb;

    private Vector3 direction;

    private bool isRunning;
    private bool inCardiacArrest;


    #endregion

    #region UnityInspector

    [SerializeField] private Animator anim;


    [SerializeField] private Transform[] paths;
    [SerializeField] private float speed = 1f;

    #endregion

    private void Start() {
        rb = GetComponent<Rigidbody>();
        runnerManager = GetComponentInParent<RunnerManager>();

        currentTarget = paths[0];
        isRunning = true;
        inCardiacArrest = false;


    }

    // Update is called once per frame
    private void Update() {
        direction = currentTarget.position - transform.position;
        direction.Normalize();
        transform.LookAt(new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z));

        rb.velocity = direction * speed * Time.deltaTime;
        anim.SetBool("IsRunning", isRunning);

        if(isRunning) {
            if(Vector3.Distance(transform.position, currentTarget.position) < 0.3f) {
                if(pathIndex < paths.Length - 1) {
                    pathIndex++;
                    currentTarget = paths[pathIndex];
                } else {
                    Debug.Log("End Path");
                    speed = 0;
                    isRunning = false;
                    inCardiacArrest = true;
                    runnerManager.ActiveCardiacMassage(true);
                }
            }
        }
    }
}
