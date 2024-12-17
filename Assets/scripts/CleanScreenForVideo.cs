using UnityEngine;

public class CleanScreenForVideo : MonoBehaviour
{
    public BallCleaner Cleaner; // Referência pública que pode ser atribuída no Editor

    void Start()
    {
        if (Cleaner == null)
        {
            Cleaner = FindFirstObjectByType<BallCleaner>();
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C) && Cleaner != null)
        {
            Cleaner.PushBallsOutOfScreen();
            Debug.Log("Tecla C acionada");
        }
    }
}
