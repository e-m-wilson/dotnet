using Library.Models;

namespace Library.Repositories;

public interface IMemberRepository
{
    List<Member> GetAllMembers();

    Member AddMember(Member memberToAdd);

    void SaveMemberChanges(List<Member> members);
}
