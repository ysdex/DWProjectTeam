using UnityEngine;

public static class Utils
{
	/// <summary>
    /// ������ �������� ���� �ѷ� ��ġ�� ���ϴ� �޼ҵ�
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="angle"></param>
    public static Vector3 GetPositionFromAngle(float radius, float angle)
    {
        Vector3 position = Vector3.zero;

        angle = DegreeToRadian(angle);

        position.x = Mathf.Cos(angle) * radius;
        position.y = Mathf.Sin(angle) * radius;

        return position;
    }

    /// <summary>
    /// Degree ���� Radian ������ ��ȯ
    /// 1���� "PI/180" radian
    /// angle���� "PI/180 * angle" radian
    /// </summary>
    /// <param name="angle"></param>
    public static float DegreeToRadian(float angle)
    {
        return Mathf.PI * angle / 180;
    }
}

