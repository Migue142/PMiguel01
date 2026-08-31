using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEscena : MonoBehaviour
{
    public void CargaDeEscena(string que_escena) {
        SceneManager.LoadScene(que_escena);
    }
}

