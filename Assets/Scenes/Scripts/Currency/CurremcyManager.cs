/// <summary>
/// ゲーム中通貨を共有するため、グローバルなインスタンスを定義
/// </summary>
public class CurrencyManager
{
    public static ICurrency GoldInstance = new Gold();
    public static ICurrency MileInstance = new Mile();
}