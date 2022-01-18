namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PersonalBudget.Web.ViewModels.Members;

    public interface IMemberService
    {
        Task CreateMember(CreateMemberInputModel createMember, string userId);

        IEnumerable<T> GetAll<T>(string userId);

        IEnumerable<KeyValuePair<string, string>> GetAllMembers(string userId);

        T GetById<T>(string userId, int id);

        Task Edit(MemberViewModel editMember, string userId, int id);

        Task Delete(string userId, int id);

        bool Exists(string userId, string name);
    }
}
