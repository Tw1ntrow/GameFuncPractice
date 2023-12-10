using System.Collections;

/// <summary>
/// ロードが必要なクラスに実装する
/// ロードの進行や状態の管理はLoadTaskが行うので実際のロード処理のみを記載し、ロード進行状況を返す想定
/// </summary>
public interface ILoadable
{
    // ロードの開始
    IEnumerator Load();

}