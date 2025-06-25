using UnityEngine;

public class CameraButtonController : MonoBehaviour
{
    public CameraMoverBotones cameraMover;

    public void EmpezarMoverIzquierda()
    {
        cameraMover.moverIzquierda = true;
    }

    public void PararMoverIzquierda()
    {
        cameraMover.moverIzquierda = false;
    }

    public void EmpezarMoverDerecha()
    {
        cameraMover.moverDerecha = true;
    }

    public void PararMoverDerecha()
    {
        cameraMover.moverDerecha = false;
    }
}
