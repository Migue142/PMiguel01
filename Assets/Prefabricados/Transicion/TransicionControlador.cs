using UnityEngine;

public class TransicionControlador : MonoBehaviour
{
    public Animator _animator;

    public void IniciarTransicion(string tipo_transicion)
    {
        if (tipo_transicion == "apertura")
        {
            _animator.SetBool("abrir",true);
        }
        else if(tipo_transicion == "cierre")
        {
            _animator.SetBool("abrir", false);
        }
    }
}
