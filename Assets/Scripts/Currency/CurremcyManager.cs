/// <summary>
/// �Q�[�����ʉ݂����L���邽�߁A�O���[�o���ȃC���X�^���X���`
/// </summary>
public class CurrencyManager
{
    public static ICurrency GoldInstance = new Gold();
    public static ICurrency MileInstance = new Mile();
}