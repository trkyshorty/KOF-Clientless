
using System.ComponentModel;

namespace KOF.Core.Models;

public class Party
{

    /// <summary>
    ///     Gets or sets the IsInParty.
    /// </summary>
    public bool IsInParty() => Members != null && Members.Count > 0;

    /// <summary>
    ///    Then Party Capacity.
    /// </summary>
    public readonly int Capacity = 8;

    /// <summary>
    ///     Gets The IsFull.
    /// </summary>
    public bool IsFull() => Members.Count >= Capacity;

    /// <summary>
    ///     Gets or sets the members.
    /// </summary>
    /// <value>
    /// The members.
    /// </value>
    public List<PartyMember> Members { get; set; } = new(8);

    /// <summary>
    ///     Gets or sets the members.
    /// </summary>
    /// <value>
    /// The members.
    /// </value>
    //public static BindingList<PartyMember> BindedMembers => new BindingList<PartyMember>(Members);

    /// <summary>
    ///     Gets or sets the Leader.
    /// </summary>
    public PartyMember Leader { get; set; } = default!;

    /// <summary>
    ///     Gets the name of the member by.
    /// </summary>
    /// <param name="playerName">Name of the player.</param>
    /// <returns></returns>
    public PartyMember GetMemberByName(string playerName)
    {
        return Members.FirstOrDefault(m => m.Name == playerName)!;
    }

    /// <summary>
    ///     Gets the member by identifier.
    /// </summary>
    /// <param name="memberId">The member identifier.</param>
    /// <returns></returns>
    public PartyMember GetMemberById(int memberId)
    {
        return Members.FirstOrDefault(m => m.MemberId == memberId)!;
    }

    /// <summary>
    ///     Clears this instance.
    /// </summary>
    internal void Clear()
    {
        Members = new();
        Leader = new();
    }
}

