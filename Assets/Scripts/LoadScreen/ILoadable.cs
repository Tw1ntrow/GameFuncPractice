using System.Collections;

/// <summary>
/// ���[�h���K�v�ȃN���X�Ɏ�������
/// ���[�h�̐i�s���Ԃ̊Ǘ���LoadTask���s���̂Ŏ��ۂ̃��[�h�����݂̂��L�ڂ��A���[�h�i�s�󋵂�Ԃ��z��
/// </summary>
public interface ILoadable
{
    // ���[�h�̊J�n
    IEnumerator Load();

}