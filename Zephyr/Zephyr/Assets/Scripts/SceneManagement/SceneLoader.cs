using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the scene loading and unloading.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameSceneSO _gameplayScene = default;
    [SerializeField] private GameSceneSO _sessionScene = default;
    [SerializeField] private InputReader _inputReader = default;

    [Header("Listening to")]
    [SerializeField] private LoadEventChannelSO _loadLocation = default;
    [SerializeField] private LoadEventChannelSO _loadMenu = default;
    [SerializeField] private LoadEventChannelSO _loadHome = default;
    [SerializeField] private LoadEventChannelSO _coldStartupLocation = default;

    [Header("Broadcasting on")]
    [SerializeField] private BoolEventChannelSO _toggleLoadingScreen = default;
    [SerializeField] private VoidEventChannelSO _onSceneReady = default; //picked up by the SpawnSystem
    [SerializeField] private VoidEventChannelSO _onSessionStart = default; //picked up by SaveSystem
    [SerializeField] private VoidEventChannelSO _onEnterTutorial = default;
    [SerializeField] private VoidEventChannelSO _onEnterHome = default;
    [SerializeField] private VoidEventChannelSO _onEnterLocation = default;
    [SerializeField] private FadeChannelSO _fadeRequestChannel = default;

    private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;
    private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;
    private AsyncOperationHandle<SceneInstance> _sessionManagerLoadingOpHandle;

    //Parameters coming from scene loading requests
    private GameSceneSO _sceneToLoad;
    private GameSceneSO _currentlyLoadedScene;
    private bool _showLoadingScreen;

    private SceneInstance _gameplayManagerSceneInstance = new SceneInstance();
    private SceneInstance _sessionManagerSceneInstance = new SceneInstance();
    private float _fadeOutDuration = .5f;
    private float _fadeInDuration = .5f;
    private bool _isLoading = false; //To prevent a new loading request while already loading a new scene

    private void OnEnable()
    {
        _loadLocation.OnLoadingRequested += LoadLocation;
        _loadMenu.OnLoadingRequested += LoadMenu;
        _loadHome.OnLoadingRequested += LoadHome;
#if UNITY_EDITOR
        _coldStartupLocation.OnLoadingRequested += LocationColdStartup;
#endif
    }

    private void OnDisable()
    {
        _loadLocation.OnLoadingRequested -= LoadLocation;
        _loadMenu.OnLoadingRequested -= LoadMenu;
        _loadHome.OnLoadingRequested -= LoadHome;
#if UNITY_EDITOR
        _coldStartupLocation.OnLoadingRequested -= LocationColdStartup;
#endif
    }

#if UNITY_EDITOR
    /// <summary>
    /// This special loading function is only used in the editor, when the developer presses Play in a Location scene, without passing by Initialisation.
    /// </summary>
    private void LocationColdStartup(GameSceneSO currentlyOpenedLocation, bool showLoadingScreen, bool fadeScreen)
    {
        _currentlyLoadedScene = currentlyOpenedLocation;

        _fadeRequestChannel.FadeIn(_fadeInDuration);

        if (_currentlyLoadedScene.sceneType == GameSceneSO.GameSceneType.Location)
        {
            //Gameplay managers is loaded synchronously
            _gameplayManagerLoadingOpHandle = _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            _gameplayManagerLoadingOpHandle.WaitForCompletion();
            _gameplayManagerSceneInstance = _gameplayManagerLoadingOpHandle.Result;

            _sessionManagerLoadingOpHandle = _sessionScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            _sessionManagerLoadingOpHandle.WaitForCompletion();
            _sessionManagerSceneInstance = _sessionManagerLoadingOpHandle.Result;

            StartGameplay();
        }
        else if (_currentlyLoadedScene.sceneType == GameSceneSO.GameSceneType.Home)
        {
            //Gameplay managers is loaded synchronously
            _gameplayManagerLoadingOpHandle = _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            _gameplayManagerLoadingOpHandle.WaitForCompletion();
            _gameplayManagerSceneInstance = _gameplayManagerLoadingOpHandle.Result;

            StartGameplay();
        }
    }
#endif

    /// <summary>
    /// This function loads the home scenes passed as array parameter
    /// </summary>
    private void LoadHome(GameSceneSO locationToLoad, bool showLoadingScreen, bool fadeScreen)
    {
        Debug.Log("Homecoming");

        //Prevent a double-loading, for situations where the player falls in two Exit colliders in one frame
        if (_isLoading)
            return;

        _sceneToLoad = locationToLoad;
        _showLoadingScreen = showLoadingScreen;
        _isLoading = true;

        if (_sessionManagerSceneInstance.Scene != null
            && _sessionManagerSceneInstance.Scene.isLoaded)
            Addressables.UnloadSceneAsync(_sessionManagerLoadingOpHandle, true);

        //In case we are coming from the main menu, we need to load the Gameplay manager scene first
        if (_gameplayManagerSceneInstance.Scene == null
            || !_gameplayManagerSceneInstance.Scene.isLoaded)
        {
            _gameplayManagerLoadingOpHandle = _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            _gameplayManagerLoadingOpHandle.Completed += OnGameplayManagersLoaded;
        }
        else
        {
            StartCoroutine(UnloadPreviousScene());
        }
    }

    /// <summary>
    /// This function loads the location scenes passed as array parameter
    /// </summary>
    private void LoadLocation(GameSceneSO locationToLoad, bool showLoadingScreen, bool fadeScreen)
    {
        //Prevent a double-loading, for situations where the player falls in two Exit colliders in one frame
        if (_isLoading)
            return;

        _sceneToLoad = locationToLoad;
        _showLoadingScreen = showLoadingScreen;
        _isLoading = true;

        //In case we are coming from the main menu, we need to load the Gameplay manager scene first
        if (_gameplayManagerSceneInstance.Scene == null
            || !_gameplayManagerSceneInstance.Scene.isLoaded)
        {
            _gameplayManagerLoadingOpHandle = _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            _gameplayManagerLoadingOpHandle.Completed += LoadSession;
        }
        else if (_sessionManagerSceneInstance.Scene == null
            || !_sessionManagerSceneInstance.Scene.isLoaded)
        {
            _sessionManagerLoadingOpHandle = _sessionScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            _sessionManagerLoadingOpHandle.Completed += OnSessionManagersLoaded;
        }
        else
        {
            StartCoroutine(UnloadPreviousScene());
        }
    }

    private void LoadSession(AsyncOperationHandle<SceneInstance> obj)
    {
        _gameplayManagerSceneInstance = _gameplayManagerLoadingOpHandle.Result;

        _sessionManagerLoadingOpHandle = _sessionScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        _sessionManagerLoadingOpHandle.Completed += OnSessionManagersLoaded;
    }

    private void OnSessionManagersLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        _sessionManagerSceneInstance = _sessionManagerLoadingOpHandle.Result;

        StartCoroutine(UnloadPreviousScene());
    }

    private void OnGameplayManagersLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        _gameplayManagerSceneInstance = _gameplayManagerLoadingOpHandle.Result;

        StartCoroutine(UnloadPreviousScene());
    }

    /// <summary>
    /// Prepares to load the main menu scene, first removing the Gameplay scene in case the game is coming back from gameplay to menus.
    /// </summary>
    private void LoadMenu(GameSceneSO menuToLoad, bool showLoadingScreen, bool fadeScreen)
    {
        //Prevent a double-loading, for situations where the player falls in two Exit colliders in one frame
        if (_isLoading)
            return;

        _sceneToLoad = menuToLoad;
        _showLoadingScreen = showLoadingScreen;
        _isLoading = true;

        //In case we are coming from a Location back to the main menu, we need to get rid of the persistent Gameplay manager scene
        if (_gameplayManagerSceneInstance.Scene != null
            && _gameplayManagerSceneInstance.Scene.isLoaded)
            Addressables.UnloadSceneAsync(_gameplayManagerLoadingOpHandle, true);

        if (_sessionManagerSceneInstance.Scene != null
            && _sessionManagerSceneInstance.Scene.isLoaded)
            Addressables.UnloadSceneAsync(_sessionManagerLoadingOpHandle, true);

        StartCoroutine(UnloadPreviousScene());
    }

    /// <summary>
    /// In both Location and Menu loading, this function takes care of removing previously loaded scenes.
    /// </summary>
    private IEnumerator UnloadPreviousScene()
    {
        _inputReader.DisableAllInput();
        _fadeRequestChannel.FadeOut(_fadeOutDuration);

        yield return new WaitForSeconds(_fadeOutDuration);

        if (_currentlyLoadedScene != null) //would be null if the game was started in Initialisation
        {
            if (_currentlyLoadedScene.sceneReference.OperationHandle.IsValid())
            {
                //Unload the scene through its AssetReference, i.e. through the Addressable system
                _currentlyLoadedScene.sceneReference.UnLoadScene();
            }
#if UNITY_EDITOR
            else
            {
                //Only used when, after a "cold start", the player moves to a new scene
                //Since the AsyncOperationHandle has not been used (the scene was already open in the editor),
                //the scene needs to be unloaded using regular SceneManager instead of as an Addressable
                Debug.Log(_currentlyLoadedScene);
                SceneManager.UnloadSceneAsync(_currentlyLoadedScene.sceneReference.editorAsset.name);
            }
#endif
        }

        LoadNewScene();
    }

    /// <summary>
    /// Kicks off the asynchronous loading of a scene, either menu or Location.
    /// </summary>
    private void LoadNewScene()
    {
        if (_showLoadingScreen)
        {
            _toggleLoadingScreen.RaiseEvent(true);
        }

        _loadingOperationHandle = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
        _loadingOperationHandle.Completed += OnNewSceneLoaded;
    }

    private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        //Save loaded scenes (to be unloaded at next load request)
        _currentlyLoadedScene = _sceneToLoad;

        Scene s = obj.Result.Scene;
        SceneManager.SetActiveScene(s);
        LightProbes.TetrahedralizeAsync();

        _isLoading = false;

        if (_showLoadingScreen)
            _toggleLoadingScreen.RaiseEvent(false);

        _fadeRequestChannel.FadeIn(_fadeInDuration);

        StartGameplay();
    }

    private void StartGameplay()
    {
        _onSceneReady.RaiseEvent(); //Spawn system will spawn the Player in a gameplay scene

        switch (_currentlyLoadedScene.sceneType)
        {
            case GameSceneSO.GameSceneType.Home:
                _onEnterHome.RaiseEvent();
                goto case GameSceneSO.GameSceneType.Tutorial;
            case GameSceneSO.GameSceneType.Tutorial:
                _onEnterTutorial.RaiseEvent();
                break;
            case GameSceneSO.GameSceneType.Location:
                _onEnterLocation.RaiseEvent();
                break;
            default:
                break;

        }
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit!");
    }
}
