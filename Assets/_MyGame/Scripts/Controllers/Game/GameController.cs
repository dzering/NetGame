using Object = UnityEngine.Object;

public sealed class GameController : BaseController
{
    private PhotonRoomManager _photonRoomManager;

    private Spawner _spawner;

    private void Init(Context context)
    {
        var photonGameManagerGameObject = Object.Instantiate(context.PhotonGameManager);
        AddGameObject(photonGameManagerGameObject);
        _photonRoomManager = photonGameManagerGameObject.GetComponent<PhotonRoomManager>();
    }

    public GameController(Context context)
    {
       Init(context);
    }
}