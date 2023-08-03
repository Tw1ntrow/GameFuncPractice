using NUnit.Framework;
using UniRx;

public class BattleLifecycleTest
{
    private GameStateController _gameStateController;
    private bool _isInitialized;
    private bool _isUninitialized;

    [SetUp]
    public void SetUp()
    {
        _gameStateController = new GameStateController();
        _isInitialized = false;
        _isUninitialized = false;
    }

    [Test]
    public void TestInitialization()
    {
        // 初期化が正しく行われるかをテスト
        _isInitialized = _gameStateController.Initialize(new TestUnitCreator(), new TestMapCreatable()) == 0;
        Assert.IsTrue(_isInitialized, "Failed to initialize GameStateController.");
    }

    [Test]
    public void TestUnInitialization()
    {
        // 終了処理が正しく行われるかをテスト
        _isUninitialized = _gameStateController.UnInitialize() == 0;
        Assert.IsTrue(_isUninitialized, "Failed to uninitialize GameStateController.");
    }

    [TearDown]
    public void TearDown()
    {
        // 各テスト終了後にリソースのクリーンアップ
        _gameStateController = null;
    }
}