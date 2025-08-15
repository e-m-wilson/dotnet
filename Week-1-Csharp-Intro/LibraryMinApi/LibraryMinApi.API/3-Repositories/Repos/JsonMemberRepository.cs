using System.IO;
using System.Text.Json;
using Library.Models;

namespace Library.Repositories;

public class JsonMemberRepository : IMemberRepository
{
    //Filepath to my .json file that will hold my Member list
    private readonly string _filePath; //Notice the naming convention for my readonly, beginning with underscore

    // Our repo, like any dependency, needs a constructor
    public JsonMemberRepository()
    {
        _filePath = "./5-Data-Files/members.json"; // Relative filepath to where we want/expect the json to be
    }

    public List<Member> GetAllMembers()
    {
        //Try-catch, since are using File-IO i.e., an unmanaged resource
        try
        {
            if (!File.Exists(_filePath))
            {
                return new List<Member>(); // if the file DOESN'T EXIST, then there are no members. We return an empty list.
            }

            //using statement for my stream, it opens and reads the file.
            //Inside my file, is a single string of text in JSON format
            using FileStream stream = File.OpenRead(_filePath);

            //We take that stream of text, and deserialize it into a list of Member objects
            //If somehow, we made it this far and the file exists but has no text in it, we just return a blank list
            return JsonSerializer.Deserialize<List<Member>>(stream) ?? new List<Member>();
        }
        catch
        {
            throw new Exception("Failed to retrieve member list");
        }
    }

    public Member AddMember(Member memberToAdd)
    {
        List<Member> memberList = GetAllMembers();

        //If our search for an existing member is not null (aka they already exist)
        //we throw an exception to let the user know what happened
        if (memberList.Find(m => m.Email.Equals(memberToAdd.Email)) is not null)
            throw new Exception("Member with this email already exists.");

        //Adding our new member to our memberList we pulled from the json
        memberList.Add(memberToAdd);

        //Saving changes to the json (technically a new file is created in place of the old one)
        SaveMemberChanges(memberList);

        return memberToAdd;
    }

    public void SaveMemberChanges(List<Member> members)
    {
        using var stream = File.Create(_filePath); //Creating the file
        JsonSerializer.Serialize(stream, members); //Our file will hold a list of Members, serialized to Json
    }
}
