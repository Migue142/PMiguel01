using UnityEngine;
using Vuforia;

public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    private CloudRecoBehaviour mCloudRecoBehaviour;
    private bool mIsScanning = false;
    private string mTargetMetadata = "";

    // Arrastra aquí tu prefab o GameObject "Cloud Image Target" desde el Inspector
    public ImageTargetBehaviour ImageTargetTemplate;

    // Registrar callbacks de Cloud Recognition al arrancar
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();

        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    // Desregistrar callbacks cuando el script se destruye
    void OnDestroy()
    {
        if (mCloudRecoBehaviour != null)
        {
            mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
            mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
            mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
            mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
            mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
        }
    }

    // Métodos opcionales para monitorear el estado y errores en la consola
    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco inicializado con éxito.");
    }

    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.LogError("Error de inicialización en Cloud Reco: " + initError.ToString());
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.LogError("Error de actualización en Cloud Reco: " + updateError.ToString());
    }

    // Controla si Vuforia está escaneando activamente
    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;
        if (scanning)
        {
            // Aquí puedes limpiar o reiniciar variables si lo necesitas
        }
    }

    // Maneja el evento cuando se encuentra una imagen en la nube
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        // Almacena los metadatos de la imagen detectada
        mTargetMetadata = cloudRecoSearchResult.MetaData;

        // Detiene el escaneo apagando temporalmente el componente
        mCloudRecoBehaviour.enabled = false;
        Debug.Log("Los datos son: " + mTargetMetadata);

        // Si tienes una plantilla asignada, instancia la aumentación 3D sobre el objetivo
        if (ImageTargetTemplate != null)
        {
            mCloudRecoBehaviour.EnableObservers(cloudRecoSearchResult, ImageTargetTemplate.gameObject);
        }
    }

    // Interfaz de usuario básica para ver el estado y reiniciar el escaneo
    void OnGUI()
    {
        // Muestra si está escaneando o no
        GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Escaneando..." : "Escaneo pausado");

        // Muestra los metadatos del último objetivo encontrado
        GUI.Box(new Rect(100, 200, 200, 50), "Metadatos: " + mTargetMetadata);

        // Si el escaneo se detuvo, muestra un botón para reactivarlo manualmente
        if (!mIsScanning)
        {
            if (GUI.Button(new Rect(100, 300, 200, 50), "Reiniciar Escaneo"))
            {
                mCloudRecoBehaviour.enabled = true;
                mTargetMetadata = "";
            }
        }
    }
}

