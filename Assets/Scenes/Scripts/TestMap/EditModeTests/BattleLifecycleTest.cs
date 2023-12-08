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
        // ���������������s���邩���e�X�g
        _isInitialized = _gameStateController.Initialize(new TestUnitCreator(), new TestMapCreatable()) == 0;
        Assert.IsTrue(_isInitialized, "Failed to initialize GameStateController.");
    }

    [Test]
    public void TestUnInitialization()
    {
        // �I���������������s���邩���e�X�g
        _isUninitialized = _gameStateController.UnInitialize() == 0;
        Assert.IsTrue(_isUninitialized, "Failed to uninitialize GameStateController.");
    }

    [TearDown]
    public void TearDown()
    {
        // �e�e�X�g�I����Ƀ��\�[�X�̃N���[���A�b�v
        _gameStateController = null;
    }
}