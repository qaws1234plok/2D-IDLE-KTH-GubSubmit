using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Category", fileName = "Category")]
public class Category : ScriptableObject, IEquatable<Category>
{
    [SerializeField] private string codeName;
    [SerializeField] private string displayName;

    public string CodeName => codeName;
    public string DisplayName => displayName;

    #region Opearator
    public bool Equals(Category other)
    {
        if (other == null) return false;
        if (ReferenceEquals(other, this)) return true;
        if (GetType() != other.GetType()) return false;

        return codeName == other.CodeName;
    }

    public override int GetHashCode() => (CodeName, DisplayName).GetHashCode();

    public override bool Equals(object other) => Equals(other as Category);

    public static bool operator == (Category lhs, string rhs)
    {
        if (lhs is null) return ReferenceEquals(rhs, null);
        return lhs.CodeName == rhs || lhs.DisplayName == rhs;
    }

    public static bool operator !=(Category lhs, string rhs) => !(lhs == rhs);
    // 이러면 더이상 Category.CodeName == "Kill" 같이 할 필요 없이
    // Category == "Kill" 이런 식으로 작성할 수 있다.
    #endregion
}
